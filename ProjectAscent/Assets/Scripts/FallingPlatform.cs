using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
  public Rigidbody2D rb;
  public float fallDelay;

  private void OnCollisionEnter2D(Collision2D other)
  {
    if (other.collider.CompareTag("Player"))
    {
      StartCoroutine(Fall());
      Destroy(gameObject, 3f);
    }
  }

  IEnumerator Fall()
  {
    yield return new WaitForSeconds(fallDelay);
    rb.isKinematic = false;
    // GetComponent<BoxCollider2D>().isTrigger = true;
    yield return 0;
  }
}
