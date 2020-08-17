using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGhoulBehavior : MonoBehaviour
{
  public Transform player;
  public float visionRange = 5.5f;
  public float speed = 1f;
  private Rigidbody2D rb;
  private EnemySkeletonBehavior skeletonScript;
  private HealthSystem playerHealth;
  public Animator animator;
  public Transform castPoint;
  public LayerMask actionLayer;
  public LayerMask objectLayer;
  public float explosionRange = 2f;
  public float blastRadius = 3f;
  public GameObject explosionEffect;

  private void Start()
  {
    rb = GetComponent<Rigidbody2D>();
    skeletonScript = GetComponent<EnemySkeletonBehavior>();
    playerHealth = player.GetComponent<HealthSystem>();
  }
  private void Update()
  {
    if (CanSeePlayer(visionRange))
    {
      skeletonScript.enabled = false;
      animator.SetBool("IsChasing", true);
      ChasePlayer();
    }
    else
    {
      skeletonScript.enabled = true;
      animator.SetBool("IsChasing", false);
    }
  }
  private void ChasePlayer()
  {
    if (transform.position.x - player.position.x < -explosionRange)
    {
      rb.velocity = new Vector2(speed + 2, 0);
      transform.eulerAngles = new Vector3(0, 180, 0);
      skeletonScript.movingLeft = false;
    }
    else if (transform.position.x - player.position.x > explosionRange)
    {
      rb.velocity = new Vector2(-(speed + 2), 0);
      transform.eulerAngles = new Vector3(0, 0, 0); ;
      skeletonScript.movingLeft = true;
    }
    else
    {
      rb.velocity = new Vector2(0, 0);
      animator.SetBool("IsChasing", false);
      Explode();
    }
  }

  private bool CanSeePlayer(float distance)
  {
    bool val = false;
    float castDistance = skeletonScript.movingLeft ? -distance : distance;
    Vector2 endPosition = castPoint.position + Vector3.right * castDistance;
    RaycastHit2D visionInfo = Physics2D.Linecast(transform.position, endPosition, actionLayer);
    if (visionInfo.collider != null)
    {
      if (visionInfo.collider.gameObject.tag == "Player")
      {
        val = true;
      }
      else
      {
        val = false;

      }

      Debug.DrawLine(castPoint.position, visionInfo.point, Color.red);

    }
    else
    {
      Debug.DrawLine(castPoint.position, endPosition, Color.blue);

    }
    return val;
  }

  private void Explode()
  {
    Instantiate(explosionEffect, transform.position, Quaternion.identity);
    Destroy(gameObject);
    Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, blastRadius, objectLayer);
    foreach (Collider2D nearbyObject in colliders)
    {
      if (nearbyObject.tag == "Player")
      {
        playerHealth.health = 0;
        playerHealth.playerDeath();
      }
    }
  }


}


