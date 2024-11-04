using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [Header("Audio Sources")]
    [SerializeField] AudioSource bgMusicAudioSource;
    [SerializeField] AudioSource sfxAudioSource;
    [Header("Audio Clips")]
    [SerializeField] AudioClip bgMusicAudioClip;
    [Space]

    [SerializeField] AudioClip catapultShootAudioClip;
    [SerializeField] AudioClip projectileHitAudioClip;
    [Space]
    [SerializeField] AudioClip cannonShootAudioClip;
    [SerializeField] AudioClip enemyProjectileHitAudioClip;
    [SerializeField] AudioClip enemyDeadAudioClip;
    [Space]
    [SerializeField] AudioClip successAudioClip;
    [SerializeField] AudioClip failureAudioClip;
    [SerializeField] AudioClip uiButtonAudioClip;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }

    public void PlaySuccessSound()
    {
        PlaySFXAudio(successAudioClip);
    }

    public void PlayFailureSound()
    {
        PlaySFXAudio(failureAudioClip);
    }
    public void PlayCatapultShootSound()
    {
        PlaySFXAudio(catapultShootAudioClip);
    }
    public void PlayProjectileHitSound()
    {
        PlaySFXAudio(projectileHitAudioClip);
    }

    public void PlayCannonShootSound()
    {
        PlaySFXAudio(cannonShootAudioClip);
    }
    public void PlayEnemyProjectileHitSound()
    {
        PlaySFXAudio(enemyProjectileHitAudioClip);
    }
    public void PlayEnemyDeadSound()
    {
        PlaySFXAudio(enemyDeadAudioClip);
    }

    public void PlayUIButtonSound()
    {
        PlaySFXAudio(uiButtonAudioClip);
    }

    private void PlaySFXAudio(AudioClip audioClip)
    {
        sfxAudioSource.PlayOneShot(audioClip);
    }
}
