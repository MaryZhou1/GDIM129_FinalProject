using UnityEngine;
using TMPro;
using System.Collections;

public class TypingLetter : MonoBehaviour
{
    public TextMeshProUGUI letterText;
    [TextArea(5, 10)]
    public string fullText;

    public float typingSpeed = 0.035f;
    public AudioSource typeSound;
    public GameObject closeButton;

    private Coroutine typingCoroutine;

    void OnEnable()
    {
        // ÿ�δ���ʱ��������Ч��
        letterText.text = "";
        closeButton.SetActive(false);

        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        typingCoroutine = StartCoroutine(TypeOutText());
    }

    IEnumerator TypeOutText()
    {
        for (int i = 0; i < fullText.Length; i++)
        {
            letterText.text += fullText[i];

            // ������Ч����ֻ����ʾ�ɼ��ַ�ʱ���ţ�
            if (fullText[i] != ' ' && fullText[i] != '\n' && typeSound != null)
                typeSound.PlayOneShot(typeSound.clip);

            yield return new WaitForSeconds(typingSpeed);
        }

        yield return new WaitForSeconds(0.3f);
        closeButton.SetActive(true);
    }
}
