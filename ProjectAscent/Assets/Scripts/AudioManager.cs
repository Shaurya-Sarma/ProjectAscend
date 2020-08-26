using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
  public Sound[] sounds;

  private void Awake()
  {
    foreach (Sound sound in sounds)
    {
      sound.source = gameObject.AddComponent<AudioSource>();
      sound.source.clip = sound.clip;

      sound.source.volume = sound.volume;
      sound.source.pitch = sound.pitch;
      sound.source.loop = sound.loop;
    }
  }

  private void Start()
  {
    Play("Theme");
  }

  public void Play(string name)
  {
    Sound soundEffect = Array.Find(sounds, sound => sound.name == name);
    soundEffect.source.Play();
  }
}
