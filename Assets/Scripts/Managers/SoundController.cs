using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public AudioSource backgroundSFX;
    public AudioSource yesSFX;
    public AudioSource noSFX;
    public AudioSource coinSFX;
    public AudioSource screamSFX;

    // Use this for initialization
    void Start ()
    {
        backgroundSFX.Play();
    }
	
	public void PlayYesSound()
    {
        yesSFX.Play();
    }

    public void PlayNoSound()
    {
        noSFX.Play();
    }

    public void PlayCoinSound()
    {
        coinSFX.Play();
    }

    public void PlayScreamSound()
    {
        screamSFX.Play();
    }
}
