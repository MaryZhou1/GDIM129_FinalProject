using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // not singleton!!
    // manage game progress

    public DialogueManager dialogue_manager;

    [Header("Start")]
    public DialogueNode dialogue_test1;
    public DialogueNode prologue1;


    [Header("Chapter 5")]
    public DialogueNode LowSanEnd;
    public DialogueNode HighSanEnd;




    private void Start()
    {
        if (GlobalManager.Instance.Chapter == 5) // is chapter 5!!!
        {
            if (GlobalManager.Instance.sanity <= 2)
            {
                // 疯狂结局2
                dialogue_manager.StartDialogue(LowSanEnd);
            } else
            {
                // 结局2，3
                dialogue_manager.StartDialogue(HighSanEnd);
            }
        } else
        {
            dialogue_manager.StartDialogue(prologue1);
        }
        
    }

    public void StartGame()
    {
        dialogue_manager.StartDialogue(prologue1);
    }

}