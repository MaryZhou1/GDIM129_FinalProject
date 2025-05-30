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

    public Image Background;
    public Image Image;

    public Transform replyButtonParent;
    public GameObject replyButtonPrefab;


    // =================
    [SerializeField] private bool dialogue_active = false;
    private DialogueNode current_node;
    private int line_index = 0;
    private int tmp_dice_holder;


    private void Update()
    {
        if (dialogue_active && Input.GetKeyDown(KeyCode.Space))
        {
            NextLine();
        }
    }

    public void StartDialogue(DialogueNode node)
    {
        // basic set
        dialogue_active = true;
        current_node = node;
        line_index = 0;
        dialoguePanel.SetActive(true);

        // GlobalManager.Instance.Pause();

        //                   -------  optional set  -------

        // speaker
        speakerText.text = current_node.Speaker;

        // has image or no image
        if (current_node.Image_Sprite != null)
        {
            Image.gameObject.SetActive(true);
            Image.sprite = current_node.Image_Sprite; 
        }
        else
            Image.gameObject.SetActive(false);

        // change background
        if (current_node.Image_Sprite != null)
            Image.sprite = current_node.Image_Sprite;

        // change ending
        if (current_node.ChangeEndingIndex != 0)
            GlobalManager.Instance.EndingIndex = current_node.ChangeEndingIndex;

        // change san
        if (current_node.SanChange != 0)
            GlobalManager.Instance.ChangeSanity(current_node.SanChange);
        

        //                   -------  start line  -------
        NextLine();
    }



    private void NextLine()
    {
        if (line_index < current_node.Lines.Count)
        {
            dialogueText.text = current_node.Lines[line_index]; // line
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
        foreach (Transform child in replyButtonParent) // clear options
            Destroy(child.gameObject);



        if (reply.DiceCheck_AC != 0) // has dice check
        {
            if (DiceRolling(reply.DiceCheck_AC)) // call func
            {
                reply.SuccessedNode.Lines[0] = "You rolled: " + tmp_dice_holder; // write on scriptable obj
                StartDialogue(reply.SuccessedNode);
            }
            else
            {
                reply.FailedNode.Lines[0] = "You rolled: " + tmp_dice_holder; // write on scriptable obj
                StartDialogue(reply.FailedNode);
            }
        }
        else
        {
            StartDialogue(reply.nextNode); // no dice check
        }
    }



    private void EndDialogue()
    {
        dialogue_active = false;
        dialoguePanel.SetActive(false);

        // GlobalManager.Instance.Resume();


        // If Progress Quest
        //if (current_node.ProcessQuest)
        //{
        //    QuestManager.instance.NextQuest();
        //}


        current_node = null;

        // end of the game...
        GlobalManager.Instance.DisplayEnding();
    }


    private bool DiceRolling(int AC)
    {
        int outcome = Random.Range(0, 20);
        Debug.Log("You rolled " + outcome);
        tmp_dice_holder = outcome;

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
