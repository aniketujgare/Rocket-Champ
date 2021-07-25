using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScene : MonoBehaviour
{
    public RectTransform menuContainer;
    public Transform levelPanel;
    public Text shipBuySetText;
    public Text[] diamondText;

    private int[] shipCost = new int[] { 0, 50, 50, 50, 115, 110, 180, 115, 180, 120, 120, 300, 135, 350, 370, 400 };
    private int selectedShipIndex;
    private int activeShipIndex;

    public Transform ShipPanel;
    public Transform ShipPreviewPanel;

    public GameObject SpinWheelCamera;

    public GameObject gg; // reference form panel toggle
    public ParticleSystem FireCircleVFX;

    [Header("Lights Of ShipCam")]
    public GameObject lightUp;
    public GameObject lightDown;
    public GameObject lightLeft;
    public GameObject lightRight;

    public Sprite sprite;
   // public Sprite blueSprite;
    private void Start()
    {
        // $$ Temporary
        //SaveManager.Instance.state.diamond = 999;
        // Tell our diamond text how much should display

        //Particle System To be Inactive
        FireCircleVFX.Pause();
        UpdateDiamondText();

        // Add button on-click event to shop buttons
        InitShop();

        // Add buttons on-click event to levels
        InitLevel();

        // Set player preferences for ship
        OnShipSelect(SaveManager.Instance.state.activeShip);
        setShip(SaveManager.Instance.state.activeShip);

       // setShip(SaveManager.Instance.state.activeShipPreview);

        // Make BUtton bigger for the selected item
       // ShipPanel.GetChild(SaveManager.Instance.state.activeShip).GetComponent<RectTransform>().localScale = Vector3.one * 1.125f;
    }
    private void Update()
    {
        UpdateDiamondText();
       // menuContainer.anchoredPosition3D = Vector3.Lerp(menuContainer.anchoredPosition3D, desiredMenuPosition, 1f);
    }
    private void InitShop()
    {
        // Just make sure that we assign references
        if (ShipPanel == null)
            Debug.Log("You didi not assigned panel in inspector");

        //For every children transform under ship panel, find the button and add onlick
        int i = 0;
        foreach (Transform t in ShipPanel)
        {
            int currentIndex = i;
            Button b = t.GetComponent<Button>();
            b.onClick.AddListener(() => OnShipSelect(currentIndex));

            // set the color of image, based on if owned or not
            Image img = t.GetComponent<Image>();
            img.color = SaveManager.Instance.IsShipOwned(i) ? Color.white : new Color(0.7f, 0.7f, 0.7f);
            i++;
        }
    }

    private void InitLevel()
    {
        // Just make sure that we assign references
        if (levelPanel == null)
            Debug.Log("You didi not assigned level panel in inspector");

        //For every children transform under Level panel, find the button and add onlick
        int i = 0;
        foreach (Transform t in levelPanel)
        {
            int currentIndex = i;
            Button b = t.GetComponent<Button>();
            b.onClick.AddListener(() => OnLevelSelect(currentIndex));
            Image img = t.GetComponent<Image>();

            // Is it unlocked?
            if(i<= SaveManager.Instance.state.completedLevel)
            {
                // It is unlocked 
                if (i == SaveManager.Instance.state.completedLevel)
                {
                    // It's not completed
                    img.sprite =sprite;
                    img.transform.GetChild(0).GetComponent<Text>().color = new Color(0.4901961f, 0.4588236f, 0.8470589f,1);
                }
                else
                {
                    // Level is already completed
                    img.color = Color.white;
                }
            }
            else
            {
                // Level is't unlocked disable button
                b.interactable = false;

                // Set to the dark color
                img.color = Color.grey;
            }
            i++;
        }
    }
   

    private void setShip(int index)
    {
        // Set the active index
        activeShipIndex = index;
        SaveManager.Instance.state.activeShip = index;
        //SaveManager.Instance.state.activeShipPreview = index;


        // change the player model

        shipBuySetText.text = "CURRENT";

        //remember preferences
        SaveManager.Instance.Save();
    }


    // Buttons
    public void OnWorld1Click()
    {
        SoundManager.PlaySound(SoundManager.Sound.Button_Pop);
       // Debug.Log("Sound button is played at OnWorld1Click");
        //gg.GetComponent<PanelToggel>().ToggleWorld1();
        StartCoroutine(OnWorld1ClickSound());
       // Debug.Log("World 1 button has been clicked!");
    }

    public void OnWorld2Click()
    {
        SoundManager.PlaySound(SoundManager.Sound.Button_Pop);
       // Debug.Log("Sound button is played at OnWorld2Click");
        //NavigateTo(2);
       // Debug.Log("World 2 button has been clicked!");
        PanelToggel.Instance.ToggleAllOff();
        SpinWheelCamera.SetActive(true);

    }
    public void OnDiamondShopClick()
    {
        SoundManager.PlaySound(SoundManager.Sound.Button_Pop);
        //Debug.Log("Sound button is played at OnShopClick");
        gg.GetComponent<PanelToggel>().ToggleDiamondShop();

    }
    public void OnShopClick()
    {
        SoundManager.PlaySound(SoundManager.Sound.Button_Pop);
        //Debug.Log("Sound button is played at OnShopClick");
        gg.GetComponent<PanelToggel>().ToggleShop();
        GetComponent<PreviewShips>();
        //Debug.Log("Shop button has been clicked!");
        foreach (Transform t in ShipPreviewPanel)
        {
            t.gameObject.SetActive(false);
        }
        ShipPreviewPanel.transform.GetChild(SaveManager.Instance.state.activeShip).gameObject.SetActive(true);
        lightDown.SetActive(true);
        lightUp.SetActive(true);
        lightRight.SetActive(true);
        lightLeft.SetActive(true);
      
    }
    public void OnBackClick()
    {
        // gg.GetComponent<PanelToggel>().ToggleHome();
        //SoundManager.PlaySound(SoundManager.Sound.Button_Pop);
        // SceneManager.LoadScene("Menu");
       Initiate.Fade(1, GameAssets.i.color, 1f);

        //Debug.Log("Back Button has been clicked");
    }

    public void OnBackClickNoFade()
    {
        // gg.GetComponent<PanelToggel>().ToggleHome();
       SoundManager.PlaySound(SoundManager.Sound.Button_Pop);
        //Debug.Log("Sound button is played at OnBackClickNoFade");
        // SceneManager.LoadScene("Menu");
        StartCoroutine(OnBackClickNoFadSound());
        //Debug.Log("Back Button has been clicked");
    }

    public void OnBackClickSpiinWheel()
    {
        // gg.GetComponent<PanelToggel>().ToggleHome();
        SoundManager.PlaySound(SoundManager.Sound.Button_Pop);
       // Debug.Log("Sound button is played at onBackClickSpiinWheel");
        // SceneManager.LoadScene("Menu");
        SpinWheelCamera.SetActive(false);
        PanelToggel.Instance.ToggleHome();
        //Debug.Log("Back Button has been clicked");
    }
    private void OnShipSelect(int currentIndex)
    {

        // Debug.Log("Selecting Ship button :" + currentIndex);
        // if the button clicked is alreasdy selected, exit
        if (selectedShipIndex == currentIndex)
        {   // Turn On Preview Of Selected Ship in Left Preview Side
            foreach (Transform t in ShipPanel)
            {
                t.gameObject.GetComponent<RectTransform>().localScale = Vector3.one;
                //t.GetComponent<Image>().sprite =blueSprite;
            }
            ShipPanel.GetChild(selectedShipIndex).GetComponent<RectTransform>().localScale = Vector3.one*1.125f;
           // ShipPanel.GetChild(selectedShipIndex).GetComponent<Image>().sprite = sprite;

            foreach (Transform t in ShipPreviewPanel)
            {
                t.gameObject.SetActive(false);
            }
           ShipPreviewPanel.GetChild(selectedShipIndex).gameObject.SetActive(true);

            return;
        }


        // Make icon slightly bigger
        foreach (Transform t in ShipPanel)
        {
            t.gameObject.GetComponent<RectTransform>().localScale = Vector3.one;
            //t.GetComponent<Image>().sprite = blueSprite;
        }

        ShipPanel.GetChild(currentIndex).GetComponent<RectTransform>().localScale = Vector3.one * 1.125f;
        //ShipPanel.GetChild(currentIndex).GetComponent<Image>().sprite = sprite;



        //SoundManager.PlaySound(SoundManager.Sound.Button_Pop);
        // Debug.Log("Sound button is played at OnShipSelect");
        // Debug.Log("current index is"+currentIndex);


        // Turn On Preview Of Selected Ship in Left Preview Side
        foreach (Transform t in ShipPreviewPanel)
        {
            t.gameObject.SetActive(false); ;
        }
        ShipPreviewPanel.GetChild(currentIndex).gameObject.SetActive(true);

        // Put the previous one on normal scale
        ShipPanel.GetChild(selectedShipIndex).GetComponent<RectTransform>().localScale = Vector3.one;
        //ShipPanel.GetChild(selectedShipIndex).GetComponent<Image>().sprite = blueSprite;


        // Turn Off previous Preview Of Selected Ship in Left Preview Side
        ShipPreviewPanel.GetChild(selectedShipIndex).gameObject.SetActive(false);

        // set the selected ship
        selectedShipIndex = currentIndex;

        // change the content of the buy set button , depending on the state of the ship model
        if (SaveManager.Instance.IsShipOwned(currentIndex))
        {
            // Ship is owned
            // Is it already our current ship
            if(activeShipIndex == currentIndex)
            {
                shipBuySetText.text = "CURRENT";
            }
            else
            {
                shipBuySetText.text = "SELECT";
            }
            
        }
        else
        {
            // Ship isn't Owned
            shipBuySetText.text = "BUY: " + shipCost[currentIndex].ToString();
        }
    }
    public void OnShipBuySet ()
    {
        //Debug.Log("BUY/SET SHIP");

        // is the selected ship is owned
        if(SaveManager.Instance.IsShipOwned(selectedShipIndex))
        {
            //Set the ship!
            FireCircleVFX.Play();
            SoundManager.PlaySound(SoundManager.Sound.ShipSetSound);
            //Debug.Log("Sound button is played at OnShip Buy Set");
            setShip(selectedShipIndex);
            //FLurry Event
            EventLogExample.Instance.SelectedShipSkin(selectedShipIndex);
        }
        else
        {
            //SoundManager.PlaySound(SoundManager.Sound.Button_Pop);
            // Attempt to Buy the ship model
            if (SaveManager.Instance.BuyShip(selectedShipIndex, shipCost[selectedShipIndex]))
            {
                // Succes
                FireCircleVFX.Play();
                SoundManager.PlaySound(SoundManager.Sound.ShipSetSound);
                setShip(selectedShipIndex);
                
                // SoundManager.PlaySound(SoundManager.Sound.ShipSetSound);
                GetComponent<PreviewShips>();

                // Change the color of the button
                ShipPanel.GetChild(selectedShipIndex).GetComponent<Image>().color = Color.white;

                // Update the gold text
                UpdateDiamondText();
            }
            else
            {
                // Do not have enough diamond
                //play sound
                SoundManager.PlaySound(SoundManager.Sound.Eror_Buzzer);

                //Debug.Log("Not Enough Diamonds");
            }
        }
    }

    public void UpdateDiamondText()
    {
        foreach(Text t in diamondText)
        t.text = SaveManager.Instance.state.diamond.ToString();
    }
 
    public void OnLevelSelect(int currentIndex)
    {
        Manager.Insatance.currentLevel = currentIndex;
        //SceneManager.LoadScene(currentIndex + 2);
        SoundManager.PlaySound(SoundManager.Sound.Button_Pop);
        //Debug.Log("Sound button is played at OnLevelSelect");
        Initiate.Fade(currentIndex + 2, GameAssets.i.color, 1f);
        //Debug.Log("Selecting level  :" + currentIndex);
    }

    IEnumerator OnWorld1ClickSound()
    {
        yield return new WaitForSeconds(0.078f);
        gg.GetComponent<PanelToggel>().ToggleWorld1();

    }
    IEnumerator OnBackClickNoFadSound()
    {
        yield return new WaitForSeconds(0.078f);
        SceneManager.LoadScene("Menu");
    }

    public void BigButton ()
    {
        ShipPanel.GetChild(SaveManager.Instance.state.activeShip).GetComponent<RectTransform>().localScale = Vector3.one * 1.125f;
    }

    public void popSound ()
    {
        SoundManager.PlaySound(SoundManager.Sound.Button_Pop);
    }
}
