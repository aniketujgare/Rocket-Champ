using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StarAppearCO());
    }

IEnumerator StarAppearCO ()
    {
        transform.GetChild(2).gameObject.SetActive(true);
        SoundManager.PlaySound(SoundManager.Sound.Star_animation);
        yield return new WaitForSeconds(.35f);
        SoundManager.PlaySound(SoundManager.Sound.Star_animation);
        transform.GetChild(3).gameObject.SetActive(true);
        yield return new WaitForSeconds(.35f);
        SoundManager.PlaySound(SoundManager.Sound.Star_animation);
        transform.GetChild(4).gameObject.SetActive(true);
    }
}
