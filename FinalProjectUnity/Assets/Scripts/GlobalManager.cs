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

    // public DialogueManager DialogueManager;
    // public QuestManager QuestManager;

    // public bool isPaused = true;

    [Header("PlayerStatus")]
    public int sanity = 10;
    public int MaxSan = 10;
    


    [Header("Ending")]
    public int EndingIndex = 1;





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

    public void DisplayEnding()
    {
        SceneManager.LoadScene("EndScene");

        // EndingIndex = ending_index;
    }
}
