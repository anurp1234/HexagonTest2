using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISoundEventListener 
{
    public void PlayBkgMusic();
    public void PauseBkgMusic();
    public void PlayFootStepsSound();
    public void StopFootStepsSound();
    public void PlaySFX(SFXType sfxType);
    
}
