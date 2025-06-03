using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.U2D;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
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
    public Item itemToGive;
    public List<Image> itemSlots; // 在 Inspector 里拖进3个Image格子
    private List<Item> acquiredItems = new List<Item>();




    // =================
    public SanityUI sanUI;
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
        sanUI.UpdateSanity();

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
        if (current_node.Background_Sprite != null)
            Background.sprite = current_node.Background_Sprite;

        // change ending
        if (current_node.ChangeEndingIndex != 0)
            GlobalManager.Instance.EndingIndex = current_node.ChangeEndingIndex;

        // change san
        if (current_node.SanChange != 0)
            sanUI.ChangeSanity(current_node.SanChange);

        if (current_node.itemToGive != null)
        {
            AddItem(current_node.itemToGive);
        }


        //                   -------  start line  -------
        NextLine();

        // check for item reward
   

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

        // 切换场景（如果当前 node 设置了）
        if (!string.IsNullOrEmpty(current_node.nextSceneName))
        {
            SceneManager.LoadScene(current_node.nextSceneName);
            return;
        }

        // 否则进入默认结算逻辑
        GlobalManager.Instance.DisplayEnding();

        current_node = null;
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

    private void AddItem(Item item)
    {
        if (acquiredItems.Contains(item)) return; // avoid duplicates

        acquiredItems.Add(item);

        for (int i = 0; i < itemSlots.Count; i++)
        {
            if (itemSlots[i].sprite == null)
            {
                itemSlots[i].sprite = item.icon;
                itemSlots[i].color = Color.white; // make sure it's visible
                break;
            }
        }
    }

}
