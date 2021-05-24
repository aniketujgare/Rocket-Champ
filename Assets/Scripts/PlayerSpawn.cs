using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{ 
    public GameObject launchPad;
    private GameObject playerShip;
    private Vector3 offset = new Vector3(0, 5, 0);


    private void Start()
    {

         PlayerInstantiate();
        
    }

   
       public void PlayerInstantiate ()
       {
           if (launchPad == null)
           {
               launchPad = GameObject.Find("Launch Pad");
           }
        //Instantiate(InstantiatePlayer.Instance.Playership, launchPad.transform.position + offset, Quaternion.identity);

        int shipIndex = SaveManager.Instance.state.activeShip;
        // Debug.Log("Active ship is " + shipIndex);

        Instantiate(ShipsAssets.Instance.playerShip, launchPad.transform.position + offset, Quaternion.identity);
        
    }

}
