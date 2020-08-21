using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsmodeusBehavior : MonoBehaviour
{
  private Transform player;
  public float attackRange = 2f;
  public LayerMask damageLayer;
  public Transform attackPoint;
  public GameObject projectile;
  private Rigidbody2D rb;
  private BoxCollider2D col;
  public GameObject skeleton;
  private Transform SpawnPointOne;
  private Transform SpawnPointTwo;
  public GameObject door;
  private void Start()
  {
    player = GameObject.FindGameObjectWithTag("Player").transform;
    rb = GetComponent<Rigidbody2D>();
    col = GetComponent<BoxCollider2D>();
    SpawnPointOne = GameObject.Find("SpawnPointOne").transform;
    SpawnPointTwo = GameObject.Find("SpawnPointTwo").transform;


  }
  public void LookAtPlayer()
  {
    if (transform.position.x > player.position.x)
    {
      transform.eulerAngles = new Vector3(0, 0, 0);
    }
    else if (transform.position.x < player.position.x)
    {
      transform.eulerAngles = new Vector3(0, 180, 0);
    }
  }

  public void MeleeAttack()
  {
    Collider2D hitPlayer = Physics2D.OverlapCircle(attackPoint.position, attackRange, damageLayer);
    if (hitPlayer != null && hitPlayer.tag == "Player")
    {
      hitPlayer.GetComponent<HealthSystem>().takeDamage();
    }
  }

  public void RangedAttack()
  {
    Instantiate(projectile, transform.position, Quaternion.identity);
  }

  public void SummonEnemies()
  {
    Instantiate(skeleton, SpawnPointOne.position, Quaternion.identity);
    Instantiate(skeleton, SpawnPointTwo.position, Quaternion.identity);
  }

  public void SpawnDoor()
  {
    Instantiate(door, SpawnPointTwo.position, Quaternion.identity);
  }

  private void OnDrawGizmosSelected()
  {
    if (attackPoint == null)
    {
      return;
    }
    Gizmos.DrawWireSphere(attackPoint.position, attackRange);
  }
}
