using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject tutorialPanel; // Tutorial UI 面板
    public GameObject mainMenuPanel; // 主菜单面板（可选）

    public void StartGame()
    {
        SceneManager.LoadScene("PrologueScene");
    }

    public void OpenTutorial()
    {
        tutorialPanel.SetActive(true);
        if (mainMenuPanel != null) mainMenuPanel.SetActive(false);
    }

    public void CloseTutorial()
    {
        tutorialPanel.SetActive(false);
        if (mainMenuPanel != null) mainMenuPanel.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
