using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class PlayerData
{
  public int level;
  public float[] position;

  public PlayerData()
  {
    level = SceneManager.GetActiveScene().buildIndex;

    position = new float[2];
    position[0] = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>().lastRespawnPointPos.x;
    position[1] = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>().lastRespawnPointPos.y;

  }
}
