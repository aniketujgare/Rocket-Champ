using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

[SelectionBase]
public class PowerupMove : MonoBehaviour
{
    private GameObject player;
   // public GameObject ActivateEffect;
    public GameObject[] movingObjects;
    public Material activatedMaterialColor;
    bool a =false;
    private void Update()
    {
        
        if (a == true &&( CrossPlatformInputManager.GetButton("Jump") || Input.GetKey(KeyCode.Space)))
        {
            ReleaseShip();
           // Debug.Log("Unfreeze");
            a = false;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.gameObject;
            other.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            //Debug.Log("Freezed");
            ActivateButton();
            //Debug.Log("Returned from ActivateButton");
        }
    }

    void ActivateButton()
    {
        SoundManager.PlaySound(SoundManager.Sound.Poerup_Activate_Button);
        if (movingObjects != null)
        {
            foreach (GameObject gameobject in movingObjects)
            {
                gameobject.GetComponent<FollowThePath>().enabled = true;
            }
            GameObject edge1 = gameObject.transform.GetChild(1).gameObject;
            GameObject edge2 = gameObject.transform.GetChild(2).gameObject;
            edge1.GetComponent<Renderer>().material = activatedMaterialColor;
            edge2.GetComponent<Renderer>().material = activatedMaterialColor;
        }
        a = true;
    }
    void ReleaseShip()
    {
        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
    }
}
