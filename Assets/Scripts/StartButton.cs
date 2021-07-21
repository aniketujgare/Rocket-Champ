using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Button b = GetComponent<Button>();
        b.onClick.AddListener(() => OnStart());
    }
    public void OnStart()
    { 
        SoundManager.PlaySound(SoundManager.Sound.Button_Pop);
        Debug.Log("Sound button is played at OnStart");
        Initiate.Fade(1, GameAssets.i.color, 5f) ;
        //SceneManager.LoadScene("Menu");

    }
}