using System.Collections;
using UnityEngine;

public class EnemySFX : MonoBehaviour
{
    public AudioClip enemyAttack;
    public AudioClip enemyAttackClaws;
    public AudioClip enemyBreathChase;
    public AudioClip enemyBreathPatrol;
    public AudioClip enemyBreathSearch;
    public AudioClip enemyDetect;
    public AudioClip enemyGiveUp;
    public AudioClip enemyLost;
    public AudioClip enemyRun;
    public AudioClip enemySearch;
    public AudioClip enemyWalk;

    public AudioSource _audioSource1; // Audio source for movement sounds
    public AudioSource _audioSource2; // Audio source for agitated sounds
    public AudioSource _audioSource3;

    private float fadeOutTime = 2f; // Time it takes to fade out the sound
    private float fadeInTime = 2f; // Time it takes to fade in the sound

    private Coroutine currentBreathCoroutine;

    void Start()
    {
        // Initialization code goes here
    }

    public void PlaySoundEnemyWalk()
    {
        StartCoroutine(FadeIn(_audioSource3, enemyWalk, 1f, true));
    }

    public void PlaySoundEnemyRun()
    {
        StartCoroutine(FadeIn(_audioSource3, enemyRun, 1f, true));
    }

    public void PlaySoundGiveUp()
    {
        _audioSource1.volume = 1f;
        _audioSource1.PlayOneShot(enemyGiveUp);
    }

    public void PlaySoundDetect()
    {
        _audioSource2.volume = 1f;
        _audioSource2.PlayOneShot(enemyDetect);
    }

    public void PlaySoundBreathSearch()
    {
        CrossfadeBreathSound(enemyBreathSearch);
    }

    public void PlaySoundBreathPatrol()
    {
        CrossfadeBreathSound(enemyBreathPatrol);
    }

    public void PlaySoundBreathChase()
    {
        CrossfadeBreathSound(enemyBreathChase);
    }

    public void PlaySoundAttack()
    {
        StartCoroutine(FadeIn(_audioSource2, enemyAttack, 1f, false));
    }

    public void PlaySoundAttackClaws()
    {
        StartCoroutine(FadeIn(_audioSource2, enemyAttackClaws, 1f, false));
    }

    public void StopAudioSource1()
    {
        StartCoroutine(FadeOut(_audioSource1));
    }

    public void StopAudioSource2()
    {
        StartCoroutine(FadeOut(_audioSource2));
    }

    public void StopAudioSource3()
    {
        StartCoroutine(FadeOut(_audioSource3));
    }

    private void CrossfadeBreathSound(AudioClip newClip)
    {
        if (currentBreathCoroutine != null)
        {
            StopCoroutine(currentBreathCoroutine);
        }
        currentBreathCoroutine = StartCoroutine(CrossfadeCoroutine(_audioSource1, newClip, 1f, true));
    }

    private IEnumerator CrossfadeCoroutine(AudioSource audioSource, AudioClip newClip, float targetVolume, bool loop)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / fadeOutTime;
            yield return null;
        }

        audioSource.Stop();
        audioSource.clip = newClip;
        audioSource.volume = 0;
        audioSource.loop = loop;
        audioSource.Play();

        while (audioSource.volume < targetVolume)
        {
            audioSource.volume += Time.deltaTime / fadeInTime;
            yield return null;
        }

        audioSource.volume = targetVolume;
    }

    private IEnumerator FadeIn(AudioSource audioSource, AudioClip clip, float targetVolume, bool loop)
    {
        audioSource.clip = clip;
        audioSource.volume = 0;
        audioSource.loop = loop;
        audioSource.Play();

        while (audioSource.volume < targetVolume)
        {
            audioSource.volume += Time.deltaTime / fadeInTime;
            yield return null;
        }

        audioSource.volume = targetVolume;
    }

    private IEnumerator FadeOut(AudioSource audioSource)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / fadeOutTime;
            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
    }
}