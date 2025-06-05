using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class SanityUI : MonoBehaviour
{
    // UI
    public TMP_Text SanTxt;
    public Image SanBar;

    // fields
    private int max_san = 12;
    private int current_san = 0;
    private float initialWidth = 137.44f;

    public void Start()
    {
        max_san = GlobalManager.Instance.MaxSan;
        current_san = GlobalManager.Instance.sanity;
    }

    public void UpdateSanity()
    {
        SanTxt.text = "San: " + current_san.ToString();
        float fillAmount = (float)current_san / (float)max_san;
        // Debug.Log("Fill Amount: " + fillAmount);
        SanBar.rectTransform.sizeDelta = new Vector2(initialWidth * fillAmount, SanBar.rectTransform.sizeDelta.y);
    }

    public void ChangeSanity(int change)
    {
        GlobalManager.Instance.sanity = Mathf.Clamp(current_san + change, 0, max_san);
        current_san = GlobalManager.Instance.sanity;

        Debug.Log($"Sanity changed to: {current_san} (Change: {change}) - Caller: {new System.Diagnostics.StackTrace().GetFrame(1).GetMethod().Name}", this);

        //UI
        UpdateSanity();
    }
}
