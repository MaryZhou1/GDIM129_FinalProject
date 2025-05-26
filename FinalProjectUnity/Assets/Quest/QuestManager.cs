using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class QuestManager : MonoBehaviour
{
    // singleton
    // manage a list of quests throughout the game


    public static QuestManager instance;

    public QuestUI questUI;

    public QuestNode current_node;



    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void StartQuest(QuestNode node)
    {
        current_node = node;

        current_node.isActive = true;

        questUI.UpdateUI(current_node);
    }

    public void NextQuest()
    {
        if (current_node.next_quest != null)
        {
            StartQuest(current_node.next_quest);
        }
        else
        {
            current_node = null;

            //current_node.isActive = false;
            //current_node.isComplete = true;
            questUI.UpdateUI(current_node);
        }
    }
}

