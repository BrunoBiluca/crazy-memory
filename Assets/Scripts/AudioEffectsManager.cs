using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEffectsManager : MonoBehaviour
{
    private static AudioEffectsManager instance;
    public static AudioEffectsManager Instance {
        get {
            return instance;
        }
    }

    [SerializeField] private AudioClip clickSoundEffect;

    private AudioSource audioSource;

    private void Awake()
    {
        instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayClickSound()
    {
        audioSource.clip = clickSoundEffect;
        audioSource.Play();
    }
}
