using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIdle : StateMachineBehaviour
{

  private Transform player;
  private Rigidbody2D rb;
  public float speed = 2f;
  public float attackRange = 3.5f;
  private AsmodeusBehavior boss;
  private int meleeCounter = 0;
  private int rangedCounter = 0;
  private bool isEnraged = false;
  private EnemyHealth bossHealth;
  private BoxCollider2D col;
  override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
  {
    player = GameObject.FindGameObjectWithTag("Player").transform;
    rb = animator.GetComponent<Rigidbody2D>();
    boss = animator.GetComponent<AsmodeusBehavior>();
    bossHealth = animator.GetComponent<EnemyHealth>();
    col = animator.GetComponent<BoxCollider2D>();


    col.enabled = true;
  }

  override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
  {
    boss.LookAtPlayer();
    Vector2 target = new Vector2(player.position.x, player.position.y);
    Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
    rb.MovePosition(newPos);

    float distanceBetween = Vector2.Distance(player.position, rb.position);
    if (distanceBetween <= attackRange)
    {
      animator.SetTrigger("Attack");
      meleeCounter++;
    }


    if (meleeCounter >= 3)
    {
      animator.SetTrigger("Shoot");
      meleeCounter = 0;
      rangedCounter++;
    }

    if (rangedCounter >= 2 && isEnraged)
    {
      animator.SetTrigger("Summon");
      rangedCounter = 0;
    }


    if (bossHealth.currentHealth <= 50 && !isEnraged)
    {
      isEnraged = true;
      speed = 3.5f;
    }


  }

  override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
  {
    animator.ResetTrigger("Attack");
  }

}
