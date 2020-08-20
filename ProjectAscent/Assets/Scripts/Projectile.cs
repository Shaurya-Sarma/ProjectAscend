using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
  public float projectileSpeed = 4f;
  public Rigidbody2D rb;
  private GameObject target;
  private Vector2 moveDirection;

  private void Start()
  {
    target = GameObject.FindGameObjectWithTag("Player");
    moveDirection = (target.transform.position - this.transform.position).normalized * projectileSpeed;
    rb.velocity = new Vector2(moveDirection.x, moveDirection.y);
    if (transform.position.x > target.transform.position.x)
    {
      transform.eulerAngles = new Vector3(0, 0, 0);
    }
    else if (transform.position.x < target.transform.position.x)
    {
      transform.eulerAngles = new Vector3(0, 180, 0);
    }
    Destroy(gameObject, 3f);
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.gameObject.tag == "Player")
    {
      Destroy(gameObject);
    }
  }
}
