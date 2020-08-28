using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
  public static bool isPaused;
  public GameObject pauseMenuUI;

  private void Awake()
  {
    isPaused = false;
  }
  private void Start()
  {
    isPaused = false;

  }
  private void Update()
  {
    if (Input.GetButtonDown("Pause"))
    {
      if (isPaused)
      {
        Resume();
      }
      else
      {
        GameObject.FindGameObjectWithTag("GameMaster").GetComponent<AudioManager>().Play("Click");
        Pause();
      }
    }
  }

  public void Resume()
  {
    GameObject.FindGameObjectWithTag("GameMaster").GetComponent<AudioManager>().Play("Click");
    pauseMenuUI.SetActive(false);
    Time.timeScale = 1f;
    isPaused = false;
  }
  public void Pause()
  {
    pauseMenuUI.SetActive(true);
    Time.timeScale = 0f;
    isPaused = true;
  }

  public void ReloadLevel()
  {
    Resume();
    SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
  }

  public void Quit()
  {
    GameObject.FindGameObjectWithTag("GameMaster").GetComponent<AudioManager>().Play("Click");
    GameObject.FindObjectOfType<GameMaster>().SavePlayer();
    Application.Quit();
  }

}
