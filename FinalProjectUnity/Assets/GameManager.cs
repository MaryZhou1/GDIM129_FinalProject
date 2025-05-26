using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // not singleton!!
    // manage game progress

    public DialogueManager dialogue_manager;

    public DialogueNode dialogue1;
    public QuestNode quest1;



    private void Start()
    {
        dialogue_manager.StartDialogue(dialogue1);
        //QuestManager.instance.StartQuest(quest1);
    }
}