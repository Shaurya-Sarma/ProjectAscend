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
  private int loadedSceneCounter = 0;
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


  private void OnEnable()
  {
    SceneManager.sceneLoaded += OnSceneLoaded;
  }

  void OnSceneLoaded(Scene scene, LoadSceneMode mode)
  {

    if (SceneManager.GetActiveScene().name == "Graveyard" && lastRespawnPointPos == new Vector2(0, 0))
    {
      lastRespawnPointPos = GameObject.FindGameObjectWithTag("Respawn").GetComponent<Transform>().position;

    }
    else if (SceneManager.GetActiveScene().name == "Church" && lastRespawnPointPos == new Vector2(0, 0))
    {
      lastRespawnPointPos = GameObject.FindGameObjectWithTag("Respawn").GetComponent<Transform>().position;
    }
    else if (SceneManager.GetActiveScene().name == "AsmodeusBoss" && lastRespawnPointPos == new Vector2(0, 0))
    {
      lastRespawnPointPos = GameObject.FindGameObjectWithTag("Respawn").GetComponent<Transform>().position;
    }

    loadedSceneCounter++;
    if (loadedSceneCounter == 2)
    {
      LoadPlayer();
    }

  }

  public void RestartGame()
  {
    //TODO SHOW GAME OVER SCREEN
    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
  }

  public void SavePlayer()
  {
    SaveSystem.SavePlayer();
  }

  public void LoadPlayer()
  {
    PlayerData data = SaveSystem.LoadPlayer();

    if (data != null)
    {
      SceneManager.LoadScene(data.level);

      lastRespawnPointPos.x = data.position[0];
      lastRespawnPointPos.y = data.position[1];
    }

  }

}
