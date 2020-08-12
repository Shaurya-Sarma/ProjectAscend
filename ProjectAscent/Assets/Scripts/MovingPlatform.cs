using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
  public GameObject platform;
  public float speed;
  private Transform currentPoint;
  public Transform[] points;
  public int pointSelection;

  private void Start()
  {
    currentPoint = points[pointSelection];
  }

  private void FixedUpdate()
  {
    platform.transform.position = Vector3.MoveTowards(platform.transform.position, currentPoint.position, speed * Time.deltaTime);
    if (platform.transform.position == currentPoint.position)
    {
      pointSelection++;
      if (pointSelection == points.Length)
      {
        pointSelection = 0;
      }
      currentPoint = points[pointSelection];
    }
  }

}
