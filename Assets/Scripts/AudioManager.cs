using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    private AudioSource sfxSource;       
    private AudioSource bgmSource;
    private AudioSource waljSource;

    public AudioClip interactClip;
    public AudioClip defaultBGM;
    public AudioClip walk;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject); 
            return;
        }

        AudioSource[] sources = GetComponents<AudioSource>();
        if (sources.Length < 2)
        {
            Debug.LogError("Se necesitan dos AudioSources en el AudioManager");
            return;
        }

        sfxSource = sources[0];  
        bgmSource = sources[1];  

        PlayBackgroundMusic(defaultBGM); 
    }

    public void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            sfxSource.PlayOneShot(clip); 
        }
    }

    public void PlayWalk()
    {
        waljSource.clip = walk;
        waljSource.Play();
        waljSource.loop = true;
    }

    public void StopWalk()
    {
        sfxSource.Stop();
        sfxSource.loop = false;
    }

    public void PlayBackgroundMusic(AudioClip newBGM)
    {
        if (bgmSource.isPlaying)
        {
            bgmSource.Stop(); 
        }

        if (newBGM != null)
        {
            bgmSource.clip = newBGM;    
            bgmSource.loop = true;
            bgmSource.volume = 0.1f;
            bgmSource.Play();           
        }
    }
}
