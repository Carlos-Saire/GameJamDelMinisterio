using TMPro;
using UnityEngine;
public class NPC : Interactue
{
    [Header("Dialogos")]
    [SerializeField]  protected DialogueSO[] dialogos;

    [Header("Text")]
    private TMP_Text text;

    [Header("RaycastDialogo")]
    [SerializeField] protected float distance;
    [SerializeField] protected Vector2 direction;
    [SerializeField] protected LayerMask layer;

    protected virtual void Awake()
    {
        //dialogue = GetComponent<DialogueManager>();
        //text = GetComponentInChildren<TMP_Text>();
    }
    protected virtual void Start()
    {
        //dialogue.SetText(text);
        //text.enabled =false;
    }
    protected override void Interaction()
    {
        //dialogue.CheckEnumDialogue();
        GameManager.instance.Dialogos(dialogos);
    }
    private bool CheckComversation()
    {
        return Physics2D.Raycast(transform.position, direction, distance, layer);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position,distance * direction);
    }

}
