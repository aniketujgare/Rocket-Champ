using System.Collections;
using System.Collections.Generic;
using UnityEngine;
///using admob;
using UnityEngine.SceneManagement;

public class AdManager : MonoBehaviour
{


    ///public Admob ad;
    string appID = "";
    string bannerID = "";
    string interstitialID = "";
    string videoID = "";
    string nativeBannerID = "";

    /*
    void Awake()
    {
        //Replace with your IDs here. These are test ids for you to check if the ads are implemented properly or not
#if UNITY_IOS
        		 appID="ca-app-pub-6533984286287209~3970651947";
				 bannerID="ca-app-pub-3940256099942544/6300978111";
				 interstitialID="ca-app-pub-3940256099942544/1033173712";
				 videoID="ca-app-pub-3940256099942544/5224354917";
				 nativeBannerID = "ca-app-pub-3940256099942544/2247696110";
#elif UNITY_ANDROID
        appID = "ca-app-pub-1147416347412616~8130084613";
        bannerID = "ca-app-pub-3940256099942544/6300978111";
        interstitialID = "ca-app-pub-1147416347412616/4870765373";
        videoID = "ca-app-pub-1147416347412616/5655307978";
        nativeBannerID = "ca-app-pub-3940256099942544/2247696110";
#endif
        ///AdProperties adProperties = new AdProperties();
        adProperties.isTesting(false);
        adProperties.isAppMuted(false);
        adProperties.isUnderAgeOfConsent(false);
        adProperties.appVolume(100);
        adProperties.maxAdContentRating(AdProperties.maxAdContentRating_G);
        string[] keywords = { "key1", "key2", "key3" };
        adProperties.keyworks(keywords);

        ad = Admob.Instance();
        ad.bannerEventHandler += onBannerEvent;
        ad.interstitialEventHandler += onInterstitialEvent;
        ad.rewardedVideoEventHandler += onRewardedVideoEvent;


        ad.initSDK(adProperties);//reqired,adProperties can been null

    }

    */

    void CacheVideoAd()
    {/*
        if (!ad.isRewardedVideoReady())
        {
            ad.loadRewardedVideo(videoID);
        }*/
    }

    public bool IsAdReady()
    {/*
#if UNITY_EDITOR
        return false;
#endif
        return ad.isRewardedVideoReady();
        */
        return Yodo1Ads.instance.isRewardedReady();
    }

    public void ShowInterstitial()
    {/*
        //To give a call back to this use the code below
        //AdManager.Instance.ShowInterstitial();
#if UNITY_ANDROID
        print("touch inst button -------------");
        try
        {
            if (ad.isInterstitialReady())
            {
                ad.showInterstitial();
                //Flurry Event
                //            EventLogExample.Instance.InterstitialShown();

            }
            else
            {
                ad.loadInterstitial(interstitialID);
            }
        }
        catch (System.Exception e)
        {
            print(e);
        }
#endif
        */
        Yodo1Ads.instance.showInterstitialAd();
    }
    public void ShowBanner()
    {
        //To give a call back to this use the code below
        //AdManager.Instance.ShowBanner();
        //Admob.Instance().showBannerRelative(bannerID, AdSize.SMART_BANNER, AdPosition.BOTTOM_CENTER);
        Yodo1Ads.instance.showBannerAd();
    }

    public void DestroyBanner()
    {
        //To give a call back to this use the code below
        //AdManager.Instance.DestroyBanner();
        Yodo1Ads.instance.dismissBanner();
    }

    public void ShowRewardedVideo()
    {
        //To give a call back to this use the code below
        //AdManager.Instance.ShowRewardedVideo();
        //Debug.Log("touch video button -------------");
        if (Yodo1Ads.instance.isRewardedReady())
        {
            // Flurry Event
            //  EventLogExample.Instance.VideoShown();
            Yodo1Ads.instance.showRewardedAd(); ;
        }
        else { }
        /*
        else
        {
            ad.loadRewardedVideo(videoID);
        }
        */
    }

    void onInterstitialEvent(string eventName, string msg)
    {/*
        //Debug.Log("handler onAdmobEvent---" + eventName + "   " + msg);
        if (eventName == AdmobEvent.onAdLoaded)
        {
            // Admob.Instance().showInterstitial();
        }*/
    }
    void onBannerEvent(string eventName, string msg)
    {
        //Debug.Log("handler onAdmobBannerEvent---" + eventName + "   " + msg);
    }
    void onRewardedVideoEvent(string eventName, string msg)
    {/*
        //Debug.Log("handler onRewardedVideoEvent---" + eventName + "  rewarded: " + msg);
        if (eventName == AdmobEvent.onRewarded)
        {

            //Debug.Log("touch video onrewarded event -------------");
            if (PlayerPrefs.HasKey("x4DiamondButton"))
            {
                SaveManager.Instance.state.diamond += 4;
                SaveManager.Instance.Save();
                PlayerPrefs.DeleteKey("x4DiamondButton");

                //Flurry Event
                EventLogExample.Instance.RewardForVideo("x4DiamondButton");
            }
            EventLogExample.Instance.VideoShown(null, "x4DiamondButton");

         }*/
    }
}
