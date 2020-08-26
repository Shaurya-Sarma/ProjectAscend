using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
  public Text NameText;
  public Text DialogueText;
  private Queue<string> sentences;
  public Animator animator;
  private Animator soulAnimator = null;

  private void Start()
  {
    sentences = new Queue<string>();

  }

  public void StartDialogue(Dialogue dialogue)
  {
    animator.SetBool("IsOpen", true);
    NameText.text = dialogue.name;
    sentences.Clear();
    foreach (string sentence in dialogue.sentences)
    {
      sentences.Enqueue(sentence);
    }
    DisplayNextSentence();
  }

  public void DisplayNextSentence()
  {
    if (sentences.Count == 0)
    {
      EndDialog();
      return;
    }

    string sentence = sentences.Dequeue();
    StopAllCoroutines();
    StartCoroutine(TypeSentence(sentence));
  }

  private IEnumerator TypeSentence(string sentence)
  {
    DialogueText.text = "";
    foreach (char letter in sentence.ToCharArray())
    {
      DialogueText.text += letter;
      yield return null; // WAITING FOR FRAME
    }
  }

  private void EndDialog()
  {
    animator.SetBool("IsOpen", false);
    if (soulAnimator != null)
    {
      soulAnimator.SetTrigger("Death");
      soulAnimator = null;
    }
  }

  public void FindSoulAnimator(Animator anim)
  {
    soulAnimator = anim;
  }

}
