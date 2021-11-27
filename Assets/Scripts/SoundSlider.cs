using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;
public class SoundSlider : MonoBehaviour
{
    public TextMeshProUGUI text;
    public Slider slider;
    public AudioMixerGroup mixerGroup;
    public string volumeFloatName;
    // Start is called before the first frame update
    void Start()
    {
        slider.onValueChanged.AddListener(UpdateValue);
        if (PlayerPrefs.HasKey(volumeFloatName))
        {
            mixerGroup.audioMixer.SetFloat(volumeFloatName, PlayerPrefs.GetFloat(volumeFloatName));
        }
        mixerGroup.audioMixer.GetFloat(volumeFloatName, out float v);
        float vol = (v * 1.25f) + 100;
        slider.value = vol;
        text.SetText(Mathf.RoundToInt(vol).ToString() + "%");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateValue(float val)
    {
        float vol = (val - 100) * .8f;
        text.SetText(Mathf.RoundToInt(val).ToString() + "%");
        mixerGroup.audioMixer.SetFloat(volumeFloatName, vol);
        PlayerPrefs.SetFloat(volumeFloatName, vol);
    }

    public float GetSliderValue()
    {
        mixerGroup.audioMixer.GetFloat(volumeFloatName, out float v);
        return (v * 1.25f) + 100;
    }
}
