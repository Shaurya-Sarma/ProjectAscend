using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkeletonBehavior : MonoBehaviour
{
  public float speed = 2f;
  public float rayDistance = 2f;
  private bool movingLeft = true;
  public Transform groundDetection;
  public LayerMask ignoreLayers;

  private void Update()
  {
    transform.Translate(Vector2.left * speed * Time.deltaTime);
    RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, rayDistance, ~ignoreLayers);
    RaycastHit2D wallInfo = movingLeft ? Physics2D.Raycast(groundDetection.position, Vector2.left, rayDistance, ~ignoreLayers) : Physics2D.Raycast(groundDetection.position, Vector2.right, rayDistance, ~ignoreLayers);

    if (!groundInfo.collider || wallInfo.collider)
    {
      if (movingLeft)
      {
        transform.eulerAngles = new Vector3(0, 180, 0); ;
      }
      else
      {
        transform.eulerAngles = new Vector3(0, 0, 0);
      }
      movingLeft = !movingLeft;
    }
  }




}
