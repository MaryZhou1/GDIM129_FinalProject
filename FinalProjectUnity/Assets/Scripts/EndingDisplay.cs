using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndingDisplay : MonoBehaviour
{
    public TMP_Text EndingTitle;
    public TMP_Text EndingDescription;
    private string ending1 = "You know most of the main character can avoid the whole event just by, not caring." +
        " \n But seriously? This might be a horrible example of game play design, and it's your fault for clicking it.";
    private string ending2 = "Ok you went to the island. the stroy is still developing ummm,....this is just a test";
    private string ending3 = "You remember......done";
    private string ending4 = "You tried to think but everything is empty. You got a brain damange and died";

    private string e0 = "Nothing";
    private string e1 = "Sacrifice";
    private string e2 = "Insane";
    private string e3 = "Live";
    private string e4 = "Death";


    void Start()
    {
        GlobalManager.Instance.Chapter = 0; // reset


        switch (GlobalManager.Instance.EndingIndex)
        {
            case 1:
                EndingDescription.text = "You called out for truth¡ª\nBut it never came.\n\nThe ritual began, and never ended.\nNow the voices won't stop.\nNot even in the dark.";
                EndingTitle.text = "Your End: Insane" ;
                break;
            case 2:
                EndingDescription.text = "The villagers prayed.\nThe altar burned.\nYou did not resist.\n\nThe tide was calm.\nBut your name was never spoken again.";
                EndingTitle.text = "Your End: Sacrifice" ;
                break;

            case 3:
                EndingDescription.text = "You escaped the island.\nBut not the memories.\n\nIn time, you turned the story into a novel...\nYet the last page is still missing.";
                EndingTitle.text = "Your End: Survived"; 
                break;
        }

    }

}
