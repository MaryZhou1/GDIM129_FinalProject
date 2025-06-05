using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;


public class ChapterTransition : MonoBehaviour
{
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI subtitleText;
    public CanvasGroup canvasGroup;

    public float fadeInDuration = 0.5f;
    public float holdDuration = 0.5f;
    public float fadeOutDuration = 0.5f;
    public string nextSceneName = "GameScene"; // 你的第一章场景名
    public AudioSource audioSource;
    public AudioClip clickSound;
    public GameObject NextSceneButton;

    void Start()
    {
        StartCoroutine(PlayTransition());
    }

    IEnumerator PlayTransition()
    {
        // 淡入
        //float t = 0f;
        //while (t < fadeInDuration)
        //{
        //    t += Time.deltaTime;
        //    canvasGroup.alpha = Mathf.Lerp(0f, 1f, t / fadeInDuration);
        //    yield return null;
        //}

        yield return new WaitForSeconds(holdDuration);

        // button show
        NextSceneButton.SetActive(true);

        // 淡出
        //t = 0f;
        //while (t < fadeOutDuration)
        //{
        //    t += Time.deltaTime;
        //    canvasGroup.alpha = Mathf.Lerp(1f, 0f, t / fadeOutDuration);
        //    yield return null;
        //}


    }

    public void NextScene()
    {
        if (audioSource && clickSound)
            audioSource.PlayOneShot(clickSound);

        if (nextSceneName == "Chp5")
        {
            GlobalManager.Instance.Chapter = 5; //用于结局！！！！！！！！
        }

        SceneManager.LoadScene(nextSceneName);
    }

}
