using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // not singleton!!
    // manage game progress

    public DialogueManager dialogue_manager;

    // public QuestNode quest1;

    public DialogueNode dialogue_test1;
    public DialogueNode prologue1;




    private void Start()
    {
        dialogue_manager.StartDialogue(prologue1);
    }

    public void StartGame()
    {
        dialogue_manager.StartDialogue(prologue1);
    }

}