using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeController : MonoBehaviour
{
    [SerializeField] private List<GameObject> soundValues = new List<GameObject>();
    [SerializeField] private AudioMixer mixer;

    private int currentVol = 4;
    private int minVolume = -80;
    private int maxVolume = 0;
    private void Start()
    {
        foreach (var s in soundValues)
        {
            s.SetActive(false);
        }
        
        currentVol = PlayerPrefs.GetInt("MasterVolume", 4);
        

        for (int i = 0; i <= currentVol; ++i)
        {
            soundValues[i].SetActive(true);
        }
        float mappedVolume = Mathf.Lerp(minVolume, maxVolume, currentVol / 4f);
        mixer.SetFloat("MasterVolume", mappedVolume*2);
        currentVol++;
    }

    public void OnClick()
    {
        if (currentVol == soundValues.Count)
        {
            foreach (var s in soundValues)
            {
                s.SetActive(false);
            }

            currentVol = 0;
            PlayerPrefs.SetInt("MasterVolume",currentVol);
            float Volume = -80f;
            mixer.SetFloat("MasterVolume", Volume);
        }
        else
        {
            soundValues[currentVol].SetActive(true);
            PlayerPrefs.SetInt("MasterVolume", currentVol);
            float mappedVolume = currentVol==0? MapVolume(0.5f) : MapVolume(currentVol);
            mixer.SetFloat("MasterVolume", mappedVolume);
            currentVol++;
        }
    }
    
    private float MapVolume(float volumeSetting)
    {
        return volumeSetting * 10f - 40f;
        //return Mathf.Lerp(minVolume, maxVolume, volumeSetting / 4.0f);
    }
}
