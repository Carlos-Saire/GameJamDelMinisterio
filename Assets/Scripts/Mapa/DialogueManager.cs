using TMPro;
using UnityEngine;
using System.Collections;
using System;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class DialogueManager : MonoBehaviour
{
    public event Action OnFinishDialogue;

    [Header("DialogueSO")]
    [SerializeField] private DialogueSO[] dialogues;
    [SerializeField] private float typingSpeed;
    [SerializeField] private float waitAfterLine;
    [SerializeField] private Sprite[] arrayImagePanelDialogue;
    [SerializeField] private Image imagePanelDialogue;
    private int currentIndex;
    private string[] currentDialogues;


   private TMP_Text text;

    private void Reset()
    {
        typingSpeed = 0.08f;
        waitAfterLine = 1.2f;
    }

    private void Start()
    {
        imagePanelDialogue.sprite = arrayImagePanelDialogue[0];
    }
    private void Awake()
    {
        text = GetComponentInChildren<TMP_Text>();
    }
    private void Update()
    {
        
    }
    public void StartDialogue(InputAction.CallbackContext callback)
    {
        if (!callback.performed) return;
        SetDialogue(dialogues);
    }
    public void SetDialogue(DialogueSO[] dialogueSOs)
    {
        this.dialogues = dialogueSOs;
        CheckEnumDialogue();
    }
    public void CheckEnumDialogue()
    {
        if (currentIndex < 0 || currentIndex >= dialogues.Length)
        {
            GameManager.instance.EnableInput(true);
            return;
        }
        if (dialogues[currentIndex].DialogueOption == DialogueSO.Option.Player)
        {
            imagePanelDialogue.sprite = arrayImagePanelDialogue[1];
            StartDialogues(text);
        }
        else
        {
            imagePanelDialogue.sprite = arrayImagePanelDialogue[0];
            StartDialogues(text);
        }
    }
    private void StartDialogues(TMP_Text text)
    {
        GameManager.instance.EnableInput(false);
        StartCoroutine(DialoguesCorritune(text));
    }
    private void StartNextDialogue()
    {
        if (currentIndex < dialogues.Length)
        {
            if (dialogues[currentIndex].DialogueOption == DialogueSO.Option.Player)
            {
                imagePanelDialogue.sprite = arrayImagePanelDialogue[1];
                StartCoroutine(DialoguesCorritune(text));
            }
            else
            {
                imagePanelDialogue.sprite = arrayImagePanelDialogue[0];
                StartCoroutine(DialoguesCorritune(text));
            }
        }
        else
        {

            OnFinishDialogue?.Invoke();
            GameManager.instance.EnableInput(true);
        }
    }
    private IEnumerator DialoguesCorritune(TMP_Text text)
    {
        currentDialogues = dialogues[currentIndex].Dialogues;
        for (int i = 0; i <currentDialogues.Length; ++i)
        {
            text.text = "";
            for (int j = 0; j < currentDialogues[i].Length; ++j)
            {
                text.text += currentDialogues[i][j];
                yield return new WaitForSecondsRealtime(typingSpeed);
            }
            yield return new WaitForSecondsRealtime(waitAfterLine);
        }
        ++currentIndex;  
        StartNextDialogue();
    }

}
