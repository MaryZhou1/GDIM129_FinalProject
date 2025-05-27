using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalManager : MonoBehaviour
{
    // singleton.
    // manage pause, resume, scene change

    public static GlobalManager Instance;

    // public DialogueManager DialogueManager;
    // public QuestManager QuestManager;

    // public bool isPaused = true;







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

    //public void global_StartDialogue(DialogueNode node)
    //{
    //    DialogueManager.StartDialogue(node);
    //}


    //public void Pause()
    //{
    //    isPaused = true;
    //    Time.timeScale = 0f;
    //    //Cursor.visible = true;
    //    //Cursor.lockState = CursorLockMode.None;
    //}

    //public void Resume()
    //{
    //    isPaused = false;
    //    Time.timeScale = 1f;
    //    //Cursor.visible = false;
    //    //Cursor.lockState = CursorLockMode.Locked;
    //}

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

    public void ToEndScene()
    {
        SceneManager.LoadScene("EndScene");
    }
}
