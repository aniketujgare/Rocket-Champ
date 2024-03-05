using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yodo1.MAS;

public class Yodo1Ads : MonoBehaviour
{
    public static Yodo1Ads instance = null;
    bool isBannerShown = false;
    Yodo1U3dBannerAdView bannerAdView = null;
    private int retryAttempt = 0;
    // Start is called before the first frame update
    void Awake()
    {
        // Singleton Class
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        Yodo1U3dMas.InitializeMasSdk();
        bannerAdView = new Yodo1U3dBannerAdView(Yodo1U3dBannerAdSize.Banner, Yodo1U3dBannerAdPosition.BannerTop | Yodo1U3dBannerAdPosition.BannerHorizontalCenter);
        // LoadBannerAd();

        SetupEventCallbacks();
      //  LoadInterstitialAd();

        SetupEventCallbacksReward();
       // LoadRewardAd();
    }


    private void LoadBannerAd()
    {
   
        bannerAdView.LoadAd();
    }
    // Banner

  
    public void showBannerAd()
    {
        bannerAdView.Show();
    }
    public void dismissBanner()
    {
        bannerAdView.Destroy();
        
       
    }

    // Interstital
    private void SetupEventCallbacks()
    {
        Yodo1U3dInterstitialAd.GetInstance().OnAdLoadedEvent += OnInterstitialAdLoadedEvent;
        Yodo1U3dInterstitialAd.GetInstance().OnAdLoadFailedEvent += OnInterstitialAdLoadFailedEvent;
        Yodo1U3dInterstitialAd.GetInstance().OnAdOpenedEvent += OnInterstitialAdOpenedEvent;
        Yodo1U3dInterstitialAd.GetInstance().OnAdOpenFailedEvent += OnInterstitialAdOpenFailedEvent;
        Yodo1U3dInterstitialAd.GetInstance().OnAdClosedEvent += OnInterstitialAdClosedEvent;
    }

    private void LoadInterstitialAd()
    {
        Yodo1U3dInterstitialAd.GetInstance().LoadAd();
    }

    private void OnInterstitialAdLoadedEvent(Yodo1U3dInterstitialAd ad)
    {
        // Code to be executed when an ad finishes loading.
        retryAttempt = 0;
        Yodo1U3dInterstitialAd.GetInstance().ShowAd();
    }

    private void OnInterstitialAdLoadFailedEvent(Yodo1U3dInterstitialAd ad, Yodo1U3dAdError adError)
    {
        // Code to be executed when an ad request fails.
        retryAttempt++;
        double retryDelay = System.Math.Pow(2, Math.Min(6, retryAttempt));
        Invoke("LoadInterstitialAd", (float)retryDelay);
    }

    private void OnInterstitialAdOpenedEvent(Yodo1U3dInterstitialAd ad)
    {
        // Code to be executed when an ad opened
    }

    private void OnInterstitialAdOpenFailedEvent(Yodo1U3dInterstitialAd ad, Yodo1U3dAdError adError)
    {
        // Code to be executed when an ad open fails.
        LoadInterstitialAd();
    }

    private void OnInterstitialAdClosedEvent(Yodo1U3dInterstitialAd ad)
    {
        // Code to be executed when the ad closed
        LoadInterstitialAd();
    }
    public bool isInterstitialReady()
    {
        bool isLoaded = Yodo1U3dInterstitialAd.GetInstance().IsLoaded();
        return isLoaded;
    }

    public void showInterstitialAd()
    {
        if (isInterstitialReady())
        {
            Yodo1U3dInterstitialAd.GetInstance().ShowAd();
           
        }
        else
        {
            Debug.Log("[Yodo1 Mas] Interstitial ad has not been cached.");
        }
    }

