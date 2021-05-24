using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager: MonoBehaviour
{
    public static SaveManager Instance {set; get;}
    public SaveState state;



    private void Awake()
    {
       // ResetSave();
        DontDestroyOnLoad(gameObject);
        Instance = this;
        Load();
    }
    // Save the whole state of this saveState script to the player pref
    public void Save ()
    {
       PlayerPrefs.SetString("save",Helper.Encrypt(Helper.serialize<SaveState>(state)));
    }

    // Load the previous saved stat from the player prefs
    public void Load()
    {
        if(PlayerPrefs.HasKey("save"))
        {
            state = Helper.Deserialize<SaveState>(Helper.Decrypt(PlayerPrefs.GetString("save")));
        }
        else
        {
            state = new SaveState();
            Save();
            Debug.Log("No save file found, creating a new one");
        }
    }
    
    //check if ship is owned
    public bool IsShipOwned(int index)
    {
        // check if the bit is set, if so the ship is  owned
        return (state.shipOwned & (1 << index)) != 0;
    }

    // unlock a ship in the "shipOwned"int

    //Attempt buying a Ship, return true/false
    public bool BuyShip(int index, int cost)
    {
        if(state.diamond >= cost)
        {
            // Enough miney, remove from the current gold stack
            state.diamond -= cost;
            UnlockShip(index);
            //Flurry Event
            EventLogExample.Instance.UnlockedShipSkin(index,cost);
            // Save progress
            Save();
            return true;
        }
        else
        {
            //Not enough money return false
            return false;
        }
    }
    public void UnlockShip(int index)
    {
        // TOggle on the bit at index
        state.shipOwned |= 1 << index;
    }

    // Complete Level
    public void CompleteLevel(int index)
    {
        // If this is the current active level
        if (state.completedLevel == index)
            state.completedLevel++;
        Save();
    }

    //reset the whole save file
    public void ResetSave ()
    {
        PlayerPrefs.DeleteKey("save");
    }

    public void DiamondState ()
    {
        state.sceneNoIndex.Add(Manager.Insatance.currentLevel);
        state.IsDiamondCollected.Add(true);
        state.diamond++;
        Save();
    }

    public void DiamondReward()
    {
        state.diamond++;
        state.diamond++;
        state.diamond++;
        state.diamond++;
        Save();
    }

    public void Diamondx4()
    {
        state.diamond+=4;
        Save();
    }
    public void Diamondx8()
    {
        state.diamond += 8;
        Save();
    }
    public void DiamondJackpot()
    {
        state.diamond += 25;
        Save();
    }



}
