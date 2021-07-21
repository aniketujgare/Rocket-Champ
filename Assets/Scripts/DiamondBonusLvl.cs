using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DiamondBonusLvl : MonoBehaviour
{
    private GameObject Diamond_FX;
    string LevelNo; //For Flurry
    private void Start()
    {
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
            SaveManager.Instance.state.diamond++;
            SaveManager.Instance.Save();
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

    IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(1.088f);
        Destroy(gameObject);

    }
}
