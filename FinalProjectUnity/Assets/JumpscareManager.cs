using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JumpscareManager : MonoBehaviour
{
    public void ToEnding()
    {
        SceneManager.LoadScene("EndScene");
    }
}
