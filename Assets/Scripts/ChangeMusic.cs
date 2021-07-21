using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeMusic : MonoBehaviour
{
    public AudioClip levelmusic;
    public AudioClip homemusic;
    private AudioSource source;

     void Awake()
    {
        DontDestroyOnLoad(gameObject);
        source = GetComponent<AudioSource>();
        source.clip = homemusic;
        source.Play();
    }

    void OnLevelWasLoaded(int level)
    {
        //Debug.Log("active scene index " + SceneManager.GetActiveScene().buildIndex);
        //Debug.Log("level index " + level);
       if(level>1 && source.clip != levelmusic)
        {
            source.clip = levelmusic;
            source.Play();
            return;
        }
         if (SceneManager.GetActiveScene().buildIndex <= 1)
        {
            if (source.clip == homemusic)
            {
            }
            else
            {
                source.clip = homemusic;
                source.Play();
            }        
        }
    }
}

