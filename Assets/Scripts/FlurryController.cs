using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Assertions;
using FlurrySDK;

// ------------------------------------------------------------------------
// Name	:	CanPlay
// Desc	:	-
// KEY - DFGV9Z7N79WZ6SSHWNJD
// ------------------------------------------------------------------------

public class FlurryController : MonoBehaviour
{
#if UNITY_ANDROID
    private string FLURRY_API_KEY = "DFGV9Z7N79WZ6SSHWNJD";
#elif UNITY_IPHONE
    private string FLURRY_API_KEY = "IOS_API_KEY";
#else
    private string FLURRY_API_KEY = null;
#endif

    public enum EventID
    {
        GameFirstLaunch,//
        GameLaunch,//
        KeyPress,
        SpinAndWin,
        UnlockedShipSkin,
        SelectedShipSkin,

        First_Game_start,//
        First_Play_CLick,//

        GameMode,
        GameLevel,
        NumberOfRounds,
        CompletedRounds,

        PlayerSkin,
        ShareButton,


        LevelNo,//
        LevelStart,
        LevelComplete,
        LevelLoss,
        LevelReplay,

        PressedX4DiamondRward,

        DiamondCollected,
        NoOfWins,//
        NoOfLoss,//

        VideoShown,
        VideoFail,
        RewardForVideo,

        PlayerRetry,
        PlayerNextLevel,
        PlayerMainMenu,

        GameRetry,
        GameMainMenu,

        InterstitialShown,//

        Game_Complete_Successfully,//
        BonusLevelNo,
    }
    /*
    [SerializeField]
    private string _androidApiKey = string.Empty;
    [SerializeField]
    private string _iOSApiKey = string.Empty;
    */
    private string UserIdStr;

    private static Flurry FlurryInstance;

    private static FlurryController _Instance = null;
    public static FlurryController Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = (FlurryController)FindObjectOfType(typeof(FlurryController));
            }

            return _Instance;
        }
    }
    private void Awake()
    {
        UserIdStr = SystemInfo.deviceUniqueIdentifier;
    }
    void Start()
    {
        // Initialize Flurry.
        new Flurry.Builder()
                  .WithCrashReporting(true)
                  .WithLogEnabled(true)
                  .WithLogLevel(Flurry.LogLevel.VERBOSE)
                  .WithMessaging(true)               
                  .Build(FLURRY_API_KEY);

        // Log Flurry events.
        Flurry.EventRecordStatus status = Flurry.LogEvent("Unity Event");

        

        Flurry.UserProperties.Set("UserId", UserIdStr);
        UnityEngine.Debug.Log("Log Unity Event status: " + status);
        //Flurry event
        string prefStr = FlurryController.EventID.GameFirstLaunch.ToString();
        if (PlayerPrefs.GetInt(prefStr, 0) == 0)
        {
            FlurryController.Instance.PostEvent(FlurryController.EventID.GameFirstLaunch, true, "name", GetPlayerProfileName());
            PlayerPrefs.SetInt(prefStr, 1);
        }

    }
    // ------------------------------------------------------------------------
    // Name	:	-
    // Desc	:	-
    // ------------------------------------------------------------------------

    public string GetPlayerProfileName()
    {
        string name;
        bool createAcount = PlayerPrefs.GetInt("custom_id_existing", 0) == 0;
        string[] titleOptions = {  "Ninja","Hattori","Shinchan","Shishimaru","Kenichi","Mitsuba",
            "Tsubame","Sonam","Shinzo","Player","Guest","User","Amara",
            "Kemuzo","Kemumaki","Kagechiyo","Keo","Yumeko","Aiko","Jinchu",
            "Jippo","Kentaru","Professor","Cactochan","Sasuke","Naruto",
            "Yoshi","Shredder","Doraemon","Krang","Karai","Slash",
            "Uzumaki","Leonardo","Ayane","Yoshi","Hamate","Splinter","Orochimaru" };
        if (createAcount)
        {
            name = titleOptions[Random.Range(0, 38)] + Random.Range(0, 999).ToString();
            PlayerPrefs.SetString("guest_login_name", name);
        }
        else
        {
            name = PlayerPrefs.GetString("guest_login_name", "");
        }

        PlayerPrefs.SetInt("custom_id_existing", 1);
        PlayerPrefs.Save();

        return name;
    }

    // ------------------------------------------------------------------------
    // Name	:	-
    // Desc	:	-
    // ------------------------------------------------------------------------

    public void PostEvent(EventID eId, bool onlyFirst = false)
    {
        bool canPost = true;

        if (onlyFirst)
        {
            if (PlayerPrefs.GetInt(eId.ToString(), 0) == 0)
            {
                PlayerPrefs.SetInt(eId.ToString(), 1);
                PlayerPrefs.Save();
            }
            else
            {
                canPost = false;
            }
        }

        if (canPost)
            Flurry.LogEvent(eId.ToString()).ToString();
        //FlurryInstance.LogEvent(eId.ToString()).ToString();

    }

    // ------------------------------------------------------------------------
    // Name	:	-
    // Desc	:	-
    // ------------------------------------------------------------------------

    public void PostEvent(EventID eId, bool onlyFirst, params string[] paramsArray)
    {
        bool canPost = true;

        if (onlyFirst)
        {
            if (PlayerPrefs.GetInt(eId.ToString(), 0) == 0)
            {
                PlayerPrefs.SetInt(eId.ToString(), 1);
                PlayerPrefs.Save();
            }
            else
            {
                canPost = false;
            }
        }

        if (canPost)
        {
            Dictionary<string, string> paramsDict = new Dictionary<string, string>() { { "userId", UserIdStr } };

            for (int i = 0; i < paramsArray.Length; i += 2)
            {
                paramsDict.Add(paramsArray[i], paramsArray[i + 1]);
            }

           string successStr = Flurry.LogEvent(eId.ToString(), paramsDict).ToString();

            UnityEngine.Debug.Log("Flurry event status : " + eId + " : " + successStr);

            foreach (var item in paramsDict)
                UnityEngine.Debug.Log(item.Key + " , " + item.Value);
        }
    }

}
