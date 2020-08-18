using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
  private SpringJoint2D spring;
  public bool playerHasKey = false;
  private void Start()
  {
    spring = GetComponent<SpringJoint2D>();
    spring.enabled = false;
    GameObject backpack = GameObject.FindGameObjectWithTag("Backpack");
    spring.connectedBody = backpack.GetComponent<Rigidbody2D>();
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.tag == "Player")
    {
      spring.enabled = true;
      playerHasKey = true;
    }
  }
}
