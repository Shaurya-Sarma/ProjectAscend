using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{
  public Text InteractText;
  public static GameMaster gm;
  private void Start()
  {
    if (!gm)
    {
      gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();
    }
    Vector3 respawnPoint = GameObject.FindGameObjectWithTag("Respawn").transform.position;
    GameObject.FindGameObjectWithTag("Player").transform.position = new Vector3(respawnPoint.x, respawnPoint.y, respawnPoint.z);

    foreach (var text in GameObject.FindGameObjectsWithTag("Text"))
    {
      text.GetComponent<Renderer>().sortingLayerName = "Base";

    }
  }

  public void RestartGame()
  {
    //TODO SHOW GAME OVER SCREEN
    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
  }




}
