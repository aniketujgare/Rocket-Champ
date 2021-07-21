using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameScene : MonoBehaviour
{
    public void CompleteLevel()
    {

        // Complete level, save progress
        SaveManager.Instance.CompleteLevel(Manager.Insatance.currentLevel);
    }
}
