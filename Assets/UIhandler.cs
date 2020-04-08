using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIhandler : MonoBehaviour
{
    public GameObject CompleteLevelDialog;
    public GameObject FailedLevelDialog;
    public static UIhandler instance;

    public void Awake()
    {
        if (instance == null)
            instance = this;
    }
    public void ShowCompleteLevelDialog ()
    {
        CompleteLevelDialog.SetActive(true);
    }
    public void ShowFailedLevelDialog()
    {
        FailedLevelDialog.SetActive(true);
    }
    public void BackToMain()
    {
        SceneManager.LoadScene(0);
    }
    public void ReplayLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
}
