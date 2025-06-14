using System.Collections;
using UnityEngine;
[RequireComponent(typeof(DoFade))]
public class MuquiController : NPC
{
    [Header("Characteristis")]
    [SerializeField] private Transform target;
    [SerializeField] private float timeTarget;
    private Vector3 currentPosition;
    private float totalDistance;
    private float speedTarget;
    private Collider2D colliderMuqui;
    private bool wentToPlayer = false;

    [Header("Player Data")]
    [SerializeField] private float stopDistance;
    [SerializeField] private float time;
    private Transform player;
    private float speed;

    [Header("DoFade")]
    private DoFade fade;

    protected override void Awake()
    {
        base.Awake();
        fade = GetComponent<DoFade>();
        colliderMuqui= GetComponent<Collider2D>();
    }
    protected override void Start()
    {
        base.Start();
        player = GameManager.instance.GetTransformPlayer();
    }
    private void OnEnable()
    {
        FlorController.OnInteractue += InitialMovement;
        //dialogue.OnFinishDialogue += StartTutorial;
    }
    private void OnDisable()
    {
        FlorController.OnInteractue -= InitialMovement;
        //dialogue.OnFinishDialogue += StartTutorial;
    }
    private void StartTutorial()
    {
        GameManager.instance.Tutorial(true);
    }
    private void InitialMovement()
    {
        if (wentToPlayer)
        {
            currentPosition = target.position;
            totalDistance = Vector2.Distance(transform.position, currentPosition);
            speedTarget = totalDistance / timeTarget;
            fade.FadeOut();
            colliderMuqui.enabled = false;
            StartCoroutine(GoPosition(currentPosition, speedTarget, 0));
        }
        else
        {
            wentToPlayer = true;
            GameManager.instance.EnableInput(false);
            currentPosition = player.position;
            totalDistance = Vector2.Distance(transform.position, currentPosition) - stopDistance;
            speed = totalDistance / time;
            StartCoroutine(GoPosition(currentPosition, speed, stopDistance));
        }
    }
    private IEnumerator GoPosition(Vector3 position,float speed,float stopDistance)
    {
        while (Vector2.Distance(transform.position, position) > stopDistance)
        {
            transform.position = Vector2.MoveTowards(
                transform.position,
                position,
                speed * Time.deltaTime
            );
            yield return null;
        }
        if (Vector2.Distance(transform.position, player.position) <= stopDistance + 0.05f)
        {
            //dialogue.CheckEnumDialogue();
        }
    }
}
