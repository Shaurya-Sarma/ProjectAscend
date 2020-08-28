using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EndScreen : MonoBehaviour
{
  public void ButtonClicked()
  {
    GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>().HardResetGame();
    GameObject.FindGameObjectWithTag("GameMaster").GetComponent<AudioManager>().Play("Click");
  }
}