using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
  public int maxHealth;
  public int currentHealth;
  public Animator animator;
  private Material matWhite;
  private Material matDefault;
  private SpriteRenderer sr;
  private void Start()
  {
    currentHealth = maxHealth;
    sr = GetComponent<SpriteRenderer>();
    matWhite = Resources.Load("WhiteFlash", typeof(Material)) as Material;
    matDefault = sr.material;
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
      sr.material = matWhite;
      Invoke("ResetMaterial", 0.2f);
    }
  }

  private void ResetMaterial()
  {
    sr.material = matDefault;
  }

  public void EnemyDie()
  {
    animator.SetBool("IsDead", true);
    GetComponent<BoxCollider2D>().enabled = false;
    this.enabled = false;
  }

  public void DeactivateEnemy()
  {
    this.gameObject.SetActive(false);
  }
}
