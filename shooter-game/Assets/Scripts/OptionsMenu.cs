using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour
{
    public AudioMixer mainAudioMixer;

    public void HandleVolume(float volume)
    {
        mainAudioMixer.SetFloat("Volume", volume);
    }
}
