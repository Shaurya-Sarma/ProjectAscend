using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{
  public Text InteractText;
  private static GameMaster instance;
  public Vector2 lastRespawnPointPos;
  private void Awake()
  {
    if (instance == null)
    {
      instance = this;
      DontDestroyOnLoad(instance);
    }
    else
    {
      Destroy(gameObject);
    }

    foreach (var text in GameObject.FindGameObjectsWithTag("Text"))
    {
      text.GetComponent<Renderer>().sortingLayerName = "Base";
    }
  }

  // private void OnEnable()
  // {
  //   SceneManager.sceneLoaded += OnSceneLoaded;
  // }

  // void OnSceneLoaded(Scene scene, LoadSceneMode mode)
  // {

  //   if (SceneManager.GetActiveScene().name == "Graveyard" && lastRespawnPointPos == new Vector2(0, 0))
  //   {
  //     lastRespawnPointPos = GameObject.FindGameObjectWithTag("Respawn").GetComponent<Transform>().position;
  //   }
  //   else if (SceneManager.GetActiveScene().name == "Church" && lastRespawnPointPos == new Vector2(0, 0))
  //   {
  //     lastRespawnPointPos = GameObject.FindGameObjectWithTag("Respawn").GetComponent<Transform>().position;
  //   }
  //   else if (SceneManager.GetActiveScene().name == "AsmodeusBoss" && lastRespawnPointPos == new Vector2(-100000, 100000))
  //   {
  //     lastRespawnPointPos = GameObject.FindGameObjectWithTag("Respawn").GetComponent<Transform>().position;
  //   }
  // }

  public void RestartGame()
  {
    //TODO SHOW GAME OVER SCREEN
    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
  }




}
