using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class VolumeControl : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;
    [SerializeField] Slider volumeSlider;
    [SerializeField] float multipliyer = 30f;
    // Start is called before the first frame update
       
    public void ControlVolume(float value)
    {
        mixer.SetFloat("MasterVolume", Mathf.Log10(value) * multipliyer);
    }
}
