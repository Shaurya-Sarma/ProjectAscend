using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  private Rigidbody2D rb;
  public float speed;
  public float jumpForce;
  private int jumpCounter = 0;
  public int maxJumps = 1;
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
  public float jumpBufferTime = 0.1f;
  private float jumpBufferCounter;
  public float attackTimer = 0.8f;
  private float attackCounter;
  private GameMaster gm;
  public float slopeFriction;

  private void Start()
  {
    rb = GetComponent<Rigidbody2D>();
    gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();
    transform.position = gm.lastRespawnPointPos + new Vector2(0, 1f);
    jumpCounter = maxJumps;
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

    if (jumpBufferCounter > 0 && Input.GetButtonDown("Jump"))
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

    if (Input.GetButton("Jump"))
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
    if (Input.GetButtonUp("Jump"))
    {
      isJumping = false;
      animator.SetBool("isJumping", false);

    }

    //* PLAYER MELEE

    // LEFT MOUSE BUTTON CLICK
    if (Input.GetButtonDown("Attack") && Time.time > attackCounter)
    {
      attackCounter = Time.time + attackTimer;
      PlayerAttack();
    }

  }

  void PlayerAttack()
  {
    // PLAY ATTACK ANIMATION
    GameObject.FindGameObjectWithTag("GameMaster").GetComponent<AudioManager>().Play("PlayerAttack");
    animator.SetTrigger("Attack");
  }

  public void attackEnemies()
  {
    // DETECT ENEMIES IN RANGE OF ATTACK
    Collider2D[] damagedEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
    // DAMAGE ENEMIES
    foreach (Collider2D enemy in damagedEnemies)
    {
      if (enemy.tag == "EnemyProjectile" || enemy.tag == "SlowProjectile")
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
    if (feetPos == null)
    {
      return;
    }
    Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    Gizmos.DrawWireSphere(feetPos.position, checkRadius);

  }
  private void FixedUpdate()
  {
    playerInput = Input.GetAxisRaw("Horizontal");
    rb.velocity = new Vector2(playerInput * speed, rb.velocity.y);
    animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
    NormalizeSlope();
  }

  void NormalizeSlope()
  {
    // Attempt vertical normalization
    if (isGrounded)
    {
      RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, 2f, whatIsGround);

      if (hit.collider != null && Mathf.Abs(hit.normal.x) > 0.1f)
      {
        Rigidbody2D body = GetComponent<Rigidbody2D>();
        // Apply the opposite force against the slope force 
        // You will need to provide your own slopeFriction to stabalize movement
        body.velocity = new Vector2(body.velocity.x - (hit.normal.x * slopeFriction), body.velocity.y);

        //Move Player up or down to compensate for the slope below them
        Vector3 pos = transform.position;
        pos.y += -hit.normal.x * Mathf.Abs(body.velocity.x) * Time.deltaTime * (body.velocity.x - hit.normal.x > 0 ? 1 : -1);
        transform.position = pos;
      }
    }
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

