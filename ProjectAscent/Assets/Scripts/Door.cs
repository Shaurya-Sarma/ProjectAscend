using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
  private GameMaster gm;

  private void Start()
  {
    gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();
  }
  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.tag == "Player")
    {
      gm.DoorText.text = ("[E] To Enter");
      if (Input.GetKeyDown(KeyCode.E))
      {
        LoadNextScene();

      }
    }
  }

  private void OnTriggerStay2D(Collider2D other)
  {
    if (other.tag == "Player")
    {
      if (Input.GetKeyDown(KeyCode.E))
      {
        LoadNextScene();
      }
    }
  }

  private void OnTriggerExit2D(Collider2D other)
  {
    if (other.tag == "Player")
    {
      gm.DoorText.text = (" ");
    }
  }

  private void LoadNextScene()
  {
    int SceneToLoad = SceneManager.GetActiveScene().buildIndex + 1;
    SceneManager.LoadScene(SceneToLoad);
  }
}
