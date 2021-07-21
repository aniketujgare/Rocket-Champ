using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hyperlink : MonoBehaviour
{
public void FBPage()
    {
        Application.OpenURL("https://www.facebook.com/RocketChampGame");
    }
    public void Website()
    {
        Application.OpenURL("https://rocketchampgame.blogspot.com/");
    }
}
