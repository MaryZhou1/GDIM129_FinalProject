using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GlobalManager : MonoBehaviour
{
    // singleton.
    // manage pause, resume, scene change

    public static GlobalManager Instance;

    [Header("PlayerStatus")]
    public int sanity = 10;
    public int MaxSan = 10;
    


    [Header("Ending")]
    public int EndingIndex = 1; // 储存玩家触发的结局index





    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void ToGameScene()
    {
        SceneManager.LoadScene("GameScene");
        // isPaused = false;
    }

    public void ToHomeScene()
    {
        SceneManager.LoadScene("HomeScene");
        // isPaused = true;
    }


    //private string e0 = "Nothing";
    //private string e1 = "Sacrifice";
    //private string e2 = "Insane";
    //private string e3 = "flead";

    public void DisplayEnding()
    {
        if (EndingIndex == 1)
        {
            if (sanity > 2)
            {
                SceneManager.LoadScene("EndScene");
            }
            else
            {
                SceneManager.LoadScene("Jumpscare");
            }
        }
        else if (EndingIndex == 2)
        {
            if (sanity > 2)
            {
                SceneManager.LoadScene("EndScene");
            }
            else
            {
                SceneManager.LoadScene("Jumpscare");
            }
        }
    }
}
