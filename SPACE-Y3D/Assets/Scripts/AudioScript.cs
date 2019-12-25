using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScript : MonoBehaviour
{

    public AudioSource AudioSource;
    public AudioClip degalClip;
    
    public void GunShotSound()
    {
        AudioSource.clip = degalClip;
        AudioSource.Play();
    }
   
}
