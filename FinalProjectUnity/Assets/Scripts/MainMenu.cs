using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioSource source;
    public AudioClip clip;


    public GameObject tutorialPanel; // Tutorial UI ���
    public GameObject mainMenuPanel; // ���˵���壨��ѡ��

    public void StartGame()
    {
        SceneManager.LoadScene("PrologueScene");
    }

    public void OpenTutorial()
    {
        source.PlayOneShot(clip);

        tutorialPanel.SetActive(true);
        if (mainMenuPanel != null) mainMenuPanel.SetActive(false);
    }

    public void CloseTutorial()
    {
        source.PlayOneShot(clip);

        tutorialPanel.SetActive(false);
        if (mainMenuPanel != null) mainMenuPanel.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
