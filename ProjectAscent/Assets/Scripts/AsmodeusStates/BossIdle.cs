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
  private bool isEnraged = false;
  private EnemyHealth bossHealth;
  public GameObject enragedEffect;

  // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
  override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
  {
    player = GameObject.FindGameObjectWithTag("Player").transform;
    rb = animator.GetComponent<Rigidbody2D>();
    boss = animator.GetComponent<AsmodeusBehavior>();
    animator.GetComponent<BoxCollider2D>().enabled = true;
    bossHealth = animator.GetComponent<EnemyHealth>();
  }

  // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
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

    Debug.Log(meleeCounter);

    if (meleeCounter >= 3)
    {
      animator.SetTrigger("Shoot");
      meleeCounter = 0;
    }

    if (bossHealth.currentHealth <= 50 && !isEnraged)
    {
      isEnraged = true;
      speed = 3.5f;
    }





  }

  // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
  override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
  {
    animator.ResetTrigger("Attack");
  }

  // OnStateMove is called right after Animator.OnAnimatorMove()
  //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
  //{
  //    // Implement code that processes and affects root motion
  //}

  // OnStateIK is called right after Animator.OnAnimatorIK()
  //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
  //{
  //    // Implement code that sets up animation IK (inverse kinematics)
  //}
}
