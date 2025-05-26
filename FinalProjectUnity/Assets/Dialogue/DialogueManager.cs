using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.U2D;
// using UnityEngine.InputSystem;


public class DialogueManager : MonoBehaviour
{
    // not singleton!!
    // manage dialogue


    //public static DialogueManager Instance;
    //private void Awake()
    //{
    //    if (Instance == null)
    //    {
    //        Instance = this;
    //        DontDestroyOnLoad(gameObject);
    //    }
    //    else
    //    {
    //        Destroy(gameObject);
    //    }
    //}

    [Header("UI")]
    public GameObject dialoguePanel;

    public TMP_Text speakerText;
    public TMP_Text dialogueText;
    public Image Image;

    public Transform replyButtonParent;
    public GameObject replyButtonPrefab;

    [Header("SerializeField")]
    [SerializeField] private DialogueNode current_node;
    [SerializeField] private int line_index = 0;



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            NextLine();
        }
    }

    public void StartDialogue(DialogueNode node)
    {
        // set
        current_node = node;
        line_index = 0;
        dialoguePanel.SetActive(true);

        GlobalManager.Instance.Pause();

        NextLine();
    }

    private void NextLine()
    {
        if (line_index < current_node.Lines.Count)
        {
            dialogueText.text = current_node.Lines[line_index]; // line
            speakerText.text = current_node.Speaker; // speaker
            Image.sprite = current_node.ImageSprite; // image
            line_index++;
        }
        else
        {
            if (current_node.ReplyOptions != null && current_node.ReplyOptions.Count > 0)
            {
                ShowReplyOptions();
            }
            else
            {
                EndDialogue();
            }
        }
    }



    private void ShowReplyOptions()
    {
        dialogueText.text = "";

        foreach (Transform child in replyButtonParent) // clear
            Destroy(child.gameObject);


        foreach (var reply in current_node.ReplyOptions)
        {
            GameObject buttonObj = Instantiate(replyButtonPrefab, replyButtonParent);
            buttonObj.GetComponentInChildren<TextMeshProUGUI>().text = reply.line;
            buttonObj.GetComponent<Button>().onClick.AddListener(() => OnClickReply(reply));
        }
    }


    private void OnClickReply(ReplyOption reply)
    {
        foreach (Transform child in replyButtonParent) // clear
            Destroy(child.gameObject);

        if (reply.DiceCheck)
        {
            // has dice check
            if (DiceRolling(10))
            {
                StartDialogue(reply.SuccessedNode);
            }
            else
            {
                StartDialogue(reply.FailedNode);
            }
        }
        else
        {
            // no dice check
            StartDialogue(reply.nextNode);
        }
    }



    private void EndDialogue()
    {
        dialoguePanel.SetActive(false);

        GlobalManager.Instance.Resume();


        // If Progress Quest
        if (current_node.ProcessQuest)
        {
            QuestManager.instance.NextQuest();
        }


        current_node = null;
    }


    private bool DiceRolling(int AC)
    {
        int outcome = Random.Range(0, 20);
        Debug.Log("You rolled " + outcome);

        if (outcome >= AC)
        {
            Debug.Log("passed");
            return true;
        }
        else
        {
            Debug.Log("failed");
            return false;
        }
    }
}
