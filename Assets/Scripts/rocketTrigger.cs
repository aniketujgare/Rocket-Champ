using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

[SelectionBase]
public class rocketTrigger : MonoBehaviour
{
    private GameObject player;
    private GameObject[] rocket;
    private void Update()
    {
        ReleaseShip();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = GameObject.FindGameObjectWithTag("Player");
            other.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            Debug.Log("Freezed");
            gameObject.GetComponent<Animator>().enabled = true;
            StartCoroutine(WaitForAnimationFinish());
            Debug.Log("Returned from ActivateButton");
            if (CrossPlatformInputManager.GetButton("Jump") || Input.GetKey(KeyCode.Space))
            {
                Debug.Log("Recorded input");
                other.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            }
        }
    }
    void ReleaseShip()
    {
        if (CrossPlatformInputManager.GetButton("Jump") || Input.GetKey(KeyCode.Space))
        {
            Debug.Log("Recorded input");
            player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        }
    }

    void ActivateButton()
    {
        SoundManager.PlaySound(SoundManager.Sound.Poerup_Activate_Button);

        rocket = GameObject.FindGameObjectsWithTag("Rocket");

        if (rocket != null)
        {
            foreach (GameObject go in rocket)
            {
                go.GetComponent<RocketCracker>().enabled = true;
            }
        }
    }

    IEnumerator WaitForAnimationFinish()
    {
        yield return new WaitForSeconds(.35f);
        ActivateButton();
    }
}

