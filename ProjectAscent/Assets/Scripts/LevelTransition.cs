using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransition : MonoBehaviour
{
  public Animator animator;
  private int LevelToLoad;

  public void FadeToNextLevel()
  {
    FadeToLevel(SceneManager.GetActiveScene().buildIndex + 1);
  }
  public void FadeToLevel(int levelIndex)
  {
    LevelToLoad = levelIndex;
    animator.SetTrigger("FadeOut");
  }

  public void OnFadeComplete()
  {
    SceneManager.LoadScene(LevelToLoad);
  }
}
