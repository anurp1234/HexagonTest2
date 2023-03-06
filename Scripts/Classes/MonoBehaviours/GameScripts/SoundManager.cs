using System.Collections.Generic;
using UnityEngine;

public enum SFXType
{
    COLLECT,
    PLAYERHIT,
    CELEBRATION
}

[System.Serializable]
public struct SFXInfo
{
    public SFXType sfxType;
    public AudioClip audioClip;
}
public class SoundManager : MonoBehaviour, ISoundEventListener
{
    [SerializeField]
    SoundEvents soundEvents;

    [SerializeField]
    List<SFXInfo> sfxMapping;

    [SerializeField]
    AudioSource bkgMusicChannel;

    [SerializeField]
    AudioSource stepsSoundChannel;

    [SerializeField]
    List<AudioSource> sfxChannels;

    [SerializeField]
    List<AudioClip> footStepSounds;

    [SerializeField]
    AudioClip buttonClickSnd;

    bool isFootStepSoundPlaying = false;
    int currrentFootStepSoundsIdx = 0;
    int totalFootStepsCount;
    float currentClipLength;

    void Start()
    {
        bkgMusicChannel.loop = true;
        if (footStepSounds != null)
        {
            totalFootStepsCount = footStepSounds.Count;
        }
        PlayBkgMusic();
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if (isFootStepSoundPlaying)
        {
            if (Mathf.Abs(stepsSoundChannel.time - currentClipLength) < 0.1)
            {
                currrentFootStepSoundsIdx = currrentFootStepSoundsIdx % totalFootStepsCount;
                AudioClip audioClip = footStepSounds[currrentFootStepSoundsIdx];
                currrentFootStepSoundsIdx++;
                stepsSoundChannel.clip = audioClip;
                currentClipLength = audioClip.length;
                stepsSoundChannel.Play();
            }
        }
    }

    public void PlayBkgMusic()
    {
        bkgMusicChannel.Play();
    }

    public void PauseBkgMusic()
    {
        bkgMusicChannel.Pause();
    }

    public void PlayFootStepsSound()
    {
        isFootStepSoundPlaying = true;
        stepsSoundChannel.Play();
        currentClipLength = stepsSoundChannel.clip.length;
    }

    public void StopFootStepsSound()
    {
        isFootStepSoundPlaying = false;
        stepsSoundChannel.Stop();
    }

    public void PlayButtonClickSound()
    {
        AudioSource audioSource = GetFreeAudioSource();
        Debug.Assert(audioSource != null, "No audiosource is free");
        audioSource.clip = buttonClickSnd;
        audioSource.Play();

    }

    public void PlaySFX(SFXType sfxType)
    {
        AudioClip clip = GetAudioClip(sfxType);
        Debug.Assert(clip != null, "No audioclip for type " + sfxType.ToString());
        AudioSource audioSource = GetFreeAudioSource();
        Debug.Assert(audioSource != null, "No audiosource is free");
        audioSource.clip = clip;
        audioSource.Play();
    }

    public void OnEnable()
    {
        soundEvents.RegisterEventListener(this);
    }

    public void OnDisable()
    {
        soundEvents.UnRegisterEventListener(this);
    }

    private AudioSource GetFreeAudioSource()
    {
        foreach (AudioSource source in sfxChannels)
        {
            if (source.isPlaying == false)
                return source;
        }
        return null;
    }

     AudioClip GetAudioClip(SFXType sfxType)
    {
        foreach (SFXInfo mapping in sfxMapping)
        {
            if (mapping.sfxType == sfxType)
                return mapping.audioClip;
        }
        return null;
    }


   
}
