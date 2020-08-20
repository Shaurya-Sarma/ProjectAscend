using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBoss : MonoBehaviour
{
  public GameObject boss;
  public Transform spawnPos;
  public float spawnDelay = 2.5f;

  private void Start()
  {
    boss.GetComponent<BoxCollider2D>().enabled = false;
  }
  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.tag == "Player")
    {
      Invoke("spawnBoss", spawnDelay);
      GetComponent<CircleCollider2D>().enabled = false;
    }
  }

  private void spawnBoss()
  {
    Instantiate(boss, spawnPos.position, Quaternion.identity);
  }
}
