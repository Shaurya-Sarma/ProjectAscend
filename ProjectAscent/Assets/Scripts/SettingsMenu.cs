using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
  public void SetVolume(bool isMute)
  {
    AudioListener.pause = isMute;
  }

  public void SetQuality(int index)
  {
    QualitySettings.SetQualityLevel(index + 2);
    Debug.Log("helo");

  }
  public void SetFullscreen(bool isFullscreen)
  {
    Screen.fullScreen = isFullscreen;
  }
}
