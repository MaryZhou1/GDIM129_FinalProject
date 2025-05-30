using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(fileName = "DialogueNode", menuName = "Dialogue/DialogueNode", order = 1)]
public class DialogueNode : ScriptableObject
{
    [Header("Basic")]
    public string Speaker;
    public List<string> Lines;

    [Header("Reply Options")]
    public List<ReplyOption> ReplyOptions;

    [Header("Background & Image")]
    public Sprite Background_Sprite;
    public Sprite Image_Sprite;

    [Header("Ending Change")]
    public int ChangeEndingIndex; // default 0 = no leading to ending

    [Header("San Change")]
    public int SanChange = 0; // defult 0 = no san change

    //[Header("not used...")]
    //public bool ProcessQuest;

    //[Tooltip("[Optional] The associated quest FINISHED after this line completes.")]
    //public Quest QuestFinished;

    //[Tooltip("[Optional] The audio that should play at the beginning of this line.")]
    //public AudioClip Audio;
}

[Serializable]
[Inspectable]
public class ReplyOption
{
    [Header("Basic")]
    [Inspectable] public string line;
    [Inspectable] public DialogueNode nextNode;

    [Header("Dice Checking")]
    [Inspectable] public int DiceCheck_AC; // defult 0 = no dice check
    [Inspectable] public DialogueNode SuccessedNode;
    [Inspectable] public DialogueNode FailedNode;
}
