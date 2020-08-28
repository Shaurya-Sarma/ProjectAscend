using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
  public void SetVolume(bool isOn)
  {
    AudioListener.pause = !isOn;
  }

  public void SetQuality(int index)
  {
    QualitySettings.SetQualityLevel(index);
    Debug.Log("helo");

  }
  public void SetFullscreen(bool isFullscreen)
  {
    Screen.fullScreen = isFullscreen;
  }
}
