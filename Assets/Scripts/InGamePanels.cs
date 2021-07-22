
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//using admob;

public class InGamePanels : MonoBehaviour
{
    private GameObject[] menus;
    public static InGamePanels instance;
    public GameObject x4DdButton;
    public Text diamondText;
    string LevelNo; //For Flurry
    public Transform noVideoPanel;

    public void Awake()
    {
        if (instance == null)
            instance = this;
    }
    
    void Start()
    {
        LevelNo = SceneManager.GetActiveScene().name;
        menus = new GameObject[transform.childCount];

        //Fill the array with  panels
        for (int i = 0; i < transform.childCount; i++)
            menus[i] = transform.GetChild(i).gameObject;

        // We toggle off their renderer
        foreach (GameObject go in menus)
        {
            go.gameObject.SetActive(false);
        }

        if (PlayerPrefs.HasKey("SpinAtLevelComplete"))
        {
            PlayerPrefs.DeleteKey("SpinAtLevelComplete");
        }
        if (PlayerPrefs.HasKey("SpinAtLevelFailed"))
        {
            PlayerPrefs.DeleteKey("SpinAtLevelFailed");
        }
        /*
        Admob.Instance().initSDK(new AdProperties());
        Admob.Instance().loadRewardedVideo("ca-app-pub-1147416347412616/5655307978"); //testvideo ad (ca-app-pub-3940256099942544/5224354917) // realvideo ad(ca-app-pub-1147416347412616/5655307978)
        if (SaveManager.Instance.state.isvipMember==false)
        { 
        Admob.Instance().loadInterstitial("ca-app-pub-1147416347412616/4870765373"); //testinter ad (ca-app-pub-3940256099942544/1033173712) // realinter ad(ca-app-pub-1147416347412616/4870765373)
        }*/
    }
    private void Update()
    {
        UpdateDiamondText();

        // if (!Admob.Instance().isInterstitialReady()) { Admob.Instance().loadInterstitial("ca-app-pub-3940256099942544/1033173712"); 
        /*if (!Admob.Instance().isInterstitialReady()&& SaveManager.Instance.state.isvipMember == false)
        {
            Admob.Instance().loadInterstitial("ca-app-pub-3940256099942544/1033173712");
        }*/

    }

    public void TogglePauseMenu()
    {
        SoundManager.PlaySound(SoundManager.Sound.Button_Pop);
        //Debug.Log("Sound button is played at TogglePauseMenu");
        for (int i = 0; i <= 3; i++)
        {
            menus[i].SetActive(false);
        }
        menus[0].SetActive(true);
    }
    public void ToggleLevelComplete()
    {
        PlayerPrefs.SetInt("SpinAtLevelComplete", 1);
        if (PlayerPrefs.HasKey("4xButton"))
        {
            x4DdButton.SetActive(true);
            PlayerPrefs.DeleteKey("4xButton");
        }

        for (int i = 0; i <= 3; i++)
        {
            menus[i].SetActive(false);
        }
        menus[1].SetActive(true);
    }

    public void ToggleLevelFailed()
    {
        PlayerPrefs.SetInt("SpinAtLevelFailed", 1);
        for (int i = 0; i <= 3; i++)
        {
            menus[i].SetActive(false);
        }
        menus[2].SetActive(true);
    }

    public void TogglePanelOff()
    {
        SoundManager.PlaySound(SoundManager.Sound.Button_Pop);
        //Debug.Log("Sound button is played at TogglePanelOff");
        for (int i = 0; i <= 3; i++)
        {
            menus[i].SetActive(false);
        }
    }


    //Buttons On Panel
    public void NextLevel()
    {
        int r = Random.Range(0, 2);
        if (Yodo1Ads.instance.isInterstitialReady() && r == 1 && SaveManager.Instance.state.isvipMember == false)
        {
            Yodo1Ads.instance.showInterstitialAd();
            //Flurry Event
            EventLogExample.Instance.InterstitialShown(LevelNo);
        }
        SoundManager.PlaySound(SoundManager.Sound.Button_Pop);
        //Debug.Log("Sound button is played at NextLevel");
        SaveManager.Instance.CompleteLevel(Manager.Insatance.currentLevel);
        Manager.Insatance.currentLevel += 1;
        Initiate.Fade(SceneManager.GetActiveScene().buildIndex + 1, GameAssets.i.color, 1f);
    }

