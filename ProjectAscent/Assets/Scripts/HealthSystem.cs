using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthSystem : MonoBehaviour
{
  public int health;
  public int numOfHearts;
  public Image[] hearts;
  public Sprite fullHeart;
  public Sprite emptyHeart;
  public Animator animator;
  public float invincibleTime = 2f;
  private GameMaster gm;
  public float slowEffect = 3.5f;
  public float speedDecrease = 3f;
  private SpriteRenderer sr;
  private Color originalColor;
  private PlayerController player;

  private void Start()
  {
    sr = gameObject.GetComponent<SpriteRenderer>();
    originalColor = sr.material.color;
    player = GetComponent<PlayerController>();
    gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();

  }
  private void Update()
  {
    if (health > numOfHearts)
    {
      health = numOfHearts;
    }
    for (int i = 0; i < hearts.Length; i++)
    {
      if (i < health)
      {
        hearts[i].sprite = fullHeart;
      }
      else
      {
        hearts[i].sprite = emptyHeart;
      }

      if (i < numOfHearts)
      {
        hearts[i].enabled = true;
      }
      else
      {
        hearts[i].enabled = false;
      }
    }
  }

  private void OnCollisionEnter2D(Collision2D other)
  {
    if (other.gameObject.tag == "EnemyDamage")
    {
      takeDamage();

    }
    if (other.gameObject.tag == "Obstacle")
    {
      health = 0;
      playerDeath();
    }
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.gameObject.tag == "EnemyProjectile")
    {
      takeDamage();
    }

    if (other.gameObject.tag == "SlowProjectile")
    {
      StartCoroutine(PlayerSlow());
    }
  }

  public void takeDamage()
  {
    health--;
    if (health <= 0)
    {
      playerDeath();
    }
    else
    {
      StartCoroutine(PlayerHurt());
    }
  }
  private IEnumerator PlayerHurt()
  {
    GameObject.FindGameObjectWithTag("GameMaster").GetComponent<AudioManager>().Play("Hurt");
    //IGNORE COLLISIONS WITH OTHER ENEMIES
    int enemyLayer = LayerMask.NameToLayer("Enemies");
    int playerLayer = LayerMask.NameToLayer("Player");
    Physics2D.IgnoreLayerCollision(enemyLayer, playerLayer);
    GetComponent<BoxCollider2D>().enabled = false;
    GetComponent<BoxCollider2D>().enabled = true;
    // START LOOPING HURT ANIMATION
    animator.SetLayerWeight(1, 1);
    //WAIT FOR INVINCIBILITY TO END
    yield return new WaitForSeconds(invincibleTime);
    //STOP HURT ANIMATION AND RE-ENABLE COLLISIONS
    Physics2D.IgnoreLayerCollision(enemyLayer, playerLayer, false);
    animator.SetLayerWeight(1, 0);
  }

  public void playerDeath()
  {
    GameObject.FindGameObjectWithTag("GameMaster").GetComponent<AudioManager>().Play("Death");
    GetComponent<PlayerController>().enabled = false;
    Physics2D.IgnoreLayerCollision(9, 10);
    animator.SetBool("isJumping", false);
    animator.SetBool("isDead", true);
  }

  private void RespawnPlayer()
  {
    gm.RestartGame();
    health = numOfHearts;
    animator.SetBool("isDead", false);
    GetComponent<PlayerController>().enabled = true;
    Physics2D.IgnoreLayerCollision(9, 10, false);
  }

  private IEnumerator PlayerSlow()
  {
    player.speed = 3;
    sr.material.color = new Color(0, 0, 1.2f, 1);
    yield return new WaitForSeconds(slowEffect);
    sr.material.color = originalColor;
    player.speed = 7;
  }

}
