using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;


public class ChapterTransition : MonoBehaviour
{
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI subtitleText;
    public CanvasGroup canvasGroup;

    public float fadeInDuration = 1.5f;
    public float holdDuration = 2f;
    public float fadeOutDuration = 1.5f;
    public string nextSceneName = "GameScene"; // 你的第一章场景名

    void Start()
    {
        StartCoroutine(PlayTransition());
    }

    IEnumerator PlayTransition()
    {
        // 淡入
        float t = 0f;
        while (t < fadeInDuration)
        {
            t += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, t / fadeInDuration);
            yield return null;
        }

        yield return new WaitForSeconds(holdDuration);

        // 淡出
        t = 0f;
        while (t < fadeOutDuration)
        {
            t += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, t / fadeOutDuration);
            yield return null;
        }

        // 切换到主游戏场景
        SceneManager.LoadScene(nextSceneName);
    }
}
