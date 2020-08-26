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
    if (Input.GetKeyDown(KeyCode.Escape))
    {
      if (isPaused)
      {
        Resume();
      }
      else
      {
        Pause();
      }
    }
  }

  public void Resume()
  {
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
    Application.Quit();
  }

}
