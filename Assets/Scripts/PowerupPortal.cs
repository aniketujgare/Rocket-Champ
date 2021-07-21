using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PowerupPortal : MonoBehaviour
{
    private GameObject player;
    public GameObject mainPortal;
    private GameObject destPortalVFX;
    private GameObject mainPortalVFX;
    bool a = false;

    [Header("Activated Material Color")]
    public Material activatedMaterialColor;
    private void Start()
    {
        if (mainPortal != null)
        {
            mainPortalVFX = mainPortal.transform.GetChild(0).gameObject;
            mainPortalVFX.SetActive(false);
            destPortalVFX = mainPortal.GetComponent<Teleport>().destination.transform.GetChild(0).gameObject;
            destPortalVFX.SetActive(false);
            mainPortal.GetComponent<BoxCollider>().isTrigger = false;

        }
    }
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
            player = other.gameObject;
            other.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            Debug.Log("Freezed");
            ActivateButton();
            Debug.Log("Returned from ActivateButton");
        /*    if (CrossPlatformInputManager.GetButton("Jump") || Input.GetKey(KeyCode.Space))
            {
                Debug.Log("Recorded input");
                other.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            }*/
        }
    }
    void ReleaseShip()
    {
        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;

     /*   if (CrossPlatformInputManager.GetButton("Jump") || Input.GetKey(KeyCode.Space))
        {
            Debug.Log("Recorded input");
            player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        }*/
    }
    void ActivateButton()
    {
        SoundManager.PlaySound(SoundManager.Sound.Poerup_Activate_Button);

        if (mainPortal== null)
        mainPortal = GameObject.Find("Portal");
        if (mainPortal != null)
        {
            mainPortalVFX.SetActive(true);
            destPortalVFX.SetActive(true);
            mainPortal.GetComponent<BoxCollider>().isTrigger =true;

            Debug.Log("boxcolider enbled");
            GameObject edge1 = gameObject.transform.GetChild(1).gameObject;
            GameObject edge2 = gameObject.transform.GetChild(2).gameObject;
            edge1.GetComponent<Renderer>().material = activatedMaterialColor;
            edge2.GetComponent<Renderer>().material = activatedMaterialColor;
            a = true;
        }
        else
            Debug.Log("main portal is not found");
    }
}

