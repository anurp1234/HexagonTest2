using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The scriptable object here acts as a mechanism for decoupling events and the event responses
[CreateAssetMenu]
public class SoundEvents : ScriptableObject
{
    private List<ISoundEventListener> soundEventListeners = new List<ISoundEventListener>();

    public void RaisePlayBkgMusicEvent()
    {
        for (int i = soundEventListeners.Count - 1; i >= 0; i--)
        {
            soundEventListeners[i].PlayBkgMusic();
        }
    }

    public void RaisePauseBkgMusicEvent()
    {
        for (int i = soundEventListeners.Count - 1; i >= 0; i--)
        {
            soundEventListeners[i].PauseBkgMusic();
        }
    }

    public void RaiseFootStepsEvent()
    {
        for (int i = soundEventListeners.Count - 1; i >= 0; i--)
        {
            soundEventListeners[i].PlayFootStepsSound();
        }
    }

    public void RaiseFootStepsStopEvent()
    {
        for (int i = soundEventListeners.Count - 1; i >= 0; i--)
        {
            soundEventListeners[i].StopFootStepsSound();
        }
    }

    public void RaiseSFXEvent(SFXType type)
    {
        for (int i = soundEventListeners.Count - 1; i >= 0; i--)
        {
            soundEventListeners[i].PlaySFX(type);
        }
    }

    public void RegisterEventListener(ISoundEventListener listener)
    {
        soundEventListeners.Add(listener);
    }
    public void UnRegisterEventListener(ISoundEventListener listener)
    {
        soundEventListeners.Remove(listener);
    }
}
