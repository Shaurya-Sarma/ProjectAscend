using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  private Rigidbody2D rb;
  public float speed;
  public float jumpForce;
  private float playerInput;
  private bool isGrounded;
  public Transform feetPos;
  public float checkRadius;
  public LayerMask whatIsGround;
  private float jumpTimeCounter;
  public float jumpTime;
  private bool isJumping;
  public Animator animator;
  public Transform attackPoint;
  public float attackRange;
  public LayerMask enemyLayers;
  public float jumpBufferTime = 0.2f;
  private float jumpBufferCounter;
  public float attackTimer = 0.8f;
  private float attackCounter;

  private void Start()
  {
    rb = GetComponent<Rigidbody2D>();
  }

  private void Update()
  {
    isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);

    //* ROTATE PLAYER 
    if (playerInput > 0)
    {
      transform.eulerAngles = new Vector3(0, 0, 0);
    }
    else if (playerInput < 0)
    {
      transform.eulerAngles = new Vector3(0, 180, 0);
    }

    //* PLAYER JUMP

    if (jumpBufferCounter > 0 && Input.GetKeyDown(KeyCode.Space))
    {
      isJumping = true;
      animator.SetBool("isJumping", true);
      jumpTimeCounter = jumpTime;
      rb.velocity = Vector2.up * jumpForce;
    }

    if (isGrounded)
    {
      animator.SetBool("inAir", false);
      jumpBufferCounter = jumpBufferTime;
    }
    else
    {
      animator.SetBool("inAir", true);
      jumpBufferCounter -= Time.deltaTime;
    }

    if (Input.GetKey(KeyCode.Space))
    {
      if (jumpTimeCounter > 0 && isJumping)
      {
        rb.velocity = Vector2.up * jumpForce;
        jumpTimeCounter -= Time.deltaTime;
      }
      else
      {
        isJumping = false;
        animator.SetBool("isJumping", false);

      }
    }
    if (Input.GetKeyUp(KeyCode.Space))
    {
      isJumping = false;
      animator.SetBool("isJumping", false);

    }

    //* PLAYER MELEE

    // LEFT MOUSE BUTTON CLICK
    if (Input.GetKey(KeyCode.Q) && Time.time > attackCounter)
    {
      attackCounter = Time.time + attackTimer;
      PlayerAttack();
    }

  }

  void PlayerAttack()
  {
    // PLAY ATTACK ANIMATION
    animator.SetTrigger("Attack");
  }

  public void attackEnemies()
  {
    // DETECT ENEMIES IN RANGE OF ATTACK
    Collider2D[] damagedEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
    // DAMAGE ENEMIES
    foreach (Collider2D enemy in damagedEnemies)
    {
      if (enemy.tag == "EnemyProjectile")
      {
        Destroy(enemy.gameObject);
      }
      else
      {
        enemy.GetComponent<EnemyHealth>().takeDamage(10);
      }
    }
  }

  private void OnDrawGizmosSelected()
  {
    if (attackPoint == null)
    {
      return;
    }
    Gizmos.DrawWireSphere(attackPoint.position, attackRange);
  }
  private void FixedUpdate()
  {
    playerInput = Input.GetAxisRaw("Horizontal");
    rb.velocity = new Vector2(playerInput * speed, rb.velocity.y);
    animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
  }

  private void OnCollisionEnter2D(Collision2D other)
  {
    if (other.gameObject.tag == "MovingPlatform")
    {
      this.transform.parent = other.transform;
    }
  }

  private void OnCollisionExit2D(Collision2D other)
  {
    if (other.gameObject.tag == "MovingPlatform")
    {
      this.transform.parent = null;
    }
  }
}

