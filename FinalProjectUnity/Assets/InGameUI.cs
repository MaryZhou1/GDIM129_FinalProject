using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HomeButton()
    {
        GlobalManager.Instance.ToHomeScene();
    }

    public void GameButton()
    {
        GlobalManager.Instance.ToGameScene();
    }
}