    // Video ad
    private void SetupEventCallbacksReward()
    {
        Yodo1U3dRewardAd.GetInstance().OnAdLoadedEvent += OnRewardAdLoadedEvent;
        Yodo1U3dRewardAd.GetInstance().OnAdLoadFailedEvent += OnRewardAdLoadFailedEvent;
        Yodo1U3dRewardAd.GetInstance().OnAdOpenedEvent += OnRewardAdOpenedEvent;
        Yodo1U3dRewardAd.GetInstance().OnAdOpenFailedEvent += OnRewardAdOpenFailedEvent;
        Yodo1U3dRewardAd.GetInstance().OnAdClosedEvent += OnRewardAdClosedEvent;
        Yodo1U3dRewardAd.GetInstance().OnAdEarnedEvent += OnRewardAdEarnedEvent;
    }

    private void LoadRewardAd()
    {
        Yodo1U3dRewardAd.GetInstance().LoadAd();
    }

    private void OnRewardAdLoadedEvent(Yodo1U3dRewardAd ad)
    {
        // Code to be executed when an ad finishes loading.
        retryAttempt = 0;
        Yodo1U3dRewardAd.GetInstance().ShowAd();
    }

    private void OnRewardAdLoadFailedEvent(Yodo1U3dRewardAd ad, Yodo1U3dAdError adError)
    {
        // Code to be executed when an ad request fails.
        retryAttempt++;
        double retryDelay = Math.Pow(2, Math.Min(6, retryAttempt));
        Invoke("LoadRewardAd", (float)retryDelay);
    }

    private void OnRewardAdOpenedEvent(Yodo1U3dRewardAd ad)
    {
        // Code to be executed when an ad opened
    }

    private void OnRewardAdOpenFailedEvent(Yodo1U3dRewardAd ad, Yodo1U3dAdError adError)
    {
        // Code to be executed when an ad open fails.
        LoadRewardAd();
    }

    private void OnRewardAdClosedEvent(Yodo1U3dRewardAd ad)
    {
        // Code to be executed when the ad closed
        LoadRewardAd();
    }

    private void OnRewardAdEarnedEvent(Yodo1U3dRewardAd ad)
    {
        // Code executed when getting rewards
    }
    public bool isRewardedReady()
    {
        bool isLoaded = Yodo1U3dRewardAd.GetInstance().IsLoaded();
        return isLoaded;
    }

    public void showRewardedAd()
    {
        if (isRewardedReady())
        {
            Yodo1U3dRewardAd.GetInstance().ShowAd();
        }
        else
        {
            Debug.Log("[Yodo1 Mas] Reward video ad has not been cached.");
        }
    }
    private void Reward()
    {

        if (PlayerPrefs.HasKey("BONUSLEVEL"))
        {
            //Reward Bonus Level
            PlayerPrefs.DeleteKey("BONUSLEVEL");
            //Flurry Event
            EventLogExample.Instance.RewardForVideo("BONUS LEVEL");
        }
        else if (PlayerPrefs.HasKey("DIAMONDX8"))
        {
            SaveManager.Instance.state.diamond += 8;
            SaveManager.Instance.Save();
            PlayerPrefs.DeleteKey("DIAMONDX8");

            //Flurry Event
            EventLogExample.Instance.RewardForVideo("8 DIAMONDS");
        }
        else if (PlayerPrefs.HasKey("DIAMONDX4"))
        {
            SaveManager.Instance.state.diamond += 4;
            SaveManager.Instance.Save();
            PlayerPrefs.DeleteKey("DIAMONDX4");

            //Flurry Event
            EventLogExample.Instance.RewardForVideo("4 DIAMONDS");
        }
        else if (PlayerPrefs.HasKey("JACKPOT"))
        {
            SaveManager.Instance.state.diamond += 25;
            SaveManager.Instance.Save();
            PlayerPrefs.DeleteKey("JACKPOT");

            //Flurry Event
            EventLogExample.Instance.RewardForVideo("JACKPOT");
        }

    }


    // callbacks
    
   
}


