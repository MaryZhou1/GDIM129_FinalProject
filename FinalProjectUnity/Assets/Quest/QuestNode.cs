using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "QuestNode", menuName = "Quest/QuestNode", order = 1)]
public class QuestNode : ScriptableObject
{
    public string QuestName;
    public string QuestDescription;
    public bool isActive = false;
    public bool isComplete = false;

    public QuestNode next_quest;
}
