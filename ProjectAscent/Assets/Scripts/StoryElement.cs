using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryElement : MonoBehaviour
{
  public Dialogue dialogue;
  private GameMaster gm;
  private CircleCollider2D col;
  private void Start()
  {
    gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();
    col = GetComponent<CircleCollider2D>();

  }
  public void TriggerDialogue()
  {
    gm.InteractText.text = (" ");
    col.enabled = false;
    FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    FindObjectOfType<DialogueManager>().FindSoulAnimator(this.name == "Soul" ? GetComponent<Animator>() : null);
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.tag == "Player")
    {
      gm.InteractText.text = ("Press [E] To Talk");
      if (Input.GetButtonDown("Interact"))
      {
        TriggerDialogue();
      }
    }
  }

  private void OnTriggerStay2D(Collider2D other)
  {
    if (other.tag == "Player")
    {
      gm.InteractText.text = ("Press [E] To Talk");
      if (Input.GetButtonDown("Interact"))
      {
        TriggerDialogue();
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
