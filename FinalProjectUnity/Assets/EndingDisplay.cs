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



    void Start()
    {
        if (GlobalManager.Instance.EndingIndex == 1)
        {
            EndingDescription.text = ending1;
            EndingTitle.text = "Your End: Nothing";
        }

    }

}
