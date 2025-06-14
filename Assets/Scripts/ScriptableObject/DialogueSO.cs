using UnityEngine;
[CreateAssetMenu(fileName = "DialogueSO", menuName = "Scriptable Objects/Dialogue/DialogueSO")]
public class DialogueSO : ScriptableObject
{
    public enum Option
    {
        Player,
        NPC
    }

    [Header("Dialogue")]
    [SerializeField] private string[] dialogues;
    [SerializeField] private Option option;
    public string[] Dialogues=> dialogues;
    public Option DialogueOption => option;
}
