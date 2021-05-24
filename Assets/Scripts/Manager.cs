using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
  public static Manager Insatance { set; get; }

    private void Awake()
    {
        Application.targetFrameRate = 60;
        DontDestroyOnLoad(gameObject);
        Insatance = this;
    }

    public int currentLevel = 0;    // Used when changing from menu to game scene.
    public int menuFocus = 0;       // When entering to menu scene, to know where to focus
}
