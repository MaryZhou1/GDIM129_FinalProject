using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    // ------------------- Sanity -------------------
    //public TMP_Text SanTxt;
    //public Image SanBar;

    //public void SanityUI(int sanity, int max_san, float initialWidth)
    //{
    //    SanTxt.text = sanity.ToString();
    //    float fillAmount = (float)sanity / (float)max_san;
    //    SanBar.rectTransform.sizeDelta = new Vector2(initialWidth * fillAmount, SanBar.rectTransform.sizeDelta.y);
    //}


    // ------------------- Buttons -------------------



    public void HomeButton()
    {
        GlobalManager.Instance.ToHomeScene();
    }

    public void GameButton()
    {
        GlobalManager.Instance.ToGameScene();
    }
}
