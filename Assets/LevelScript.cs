using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        disableAll();
        // PlayerPrefs.DeLeteAll ();
        if (!PlayerPrefs.HasKey("LoadNextLevelCount"))
            PlayerPrefs.SetInt("LoadNextLevelCount", 0);
        int clearedLevel = PlayerPrefs.GetInt("LoadNextLevelCount");
        for(int i=0; i< clearedLevel+1; ++i)
        {
            transform.GetChild(i).gameObject.GetComponent<Button>().interactable = true;
        }
    

    }
    public void disableAll()
    {
        int levelBUttonCount = transform.childCount;
        for (int i = 0; i < levelBUttonCount; ++i)
        {
            transform.GetChild(i).GetComponent<Button>().interactable = false;
        }
    }

    public void playLevel(int level=0)
    {
        SceneManager.LoadScene(level);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
