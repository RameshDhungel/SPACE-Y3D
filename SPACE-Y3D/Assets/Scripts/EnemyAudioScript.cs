using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudioScript : MonoBehaviour
{
    public AudioSource AudioSource;
    public AudioClip degalClip;

    private void Start()
    {
        //AudioSource = this.gameObject.GetComponent<AudioSource>();
    }
    public void PlayAudio()
    {
        AudioSource.clip = degalClip;
        AudioSource.Play();
    }

}
