using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yodo1.MAS;

public class Yodo1Ads : MonoBehaviour
{
    public static Yodo1Ads instance = null;
    bool isBannerShown = false;
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
        Yodo1U3dMas.InitializeSdk();

    }
    // Banner

    public bool isBannerReady()
    {
        bool isLoaded = Yodo1U3dMas.IsBannerAdLoaded();
        return isLoaded;
    }

    public void showBannerAd()
    {
        if (Yodo1U3dMas.IsBannerAdLoaded())
        {
            int align = Yodo1U3dBannerAlign.BannerTop | Yodo1U3dBannerAlign.BannerHorizontalCenter;
            Yodo1U3dMas.ShowBannerAd(align);
            SetDelegates();
        }
        else
        {
            Debug.Log("[Yodo1 Mas] Banner ad has not been cached.");
        }
    }
    public void dismissBanner()
    {
        Yodo1U3dMas.DismissBannerAd();
        SetDelegates();
    }

    // Interstital

    public bool isInterstitialReady()
    {
        bool isLoaded = Yodo1U3dMas.IsInterstitialAdLoaded();
        return isLoaded;
    }

    public void showInterstitialAd()
    {
        if (Yodo1U3dMas.IsInterstitialAdLoaded())
        {
            Yodo1U3dMas.ShowInterstitialAd();
            SetDelegates();
        }
        else
        {
            Debug.Log("[Yodo1 Mas] Interstitial ad has not been cached.");
        }
    }

    // Video ad

    public bool isRewardedReady()
    {
        bool isLoaded = Yodo1U3dMas.IsRewardedAdLoaded();
        return isLoaded;
    }

    public void showRewardedAd()
    {
        if (Yodo1U3dMas.IsRewardedAdLoaded())
        {
            Yodo1U3dMas.ShowRewardedAd();
            SetDelegates();
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
    private void SetDelegates()
    {
        Yodo1U3dMas.SetInitializeDelegate((bool success, Yodo1U3dAdError error) =>
        {
            Debug.Log("[Yodo1 Mas] InitializeDelegate, success:" + success + ", error: \n" + error.ToString());

            if (success)
            {
                StartCoroutine(BannerCoroutine());
            }
            else
            {

            }
        });

        Yodo1U3dMas.SetBannerAdDelegate((Yodo1U3dAdEvent adEvent, Yodo1U3dAdError error) =>
        {
            Debug.Log("[Yodo1 Mas] BannerdDelegate:" + adEvent.ToString() + "\n" + error.ToString());
            switch (adEvent)
            {
                case Yodo1U3dAdEvent.AdClosed:
                    Debug.Log("[Yodo1 Mas] Banner ad has been closed.");

                    break;
                case Yodo1U3dAdEvent.AdOpened:
                    Debug.Log("[Yodo1 Mas] Banner ad has been shown.");
                    break;
                case Yodo1U3dAdEvent.AdError:
                    Debug.Log("[Yodo1 Mas] Banner ad error, " + error.ToString());
                    break;
            }
        });

        Yodo1U3dMas.SetInterstitialAdDelegate((Yodo1U3dAdEvent adEvent, Yodo1U3dAdError error) =>
        {
            Debug.Log("[Yodo1 Mas] InterstitialAdDelegate:" + adEvent.ToString() + "\n" + error.ToString());
            switch (adEvent)
            {
                case Yodo1U3dAdEvent.AdClosed:
                    Debug.Log("[Yodo1 Mas] Interstital ad has been closed.");
                    break;
                case Yodo1U3dAdEvent.AdOpened:
                    Debug.Log("[Yodo1 Mas] Interstital ad has been shown.");
                    break;
                case Yodo1U3dAdEvent.AdError:
                    Debug.Log("[Yodo1 Mas] Interstital ad error, " + error.ToString());
                    break;
            }

        });

        Yodo1U3dMas.SetRewardedAdDelegate((Yodo1U3dAdEvent adEvent, Yodo1U3dAdError error) =>
        {
            Debug.Log("[Yodo1 Mas] RewardVideoDelegate:" + adEvent.ToString() + "\n" + error.ToString());
            switch (adEvent)
            {
                case Yodo1U3dAdEvent.AdClosed:
                    Debug.Log("[Yodo1 Mas] Reward video ad has been closed.");
                    break;
                case Yodo1U3dAdEvent.AdOpened:
                    Debug.Log("[Yodo1 Mas] Reward video ad has shown successful.");
                    break;
                case Yodo1U3dAdEvent.AdError:
                    Debug.Log("[Yodo1 Mas] Reward video ad error, " + error);
                    break;
                case Yodo1U3dAdEvent.AdReward:
                    Debug.Log("[Yodo1 Mas] Reward video ad reward, give rewards to the player.");
                    Debug.Log("Rewarded video ad has been played finish, give rewards to the player.");
                    Reward();
                    break;
            }

        });
    }

    IEnumerator BannerCoroutine()
    {
        yield return new WaitForSeconds(2.0f);
        if (isBannerShown == false)
        {
            if (Yodo1U3dMas.IsBannerAdLoaded())
            {
                Yodo1U3dMas.ShowBannerAd();
                isBannerShown = true;
            }
            else
            {
                StartCoroutine(BannerCoroutine());
            }
        }

    }
}


