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
    public int sanity = 100;
    public TMP_Text SanTxt;
    public Image SanBar;

    private int max_san = 100;
    private float initialWidth = 137.44f;


    [Header("Ending")]
    public int EndingIndex;





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



    public void ChangeSanity(int change)
    {
        sanity += change;
        Debug.Log(sanity);
        SanTxt.text = sanity.ToString();
        float fillAmount = (float)sanity / (float)max_san;
        Debug.Log(fillAmount);
        SanBar.rectTransform.sizeDelta = new Vector2(initialWidth * fillAmount, SanBar.rectTransform.sizeDelta.y);
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

    public void DisplayEnding()
    {
        SceneManager.LoadScene("EndScene");

        // EndingIndex = ending_index;
    }
}
