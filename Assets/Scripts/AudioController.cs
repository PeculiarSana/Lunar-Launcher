using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{

    public AudioSource[] audioSources;

    private void OnEnable()
    {
        EventManager.Fire += CannonSound;
        EventManager.Propulsion += SwitchSound;
        EventManager.Arming += SwitchSound;
    }

    void CannonSound()
    {
        audioSources[4].Play();
        audioSources[5].Play();
    }

    void SwitchSound(bool b)
    {
        if (b)
            audioSources[0].Play();
        else
            audioSources[2].Play();
    }
}
