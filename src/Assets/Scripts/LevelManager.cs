using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
  public static LevelManager instance;
  public Transform respawnPoint;
  private void Awake()
  {
    instance = this;
    // GameObject.FindGameObjectWithTag("Player").transform.position = respawnPoint.position;
  }

  public void ReloadLevel()
  {
    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
  }



}
