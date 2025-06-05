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
                EndingTitle.text = "Your End: Insane";
                EndingDescription.text = "Insane...";
                break;
            case 2:
                EndingTitle.text = "Your End: Sacrifice";
                EndingDescription.text = "Sacrifice...";
                break;

            case 3:
                EndingDescription.text = "Your End: Live";
                EndingTitle.text = "Live...";
                break;
        }

    }

}
