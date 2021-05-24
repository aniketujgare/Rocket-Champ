using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    // public Toggle music;
    //  public Toggle sfx;
    GameObject music;
    GameObject sfx;
    GameObject haptic;
    private void Awake()
    {
        //SaveManager.Instance.state.completedLevel = 57;
            music = GameObject.Find("Music");
            sfx = GameObject.Find("SFX");
            haptic = GameObject.Find("Haptic");
        //Music
        int i = PlayerPrefs.GetInt("Music", 1);
        if(i ==1)
        {
            MusicToggle(true);
        }
        else
        {
            MusicToggle(false);
        }

        // SFX
        int j = PlayerPrefs.GetInt("SFX", 1);
        if (j == 1)
        {
            SfxToggle(true);
        }
        else
        {
            SfxToggle(false);
        }

        // Haptic
        int k = PlayerPrefs.GetInt("Haptic", 1);
        if (k == 1)
        {
            PlayerPrefs.SetInt("Haptic", 1);
            haptic.GetComponent<Toggle>().isOn = true;
            //Debug.Log("Haptic set true");
        }
        else
        {
            PlayerPrefs.SetInt("Haptic", 0);
            haptic.GetComponent<Toggle>().isOn = false;
            //Debug.Log("Haptic set false");
        }
    }
    public void MusicToggle (bool value)
    {
       // Debug.Log("music toggle pressed");
        if (value == true)
        {
            GameAssets.i.MusicOn();
            PlayerPrefs.SetInt("Music", 1);
            //music.isOn = true;
            music.GetComponent<Toggle>().isOn = true;
        }
        else if (value == false)
        {
            GameAssets.i.MusicOff();
            PlayerPrefs.SetInt("Music", 0);
            // music.isOn = false;
            music.GetComponent<Toggle>().isOn = false;

        }
    }
    public void SfxToggle(bool value)
    {
        if (value == true)
        {
            GameAssets.i.sfxOn();
            PlayerPrefs.SetInt("SFX", 1);
            //sfx.isOn = true;
            sfx.GetComponent<Toggle>().isOn = true;

        }
        else if (value == false)
        {
            GameAssets.i.sfxOff();
            PlayerPrefs.SetInt("SFX", 0);
            // sfx.isOn = false;
            sfx.GetComponent<Toggle>().isOn = false;

        }
    }
    public void HapticToggle(bool value)
    {
        if (value == true)
        {
            PlayerPrefs.SetInt("Haptic",1);
            haptic.GetComponent<Toggle>().isOn = true;
           // Debug.Log("Haptic set true");

        }
        else if (value == false)
        {
            PlayerPrefs.SetInt("Haptic", 0);
            haptic.GetComponent<Toggle>().isOn = false;
           // Debug.Log("Haptic set false");


        }


    }
}
