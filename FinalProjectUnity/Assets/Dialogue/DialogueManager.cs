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


    [Header("UI")]
    public GameObject dialoguePanel;

    public TMP_Text speakerText;
    public TMP_Text dialogueText;

    public Image Background;
    public Image Image;

    public Transform replyButtonParent;
    public GameObject replyButtonPrefab;
    public Item itemToGive;
    public List<Image> itemSlots; // �� Inspector ���Ͻ�3��Image����
    private List<Item> acquiredItems = new List<Item>();

    [Header("audio")]
    public AudioClip dialogueClickClip;      // ��� .wav �ļ�
    public AudioSource audioSource_click;          // ���ڲ��� clip �� audioSource
    public AudioSource audioSource_extra;          // ���ڲ��� clip �� audioSource


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
        // can't use left click, will mess up with choices
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

        //// change ending
        //if (current_node.EndingIndex != 0)
        //    GlobalManager.Instance.EndingIndex = current_node.EndingIndex;

        // change san
        if (current_node.SanChange != 0)
            sanUI.ChangeSanity(current_node.SanChange);

        if (current_node.itemToGive != null)
        {
            AddItem(current_node.itemToGive); // ��� item �� UI

            dialogue_active = false; // ��ͣ�Ի�����

            ItemPopupPanel.Instance.ShowItem(
                current_node.itemPopupImage != null ? current_node.itemPopupImage : current_node.itemToGive.icon,
                string.IsNullOrEmpty(current_node.itemPopupDescription) ? "" : current_node.itemPopupDescription,
                () =>
                {
                    dialogue_active = true;
                    NextLine(); // �ر���������Ի�
                });
        }

        //                   ----------- extra audio  -------

        if (current_node.Audio != null && audioSource_extra != null)
        {
            audioSource_extra.PlayOneShot(current_node.Audio);
        }


        //                   -------  start line  -------
        NextLine();

        // check for item reward
   

    }



    private void NextLine()
    {
        if (line_index < current_node.Lines.Count)
        {
            dialogueText.text = current_node.Lines[line_index];
            line_index++;

            //   ���ŵ����Ч��ʹ�� AudioSource ��һ�� AudioClip
            if (dialogueClickClip != null && audioSource_click != null)
            {
                audioSource_click.PlayOneShot(dialogueClickClip);
            }
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
                reply.SuccessedNode.Lines[0] = "Passed (You rolled: " + tmp_dice_holder + ", AC: " + reply.DiceCheck_AC + ")"; // write on scriptable obj
                StartDialogue(reply.SuccessedNode);
            }
            else
            {
                reply.FailedNode.Lines[0] = "Failed (You rolled: " + tmp_dice_holder + ", AC: " + reply.DiceCheck_AC + ")"; // write on scriptable obj
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

        // �л������������ǰ node �����ˣ�
        if (!string.IsNullOrEmpty(current_node.nextSceneName))
        {
            SceneManager.LoadScene(current_node.nextSceneName);
            return;
        }

        // ���������Ϸ���������Ž�֣����һ��node�������ý��index��Ĭ���ǽ��0��
        GlobalManager.Instance.EndingIndex = current_node.EndingIndex;        
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