    public void ReplayLevel()
    {
        Time.timeScale = 1;
        int r = Random.Range(0, 2);
            //Yodo1Ads.instance.showInterstitialAd();
        
        if (Yodo1Ads.instance.isInterstitialReady() && r == 1 && SaveManager.Instance.state.isvipMember == false)
        {
            Yodo1Ads.instance.showInterstitialAd();
            //Flurry Event
            EventLogExample.Instance.InterstitialShown(LevelNo);
        }
        
        //Flurry Event
       // EventLogExample.Instance.PressedLevelReplay(LevelNo);

        SoundManager.PlaySound(SoundManager.Sound.Button_Pop);
        //Debug.Log("Sound button is played at ReplayLevel");
        Initiate.Fade(SceneManager.GetActiveScene().buildIndex, GameAssets.i.color, 1f);
    }

    public void BackToMain()
    {
        Time.timeScale = 1;
        int r = Random.Range(0, 2);
        if (Yodo1Ads.instance.isInterstitialReady() && r == 1 && SaveManager.Instance.state.isvipMember == false)
        {
            Yodo1Ads.instance.showInterstitialAd();
            //Flurry Event
            EventLogExample.Instance.InterstitialShown(LevelNo);
        }
        SoundManager.PlaySound(SoundManager.Sound.Button_Pop);
        //Debug.Log("Sound button is played at BackToMain");
        Initiate.Fade(1, GameAssets.i.color, 1f);
    }
    public void ToggleSpinAndWin()
    {
        for (int i = 0; i <= 3; i++)
        {
            menus[i].SetActive(false);
        }
        menus[3].SetActive(true);
    }
    ///////////////////////////////////////

    public void CheatButton()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // todo allow for more than 2 levels
    }
    public void BackButtonSpinandWin()
    {
        if (PlayerPrefs.HasKey("SpinAtLevelComplete"))
        {
            ToggleLevelComplete();
            // PlayerPrefs.DeleteKey("SpinAtLevelComplete");
        }
        else if (PlayerPrefs.HasKey("SpinAtLevelFailed"))
        {
            ToggleLevelFailed();
            // PlayerPrefs.DeleteKey("SpinAtLevelFailed");
        }
    }
    //////////////////////////////////Pause Panel//////////////////////////////////////////
    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }
    public void x4DiamondButton()
    {
        //Flurry Event
        EventLogExample.Instance.PressedX4DiamondReward(LevelNo);
        PlayerPrefs.SetInt("DIAMONDX4", 1);
        
        if (Yodo1Ads.instance.isRewardedReady())
        {
            Yodo1Ads.instance.showRewardedAd();
            EventLogExample.Instance.VideoShown(LevelNo, "x4DiamondButton");
            
        }
        else 
        {
            noVideoPanel.gameObject.SetActive(true);
        }
        
    } 
    /*
    void onRewardedVideoEvent()
     {
            
                if (PlayerPrefs.HasKey("x4DiamondButton"))
                {
                    SaveManager.Instance.state.diamond += 4;
                    SaveManager.Instance.Save();
                    PlayerPrefs.DeleteKey("x4DiamondButton");

                    //Flurry Event
                    EventLogExample.Instance.RewardForVideo("x4DiamondButton");
                }
                EventLogExample.Instance.VideoShown(LevelNo, "x4DiamondButton");
            
    }
    */
    ///////////
    /// ///
    private bool effect = true;
    public void CollisionKey()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<CapsuleCollider>().enabled = effect;
        effect = !effect;
    }
    private void UpdateDiamondText()
    {
        diamondText.text = SaveManager.Instance.state.diamond.ToString();
    }
    IEnumerator RewardCo()
    {
        yield return new WaitForSeconds(0.5f);
        //onRewardedVideoEvent();
    }
    
}
