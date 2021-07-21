using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PowerupWindFanOn : MonoBehaviour
{
    private GameObject player;
    public GameObject[] windFans;
    public GameObject[] windHideBar;
    public GameObject[] windVFX;
    private GameObject otherd;
    bool a = false;

    [Header("Activated Material Color")]
    public Material activatedMaterialColor;

    // public GameObject scriptattachedobject;
    private void Update()
    {
        if (a == true && (CrossPlatformInputManager.GetButton("Jump") || Input.GetKey(KeyCode.Space)))
        {
            ReleaseShip();
            a = false;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = GameObject.FindGameObjectWithTag("Player");
            other.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            Debug.Log("Freezed");
            ActivateButton();
            Debug.Log("Returned from ActivateButton");
      /*      if (CrossPlatformInputManager.GetButton("Jump") || Input.GetKey(KeyCode.Space))
            {
                Debug.Log("Recorded input");
                other.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            }*/
        }
    }
    void ReleaseShip()
    {
        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;

       /* if (CrossPlatformInputManager.GetButton("Jump") || Input.GetKey(KeyCode.Space))
        {
            Debug.Log("Recorded input");
            player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        }*/
    }

    void ActivateButton()
    {
        SoundManager.PlaySound(SoundManager.Sound.Poerup_Activate_Button);

        foreach (GameObject go in windFans)
        {
            go.transform.GetChild(3).GetComponent<RotatePreviewShip>().enabled = true;
        }

        foreach (GameObject go in windHideBar)
        {
            go.SetActive(true);
        }
        foreach (GameObject go in windVFX)
        {
            go.SetActive(true);
        }
        GameObject edge1 = gameObject.transform.GetChild(1).gameObject;
        GameObject edge2 = gameObject.transform.GetChild(2).gameObject;
        edge1.GetComponent<Renderer>().material = activatedMaterialColor;
        edge2.GetComponent<Renderer>().material = activatedMaterialColor;
        a = true;
    }
}