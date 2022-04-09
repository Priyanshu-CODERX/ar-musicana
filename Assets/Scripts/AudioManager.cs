using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] Slider volumeRocker;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("Volume"))
        {
            PlayerPrefs.SetFloat("Volume", 1);
            LoadVolumeLevel();
        }
        else
        {
            LoadVolumeLevel();
        }
    }

    public void ChangeVolume()
    {
        AudioListener.volume = volumeRocker.value;
        SaveVolumeLevel();
    }

    void LoadVolumeLevel()
    {
        volumeRocker.value = PlayerPrefs.GetFloat("Volume");
    }

    void SaveVolumeLevel()
    {
        PlayerPrefs.SetFloat("Volume" ,volumeRocker.value);
    }

}
