using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
  public GameObject activatedEffect;
  public bool isActivated = false;
  private GameMaster gm;
  private void Start()
  {
    gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (gm.lastRespawnPointPos.x == this.transform.position.x)
    {
      isActivated = true;
    }

    if (other.tag == "Player")
    {
      if (!isActivated)
      {
        gm.InteractText.text = ("Press [E] To Ignite");
        if (Input.GetKeyDown(KeyCode.E))
        {
          gm.lastRespawnPointPos = transform.position;
          isActivated = true;
          Instantiate(activatedEffect, (this.transform.position + new Vector3(0, 0.75f, 0)), Quaternion.identity);
        }
      }
      else
      {
        gm.InteractText.text = (" ");
      }
    }
  }

  private void OnTriggerStay2D(Collider2D other)
  {
    if (gm.lastRespawnPointPos.x == this.transform.position.x)
    {
      isActivated = true;
    }

    if (other.tag == "Player")
    {
      if (!isActivated)
      {
        gm.InteractText.text = ("Press [E] To Ignite");
        if (Input.GetKeyDown(KeyCode.E))
        {
          gm.lastRespawnPointPos = transform.position;
          isActivated = true;
          Instantiate(activatedEffect, (this.transform.position + new Vector3(0, 0.75f, 0)), Quaternion.identity);
        }
      }
      else
      {
        gm.InteractText.text = (" ");
      }
    }
  }

  private void OnTriggerExit2D(Collider2D other)
  {
    if (other.tag == "Player")
    {
      gm.InteractText.text = (" ");
    }
  }

}
