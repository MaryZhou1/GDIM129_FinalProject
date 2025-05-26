using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestUI : MonoBehaviour
{
    public TMP_Text quest_name;
    public TMP_Text quest_description;


    public void UpdateUI(QuestNode node)
    {
        if (node != null)
        {
            quest_name.text = node.QuestName;
            quest_description.text = node.QuestDescription;
        }
        else
        {
            quest_name.text = "???";
            quest_description.text = "The End. More gameplay coming up!";
        }
    }
}
