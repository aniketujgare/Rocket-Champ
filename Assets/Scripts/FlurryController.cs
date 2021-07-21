using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Assertions;
using Analytics;

// ------------------------------------------------------------------------
// Name	:	CanPlay
// Desc	:	-
// ------------------------------------------------------------------------

public class FlurryController : MonoBehaviour
{
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
    }

    [SerializeField]
    private string _androidApiKey = string.Empty;
    [SerializeField]
    private string _iOSApiKey = string.Empty;
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

    void Awake()
    {
        Init();
    }

    // ------------------------------------------------------------------------
    // Name	:	-
    // Desc	:	-
    // ------------------------------------------------------------------------

    public void Init()
    {
        UnityEngine.Debug.Log("_androidApiKey  :  " + _androidApiKey);
        FlurryAndroid.SetLogEnabled(true);

        FlurryInstance = Flurry.Instance;
        FlurryInstance.StartSession(string.Empty, _androidApiKey);
        UnityEngine.Debug.Log("_androidApiKey  :  " + _androidApiKey + "string.Empty : " + string.Empty);

#if UNITY_ANDROID
        UserIdStr = SystemInfo.deviceUniqueIdentifier;
        FlurryAndroid.SetUserId(UserIdStr);
        //if(Settings.Instance.Age >0)
        //FlurryAndroid.SetAge(Settings.Instance.Age);
#endif

        //Flurry event
        string prefStr = FlurryController.EventID.GameFirstLaunch.ToString();
        if (PlayerPrefs.GetInt(prefStr, 0) == 0)
        {
            FlurryController.Instance.PostEvent(FlurryController.EventID.GameFirstLaunch, true, "name", GetPlayerProfileName());
            PlayerPrefs.SetInt(prefStr, 1);
        }
        //		FlurryController.Instance.PostEvent(FlurryController.EventID.GameFirstLaunch);
    }

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
            FlurryInstance.LogEvent(eId.ToString()).ToString();
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

            string successStr = FlurryInstance.LogEvent(eId.ToString(), paramsDict).ToString();

            UnityEngine.Debug.Log("Flurry event status : " + eId + " : " + successStr);

            foreach (var item in paramsDict)
                UnityEngine.Debug.Log(item.Key + " , " + item.Value);
        }
    }
}
