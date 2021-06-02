using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;
public class SoundSlider : VRSlider.ValueProvider
{
    public TextMeshProUGUI text;
    public VRSlider slider;
    public AudioMixerGroup mixerGroup;
    public string volumeFloatName;
    // Start is called before the first frame update
    void Start()
    {
        slider.onValueChange.AddListener(UpdateValue);
        if (PlayerPrefs.HasKey(volumeFloatName))
        {
            mixerGroup.audioMixer.SetFloat(volumeFloatName, PlayerPrefs.GetFloat(volumeFloatName));
        }
        mixerGroup.audioMixer.GetFloat(volumeFloatName, out float v);
        text.SetText(Mathf.RoundToInt(v + 80).ToString() + "%");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateValue(float val)
    {
        float vol = (val * 100) - 80;
        text.SetText(Mathf.RoundToInt(val * 100).ToString() + "%");
        mixerGroup.audioMixer.SetFloat(volumeFloatName, vol);
        PlayerPrefs.SetFloat(volumeFloatName, vol);
    }

    public override float GetSliderValue()
    {
        mixerGroup.audioMixer.GetFloat(volumeFloatName, out float v);
        return (v + 80) / 100;
    }
}
