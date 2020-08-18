using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Door : MonoBehaviour
{
  private GameMaster gm;
  private GameObject key;
  private bool canPlayerOpenDoor = true;
  public bool isDoorLocked = false;

  private void Start()
  {
    gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();
  }
  private void OnTriggerEnter2D(Collider2D other)
  {
    if (isDoorLocked)
    {
      canPlayerOpenDoor = GameObject.FindGameObjectWithTag("Key").GetComponent<Key>().playerHasKey;
    }

    if (other.tag == "Player")
    {
      if (canPlayerOpenDoor == false)
      {
        gm.DoorText.text = ("Door Is Locked");
      }
      else
      {
        gm.DoorText.text = ("[E] To Enter");
        if (Input.GetKeyDown(KeyCode.E))
        {
          GameObject.Find("LevelTransition").GetComponent<LevelTransition>().FadeToNextLevel();
        }
      }

    }
  }

  private void OnTriggerStay2D(Collider2D other)
  {

    if (isDoorLocked)
    {
      canPlayerOpenDoor = GameObject.FindGameObjectWithTag("Key").GetComponent<Key>().playerHasKey;
    }
    if (other.tag == "Player")
    {
      if (canPlayerOpenDoor == false)
      {
        gm.DoorText.text = ("Door Is Locked");

      }
      else
      {
        if (Input.GetKeyDown(KeyCode.E))
        {
          GameObject.Find("LevelTransition").GetComponent<LevelTransition>().FadeToNextLevel();
        }
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


}
