using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX : MonoBehaviour
{
    public AudioClip _deathSound; // Sound clip for death
    public AudioClip _hit; // Sound clip for hit
    public AudioClip _run; // Sound clip for running
    public AudioClip _agitated; // Sound clip for being agitated
    public AudioClip _walk; // Sound clip for walking
    public AudioSource _audioSource; // Audio source for movement sounds
    public AudioSource _audioSourceWalk; // Audio source for walk sound
    public AudioSource _audioSourceRun; // Audio source for run sound
    public AudioSource _audioSourceAgitated; // Audio source for agitated sounds

  /*  public bool _isFadingOut = false; // Flag to check if fading out is in progress
    private bool _isFadingOutAgitated = false; // Flag to check if fading out agitated sound is in progress
    private float fadeOutTime = 1f; // Time it takes to fade out the sound*/


    void Start()
    {
        // Initialization code goes here
    }

    public void PlaySoundDeath()
    {
        _audioSource.volume = 1f;

        _audioSource.PlayOneShot(_deathSound);
    }
    public void PlaySoundHit()
    {
        _audioSource.volume = 1f;
        _audioSource.PlayOneShot(_hit);
    }

   /* public void PlaySoundRun()
    {   _audioSource.clip = _run;
        _audioSource.volume = 1f;

        
        _audioSource.loop = true;
        _audioSource.Play();
    }*/

    public void PlaySoundAgitated()
    {
        _audioSourceAgitated.volume = 0.5f;
        _audioSourceAgitated.clip = _agitated;
        _audioSourceAgitated.Play();
    }

    public void PlaySoundWalk()
    {/*
        _audioSource.volume = 1f;
        _audioSource.clip = _walk;
        _audioSource.loop = true;
        _audioSource.Play();*/
    }

    public void InitializeWalk()
    {
        _audioSourceWalk.volume = 0f;
        _audioSourceWalk.clip = _walk;
        _audioSourceWalk.loop = true;
        _audioSourceWalk.Play();
    }

    public void InitializeRun()
    {
        _audioSourceRun.volume = 0f;
        _audioSourceRun.clip = _run;
        _audioSourceRun.loop = true;
        _audioSourceRun.Play();
    }

    public void InitializeAgitated()
    {
        _audioSourceAgitated.volume = 0f;
        _audioSourceAgitated.clip = _agitated;
        _audioSourceAgitated.loop = true;
        _audioSourceAgitated.Play();
    }



  /*  public void PlaySoundAttack()
    {
        _audioSource.volume = 1f;
    }
*/
   /* public void FadeOutSound(AudioSource _audioSource)
    {
        if (!_audioSource.isPlaying || _isFadingOut)
            return;

        StartCoroutine(FadeOut(_audioSource));
    }

    private IEnumerator FadeOut(AudioSource _audioSource)
    {
        float startVolume = _audioSource.volume;

        while (_audioSource.volume > 0)
        {
            _audioSource.volume -= startVolume * Time.deltaTime / fadeOutTime;
            yield return null;
        }

        _audioSource.Stop();
        _isFadingOut = false;
    }

    public void FadeOutAgitatedSound(AudioSource _audioSource)
    {
        if (!_audioSource.isPlaying || _isFadingOutAgitated)
            return;

        StartCoroutine(FadeOutAgitated(_audioSource));
    }

    private IEnumerator FadeOutAgitated(AudioSource _audioSource)
    {
        _isFadingOutAgitated = true;
        float startVolume = _audioSource.volume;

        while (_audioSource.volume > 0)
        {
            _audioSource.volume -= startVolume * Time.deltaTime / fadeOutTime;
            yield return null;
        }

        _audioSource.Stop();
        _isFadingOutAgitated = false;
    }*/
}


