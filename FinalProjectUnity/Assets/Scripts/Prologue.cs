using UnityEngine;
using TMPro;
using System.Collections;

public class PrologueText : MonoBehaviour
{
    public TextMeshProUGUI[] lines; // 赋值：Inspector中拖入每一行Text
    public float delayBetweenLines = 2f;
    public float fadeDuration = 1f;

    void Start()
    {
        StartCoroutine(PlayPrologue());
    }

    IEnumerator PlayPrologue()
    {
        foreach (var line in lines)
        {
            yield return StartCoroutine(FadeInLine(line));
            yield return new WaitForSeconds(delayBetweenLines);
        }

        // 等全部播完后进入主菜单/游戏
        yield return new WaitForSeconds(2f);
        // SceneManager.LoadScene("MainMenu");
    }

    IEnumerator FadeInLine(TextMeshProUGUI line)
    {
        line.alpha = 0;
        float t = 0;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            line.alpha = Mathf.Lerp(0, 1, t / fadeDuration);
            yield return null;
        }
    }
}
