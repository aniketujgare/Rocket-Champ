using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShipsAssets : MonoBehaviour
{
    private static ShipsAssets _instance;

    public static ShipsAssets Instance
    {
        get
        {
            if (_instance == null)
                _instance = GameObject.FindObjectOfType<ShipsAssets>();
            return _instance;
        }
    }

    /////////////////////////////////////////////////////////////////////////////

    public GameObject playerShip;
    public GameObject fracturedShip;
    int shipno;
    int newShipno;


    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        StartCoroutine(SelectShip());
    }
    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            newShipno = SaveManager.Instance.state.activeShip;
            if (shipno != newShipno)
            {
               StartCoroutine( SelectShip());
            }
        }
    }

    IEnumerator SelectShip()
    {
        shipno = SaveManager.Instance.state.activeShip;
        
        switch (shipno)
        {
            case 0:
                ResourceRequest loadOrangeShip= Resources.LoadAsync("PlayerShips/OrangeShip", typeof(GameObject));
                // Wait for completion
                yield return loadOrangeShip;
                playerShip = loadOrangeShip.asset as GameObject;

                ResourceRequest loadOrangeFracturedhip = Resources.LoadAsync("FracturedShips/Ship Orange Fractured", typeof(GameObject));
                yield return loadOrangeFracturedhip;
                fracturedShip = loadOrangeFracturedhip.asset as GameObject;
                break;

            case 1:
                ResourceRequest loadBluePlayerShip = Resources.LoadAsync("PlayerShips/BlueShip", typeof(GameObject));
                yield return loadBluePlayerShip;
                playerShip = loadBluePlayerShip.asset as GameObject;

                ResourceRequest loadBlueFracturedhip = Resources.LoadAsync("FracturedShips/Ship Blue Fractured", typeof(GameObject));
                yield return loadBlueFracturedhip;
                fracturedShip = loadBlueFracturedhip.asset as GameObject;
                break;

            case 2:
                ResourceRequest loadRedPlayerShip = Resources.LoadAsync("PlayerShips/RedShip", typeof(GameObject));
                yield return loadRedPlayerShip;
                playerShip = loadRedPlayerShip.asset as GameObject;

                ResourceRequest loadRedFracturedhip = Resources.LoadAsync("FracturedShips/Ship Red Fractured", typeof(GameObject));
                yield return loadRedFracturedhip;
                fracturedShip = loadRedFracturedhip.asset as GameObject;
                break;

            case 3:
                ResourceRequest loadYellowPlayerShip = Resources.LoadAsync("PlayerShips/YellowShip", typeof(GameObject));
                yield return loadYellowPlayerShip;
                playerShip = loadYellowPlayerShip.asset as GameObject;

                ResourceRequest loadYellowFracturedhip = Resources.LoadAsync("FracturedShips/Ship Yellow Fractured", typeof(GameObject));
                yield return loadYellowFracturedhip;
                    fracturedShip = loadYellowFracturedhip.asset as GameObject;
                break;

            case 4:
                ResourceRequest loadVioletPlayerShip = Resources.LoadAsync("PlayerShips/VioletShip", typeof(GameObject));
                yield return loadVioletPlayerShip;
                playerShip = loadVioletPlayerShip.asset as GameObject;

                ResourceRequest loadVioletFracturedhip = Resources.LoadAsync("FracturedShips/Ship Violet Fractured", typeof(GameObject));
                yield return loadVioletFracturedhip;
                fracturedShip = loadVioletFracturedhip.asset as GameObject;
                break;
            
            case 5:
                ResourceRequest loadDarkBluePlayerShip = Resources.LoadAsync("PlayerShips/ShipDarkBlue", typeof(GameObject));
                yield return loadDarkBluePlayerShip;
                playerShip = loadDarkBluePlayerShip.asset as GameObject;

                ResourceRequest loadDarkBlueFracturedhip = Resources.LoadAsync("FracturedShips/Ship Dark Blue Fractured", typeof(GameObject));
                yield return loadDarkBlueFracturedhip;
                fracturedShip = loadDarkBlueFracturedhip.asset as GameObject;
                break;

            case 6:
                ResourceRequest loadDarkOrangePlayerShip = Resources.LoadAsync("PlayerShips/ShipDarkOrange", typeof(GameObject));
                yield return loadDarkOrangePlayerShip;
                playerShip = loadDarkOrangePlayerShip.asset as GameObject;

                ResourceRequest loadDarkOrangeFracturedhip = Resources.LoadAsync("FracturedShips/Ship Dark Orange Fractured", typeof(GameObject));
                yield return loadDarkOrangeFracturedhip;
                fracturedShip = loadDarkOrangeFracturedhip.asset as GameObject;
                break;

            case 7:
                ResourceRequest loadLightGreenPlayerShip = Resources.LoadAsync("PlayerShips/ShipLightGreen", typeof(GameObject));
                yield return loadLightGreenPlayerShip;
                playerShip = loadLightGreenPlayerShip.asset as GameObject;

                ResourceRequest loadLightGreenFracturedhip = Resources.LoadAsync("FracturedShips/Ship Light Green Fractured", typeof(GameObject));
                yield return loadLightGreenFracturedhip;
                fracturedShip = loadLightGreenFracturedhip.asset as GameObject;
                break;

            case 8:
                ResourceRequest loadSkyBluePlayerShip = Resources.LoadAsync("PlayerShips/ShipSkyBlue", typeof(GameObject));
                yield return loadSkyBluePlayerShip;
                playerShip = loadSkyBluePlayerShip.asset as GameObject;

                ResourceRequest loadSkyBlueFracturedhip = Resources.LoadAsync("FracturedShips/Ship Sky Blue Fractured", typeof(GameObject));
                yield return loadSkyBlueFracturedhip;
                fracturedShip = loadSkyBlueFracturedhip.asset as GameObject;
                break;

            case 9:
                ResourceRequest loadLimeYellowPlayerShip = Resources.LoadAsync("PlayerShips/ShipLimeYellow", typeof(GameObject));
                yield return loadLimeYellowPlayerShip;
                playerShip = loadLimeYellowPlayerShip.asset as GameObject;

                ResourceRequest loadLimeYellowFracturedhip = Resources.LoadAsync("FracturedShips/Ship Lime Yellow Fractured", typeof(GameObject));
                yield return loadLimeYellowFracturedhip;
                fracturedShip = loadLimeYellowFracturedhip.asset as GameObject;
                break;

            case 10:
                ResourceRequest loadShipPurplePlayerShip = Resources.LoadAsync("PlayerShips/ShipPurple", typeof(GameObject));
                yield return loadShipPurplePlayerShip;
                playerShip = loadShipPurplePlayerShip.asset as GameObject;

                ResourceRequest loadPurpleFracturedhip = Resources.LoadAsync("FracturedShips/Ship Purple Fractured", typeof(GameObject));
                yield return loadPurpleFracturedhip;
                fracturedShip = loadPurpleFracturedhip.asset as GameObject;
                break;

            case 11:
                ResourceRequest loadShipBlackPlayerShip = Resources.LoadAsync("PlayerShips/ShipBlack", typeof(GameObject));
                yield return loadShipBlackPlayerShip;
                playerShip = loadShipBlackPlayerShip.asset as GameObject;

                ResourceRequest loadBlackFracturedhip = Resources.LoadAsync("FracturedShips/Ship Black Fractured", typeof(GameObject));
                yield return loadBlackFracturedhip;
                fracturedShip = loadBlackFracturedhip.asset as GameObject;
                break;

            case 12:
                ResourceRequest loadShipGreyRedPlayerShip = Resources.LoadAsync("PlayerShips/ShipGreyRed", typeof(GameObject));
                yield return loadShipGreyRedPlayerShip;
                playerShip = loadShipGreyRedPlayerShip.asset as GameObject;

                ResourceRequest loadShipGreyRedFracturedhip = Resources.LoadAsync("FracturedShips/Ship Grey Red Fractured", typeof(GameObject));
                yield return loadShipGreyRedFracturedhip;
                fracturedShip = loadShipGreyRedFracturedhip.asset as GameObject;
                break;


        }
    }       
}
