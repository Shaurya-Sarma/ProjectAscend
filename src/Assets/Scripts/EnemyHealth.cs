using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
  public int maxHealth;
  private int currentHealth;
  public Animator animator;
  // public Rigidbody2D rb;
  private void Start()
  {
    currentHealth = maxHealth;
  }
  public void takeDamage(int damage)
  {
    currentHealth -= damage;

    if (currentHealth <= 0)
    {
      EnemyDie();
    }
    else
    {
      // rb.AddForce(transform.up * 100);
    }
  }

  void EnemyDie()
  {
    animator.SetBool("IsDead", true);
    // rb.bodyType = RigidbodyType2D.Kinematic;
    GetComponent<BoxCollider2D>().enabled = false;
    this.enabled = false;
  }

  public void DeactivateEnemy()
  {
    this.gameObject.SetActive(false);
  }
}
