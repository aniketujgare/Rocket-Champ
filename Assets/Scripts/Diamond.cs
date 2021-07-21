using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Diamond : MonoBehaviour
{
    private GameObject Diamond_FX;
    string LevelNo; //For Flurry
    private int n;
    private void Start()
    {
            if (SaveManager.Instance.state.sceneNoIndex.Contains(Manager.Insatance.currentLevel) && SaveManager.Instance.state.IsDiamondCollected.Contains(true))
            {
                Destroy(gameObject);
            }
       
        Diamond_FX = transform.GetChild(2).gameObject;
        LevelNo = SceneManager.GetActiveScene().name;

    }
    private void Update()
    {
        transform.Rotate(Vector3.up, 200 * Time.deltaTime);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerPrefs.SetInt("4xButton", 1);
            SaveManager.Instance.DiamondState();
            Diamond_FX.SetActive(true);
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            gameObject.GetComponent<BoxCollider>().enabled = false;
            //Diamond Pickup Sound
            SoundManager.PlaySound(SoundManager.Sound.Diamond_Pickup);

            //Flurry Event
            EventLogExample.Instance.DiamondCollected(LevelNo);

            StartCoroutine(DestroyObject());
        }
    }

    IEnumerator DestroyObject ()
    {
        yield return new WaitForSeconds(1.088f);
        Destroy(gameObject);

    }
}
