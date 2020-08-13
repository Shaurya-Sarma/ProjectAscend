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
  public GameMaster gameMaster;
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
  }

  private IEnumerator PlayerHurt()
  {
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

  private void playerDeath()
  {
    GetComponent<PlayerController>().enabled = false;
    Physics2D.IgnoreLayerCollision(9, 10);
    animator.SetBool("isDead", true);
  }

  private void RespawnPlayer()
  {
    gameMaster.RestartGame();
    health = numOfHearts;
    animator.SetBool("isDead", false);
    GetComponent<PlayerController>().enabled = true;
    Physics2D.IgnoreLayerCollision(9, 10, false);
  }

}
