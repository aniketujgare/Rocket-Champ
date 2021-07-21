using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

[SelectionBase]
public class Cracker_Trigger : MonoBehaviour
{
    private GameObject player;
    public GameObject[] crackers;
    public GameObject[] distructionObstacles;
    public GameObject crackerExplodeVFX;

    [Header("Impulsive Object")]
    public GameObject[] objectImpulse;
    public float destroySeconds = 10f;
    [SerializeField] float xForce = 1f;
    [SerializeField] float yForce = 1f;
    private Vector3 impulse;

     private bool a = false;
     private bool b = true;

    private void Update()
    {

        if (a == true && (CrossPlatformInputManager.GetButton("Jump") || Input.GetKey(KeyCode.Space)))
        {
            ReleaseShip();
            Debug.Log("released");
            a = false;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && b == true)
        {
           player = other.gameObject;
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

        crackers = GameObject.FindGameObjectsWithTag("Cracker");
            distructionObstacles = GameObject.FindGameObjectsWithTag("DistructionObstacles");


            if (crackers != null && distructionObstacles != null)
        {
            SoundManager.PlaySound(SoundManager.Sound.Cracker_Explosion);


            foreach (GameObject gameobject in crackers)
                {
                    Destroy(gameobject);
                    GameObject clone = Instantiate(crackerExplodeVFX, gameobject.transform.localPosition, Quaternion.identity);
                    Destroy(clone, 2f);
                }

                foreach (GameObject gameobject in distructionObstacles)
                {
                    gameobject.GetComponent<ExplosionBar>().enabled = true;
                    Debug.Log("destrucyion box enabled");
                }
            }
            if (objectImpulse != null)
            {
                foreach (GameObject gameobject in objectImpulse)
                {
                    impulse = new Vector3(xForce, yForce, 0f);
                    gameobject.GetComponent<Rigidbody>().AddForce(impulse, ForceMode.VelocityChange);
                    Destroy(gameobject, destroySeconds);
                }
            }

            if (distructionObstacles != null)
            {


            }
             b = false;
        }

        IEnumerator WaitForAnimationFinish()
        {
            yield return new WaitForSeconds(.35f);
        SoundManager.PlaySound(SoundManager.Sound.Poerup_Activate_Button);
        ActivateButton();
            a = true;

        }
}


