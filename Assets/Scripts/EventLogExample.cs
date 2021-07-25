using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using FlurrySDK;
public class EventLogExample : MonoBehaviour
{

    // string LevelNo = "";
    private static EventLogExample _Instance = null;
    public static EventLogExample Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = (EventLogExample)FindObjectOfType(typeof(EventLogExample));
            }

            return _Instance;
        }
    }
   private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
    public void PressedSpinAndWin(string LevelNO)
    {
        FlurryController.Instance.PostEvent(FlurryController.EventID.SpinAndWin, false,"Spinned At", LevelNO);
        
        
    }
    public void UnlockedShipSkin(int index,int cost)
    {
        FlurryController.Instance.PostEvent(FlurryController.EventID.UnlockedShipSkin, false, "SkinNo", index.ToString(),"Cost",cost.ToString());
    }
    public void SelectedShipSkin(int index)
    {
        FlurryController.Instance.PostEvent(FlurryController.EventID.SelectedShipSkin, false, "SkinNo", index.ToString());
    }

    public void LevelStart(string LevelNo)
    {
        FlurryController.Instance.PostEvent(FlurryController.EventID.LevelStart, false, "LevelStart", LevelNo);
    }
    public void LevelComplete(string LevelNo)
    {
        FlurryController.Instance.PostEvent(FlurryController.EventID.LevelComplete, false, "LevelCompleted", LevelNo);
    }
    public void LevelLoss(string LevelNo)
    {
        FlurryController.Instance.PostEvent(FlurryController.EventID.LevelLoss, false, "LevelLoss", LevelNo);
    }
    public void PressedLevelReplay(string LevelNo)
    {
        FlurryController.Instance.PostEvent(FlurryController.EventID.LevelLoss, false, "LevelReplay", LevelNo);
    }
    public void PressedX4DiamondReward(string LevelNo)
    {
        FlurryController.Instance.PostEvent(FlurryController.EventID.PressedX4DiamondRward, false, "At Level",LevelNo);
    }
    public void VideoShown(string LevelNo, string ButtonName)
    {
        FlurryController.Instance.PostEvent(FlurryController.EventID.VideoShown, false, "VideoShown At",  LevelNo, "On Button", ButtonName);
    }
    public void RewardForVideo(string rewardType)
    {
        FlurryController.Instance.PostEvent(FlurryController.EventID.RewardForVideo, false,"Rewarded",rewardType);
    }
    public void InterstitialShown(string LevelNo)
    {
        FlurryController.Instance.PostEvent(FlurryController.EventID.InterstitialShown, false, "At Level", LevelNo);
    }
    
    public void DiamondCollected(string LevelNo)
    {
        FlurryController.Instance.PostEvent(FlurryController.EventID.DiamondCollected, false,"In Level",LevelNo);
    }
    public void BonusLevelPlayed(string LevelNo)
    {
        FlurryController.Instance.PostEvent(FlurryController.EventID.BonusLevelNo, false, "Bonus Level", LevelNo);
    }

}
