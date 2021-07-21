using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelToggel : MonoBehaviour
{
    private static PanelToggel _instance;

    public static PanelToggel Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<PanelToggel>();
            }

            return _instance;
        }
    }
    private GameObject[] menus;
    public GameObject shipCam;
    public GameObject AnimOff;

    private int shipCount;

    private void Start()
    {
        ToggleHome();
        shipCount = menus[1].transform.GetChild(0).GetChild(0).childCount;

    }
    public void ToggleShop()
    {
        // Toggle off the Menu panel
       menus[0].SetActive(false);
        //Toggle on the Shop panel
        menus[1].SetActive(true);
        shipCam.SetActive(true);

        for (int i = 0; i < shipCount; i++)
        {
            menus[1].transform.GetChild(0).GetChild(0).GetChild(i).GetComponent<Animator>().enabled = true;
        }
        StartCoroutine(AnimatorOff());
    }

    public void ToggleWorld1()
    {
        // Toggle off the Menu panel
        menus[0].SetActive(false);
        //Toggle on the World1 panel
        menus[2].SetActive(true);
        shipCam.SetActive(false);
    }

    public void ToggleHome()
    {
        shipCam.SetActive(false);
        menus = new GameObject[transform.childCount];

        //Fill the array with menu panels
        for (int i = 0; i < transform.childCount; i++)
            menus[i] = transform.GetChild(i).gameObject;

        // We toggle off their renderer
        foreach (GameObject go in menus)
        {
            go.gameObject.SetActive(false);
        }
        // We toggle on  First panel
        if (menus[0])
            menus[0].SetActive(true);

    }
    public void ToggleSettings()
    {
        // Toggle off the Menu panel
        menus[0].SetActive(false);
        //Toggle on the World1 panel
        menus[3].SetActive(true);
        shipCam.SetActive(false);
    }
    public void ToggleDiamondShop()
    {
        // Toggle off the Menu panel
        menus[0].SetActive(false);
        //Toggle on the World1 panel
        menus[4].SetActive(true);
        shipCam.SetActive(false);
    }
    public void ToggleAllOff()
    {
        shipCam.SetActive(false);
        menus = new GameObject[transform.childCount];

        //Fill the array with menu panels
        for (int i = 0; i < transform.childCount; i++)
            menus[i] = transform.GetChild(i).gameObject;

        // We toggle off their renderer
        foreach (GameObject go in menus)
        {
            go.gameObject.SetActive(false);
        }
    }
    IEnumerator AnimatorOff()
    {
        yield return new WaitForSeconds(.334f);
        for (int i = 0; i < shipCount; i++)
        {
            menus[1].transform.GetChild(0).GetChild(0).GetChild(i).GetComponent<Animator>().enabled = false;
        }
        AnimOff.GetComponent<MenuScene>().BigButton();
    }
}
