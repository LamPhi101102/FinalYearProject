using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get;set; }

    public AudioSource dropItemSound;
    public AudioSource pickUpItemSound;
    public AudioSource craftingItemSound;
    public AudioSource BackgroundMusic;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public void PlayDropSound(AudioSource soundtoPlay)
    {
        if(!soundtoPlay.isPlaying)
        {
            soundtoPlay.Play();
        }
    }
}
