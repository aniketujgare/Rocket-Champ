using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewShips : MonoBehaviour
{
    private GameObject[] ships;
    private int index;
    public GameObject aciveShip;


    // Start is called before the first frame update
    void Start()
    {
        ships = new GameObject[transform.childCount];

        //Fill the array with menu panels
        for (int i = 0; i < transform.childCount; i++)
            ships[i] = transform.GetChild(i).gameObject;

        //Debug.Log("no of ships is"+ships);
        // We toggle off their renderer
        foreach (GameObject go in ships)
        {
            go.gameObject.SetActive(false);
        }
        // Set player preferences for ship
        //Debug.Log("active ship indes is " + index);
        ToggleShipPreviewCurrent();
        /*
                //Turn On Lights
                lightDown.SetActive(true);
                lightUp.SetActive(true);
                lightLeft.SetActive(true);
                lightRight.SetActive(true);
        */
    }

    public void ToggleShipPreviewCurrent()
    {
        //Toggle on the new panel
        index = SaveManager.Instance.state.activeShip;
        // Debug.Log("active ship indes is " + index);
        ships[index].SetActive(true);
        aciveShip = ships[index];
    }


}