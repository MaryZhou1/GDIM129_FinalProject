using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemPopupPanel : MonoBehaviour
{
    public static ItemPopupPanel Instance;

    public GameObject panel;
    public Image itemImage;
    public TMP_Text itemDescription;

    private System.Action onCloseCallback;
    private bool isActive = false;

    private void Awake()
    {
        Instance = this;
        Debug.Log("✅ ItemPopupPanel.Instance is set!");
        panel.SetActive(false);
    }

    private void Update()
    {
        if (isActive && Input.GetKeyDown(KeyCode.Escape)) // 按 Esc 关闭
        {
            ClosePanel();
        }
    }

    public void ShowItem(Sprite image, string description, System.Action callback = null)
    {
        itemImage.sprite = image;
        itemDescription.text = description;
        onCloseCallback = callback;
        panel.SetActive(true);
        Time.timeScale = 0f; // 可选暂停
        isActive = true;
    }

    private void ClosePanel()
    {
        panel.SetActive(false);
        Time.timeScale = 1f;
        isActive = false;
        onCloseCallback?.Invoke();
        onCloseCallback = null;
    }
}
