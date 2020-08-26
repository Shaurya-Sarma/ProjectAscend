﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
  public void StartGame()
  {
    GameObject.Find("LevelTransition").GetComponent<LevelTransition>().FadeToNextLevel();
  }

  public void QuitGame()
  {
    Application.Quit();
  }


}
