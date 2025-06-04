using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PrologueWithLetter : MonoBehaviour
{
    public TextMeshProUGUI[] lines;
    public GameObject letterButton;
    public GameObject letterPanel;
    public TextMeshProUGUI letterText;
    [TextArea(5, 10)]
    public string letterContent;
    public string nextScene = "GameScene";

    private bool letterClosed = false;
    private bool letterOpen = false;

    void Start()
    {
        letterButton.SetActive(false);
        letterPanel.SetActive(false);
        StartCoroutine(PlayIntro());
    }

    void Update()
    {
        // Allow ESC to close the letter
        //if (letterOpen && Input.GetKeyDown(KeyCode.Escape))
        //{
        //    CloseLetter();
        //}
    }

    IEnumerator PlayIntro()
    {
        // 前5行字幕
        for (int i = 0; i < 5; i++)
        {
            yield return StartCoroutine(FadeIn(lines[i]));
            yield return new WaitForSeconds(1.5f);
        }

        // 显示信件按钮
        letterButton.SetActive(true);
    }

    public void OpenLetter()
    {
        letterText.text = letterContent;
        letterPanel.SetActive(true);
        letterOpen = true;
    }

    public void CloseLetter()
    {
        if (!letterClosed)
        {
            letterPanel.SetActive(false);
            letterOpen = false;
            letterClosed = true;

            // 隐藏按钮，防止重复点击
            letterButton.SetActive(false);

            // 开始继续播放剩下字幕
            StartCoroutine(ContinueAfterLetter());
        }
    }

    IEnumerator ContinueAfterLetter()
    {
        yield return new WaitForSeconds(1f);

        for (int i = 5; i < lines.Length; i++)
        {
            yield return StartCoroutine(FadeIn(lines[i]));
            yield return new WaitForSeconds(1.5f);
        }

        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(nextScene);
    }

    IEnumerator FadeIn(TextMeshProUGUI text)
    {
        text.alpha = 0;
        float t = 0f;
        float fadeTime = 1f;
        while (t < fadeTime)
        {
            t += Time.deltaTime;
            text.alpha = Mathf.Lerp(0, 1, t / fadeTime);
            yield return null;
        }
    }
}
