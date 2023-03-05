using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum SFXType
{
    COLLECT
}

[System.Serializable]
public struct SFXInfo{

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
    }

    void Update()
    {
        //Debug.Log("Soundmanager update");
        if (isFootStepSoundPlaying)
        {
            //Debug.Log("Soundmanager update stepsSoundChannel.time"+ stepsSoundChannel.time + "currentClipLength = "+ currentClipLength);
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
        //Debug.Log("Play foot step sound");
        isFootStepSoundPlaying = true;
        stepsSoundChannel.Play();
        currentClipLength = stepsSoundChannel.clip.length;
    }

    public void StopFootStepsSound()
    {
        //Debug.Log("Stop foot step sound");
        isFootStepSoundPlaying = false;
        stepsSoundChannel.Stop();
    }

    public void PlaySFX(SFXType sfxType)
    { 

    }

    public void OnEnable()
    {
        soundEvents.RegisterEventListener(this);
    }

    public void OnDisable()
    {
        soundEvents.UnRegisterEventListener(this);
    }
}
