using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMageBehavior : MonoBehaviour
{
  private Transform player;
  private bool isAttacking = false;
  private Vector3 distance;
  private float distanceFrom;
  public float fireRate = 1.5f;
  private float nextFire;
  public GameObject magicOrb;
  public Animator animator;
  private void Start()
  {
    nextFire = Time.time;
  }
  private void Update()
  {
    player = GameObject.FindGameObjectWithTag("Player").transform;

    // Determine Distance between Mage And Player
    distance = this.transform.position - player.position;
    distance.y = 0;
    distanceFrom = Vector2.Distance(this.transform.position, player.position);


    if (distance.x > 0)
    {
      transform.eulerAngles = new Vector3(0, 0, 0);
    }
    else if (distance.x < 0)
    {
      transform.eulerAngles = new Vector3(0, 180, 0);

    }

    if (distanceFrom < 10)
    {

      isAttacking = true;
    }
    else
    {
      isAttacking = false;

    }

    if (isAttacking && Time.time > nextFire)
    {
      animator.SetTrigger("Attack");

    }


  }
  public void MageAttack()
  {
    Instantiate(magicOrb, this.transform.position, Quaternion.identity);
    nextFire = Time.time + fireRate;

  }

}
