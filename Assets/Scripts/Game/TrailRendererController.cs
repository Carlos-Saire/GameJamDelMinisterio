using System;
using UnityEngine;

[RequireComponent(typeof(TrailRenderer))]
public class TrailRendererController : MonoBehaviour
{
    public event Action OnKeyPressed;
    public static event Action OnFail;

    [Header("Target")]
    [SerializeField] private RectTransform target;

    [Header("Movement Settings")]
    [SerializeField] private float timeTarget;
    private RectTransform rectTransform;
    private float speed;
    private float distanceTarget;
    private float distance;
    private bool hasArrived = true;
    private bool inputReceived = true;
    private bool eventCalled = false;

    [SerializeField] private KeyCode inputKey;
    [SerializeField] private float perfectWindow = 0.3f;
    [SerializeField] private float goodWindow = 0.6f;

    [Header("Sound")]
    [SerializeField] private AudioClipSO Up;
    [SerializeField] private AudioClipSO Down;
    [SerializeField] private AudioClipSO Left;
    [SerializeField] private AudioClipSO Right;

    public static event Action Onsfx;

    private TrailRenderer trailRenderer;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        trailRenderer = GetComponent<TrailRenderer>();
    }

    private void Start()
    {
        distanceTarget = Vector3.Distance(rectTransform.position, target.position);
        speed = distanceTarget / timeTarget;
    }

    private void Update()
    {
        if (hasArrived) return;

        rectTransform.position = Vector3.MoveTowards(rectTransform.position, target.position, speed * Time.deltaTime);
        distance = Vector3.Distance(rectTransform.position, target.position);

        if (rectTransform.position == target.position)
        {
            Destroy(gameObject, trailRenderer.time);
            hasArrived = true;
            OnKeyPressed?.Invoke();
            HandleFail();
        }
    }
    private void OnDestroy()
    {
        UnsubscribeInput();
    }
    private void CheckInput()
    {
        if (inputReceived || eventCalled) return;

        inputReceived = true;
        InvokeEventOnce();

        if (distance <= perfectWindow)
            Debug.Log("Perfect!");
        else if (distance <= goodWindow)
            Debug.Log("Good!");
        else
            Debug.Log("Miss");
    }

    private void InvokeEventOnce()
    {
        if (eventCalled) return;
        eventCalled = true;

        UnsubscribeInput();

    }

    public void SetTarget(RectTransform target, KeyCode key)
    {
        this.target = target;
        inputKey = key;
        hasArrived = false;
    }

    public void StartMove()
    {
        inputReceived = false;
        eventCalled = false;
        SubscribeInput();
    }

    private void SubscribeInput()
    {
        InputReader.OnArrowInput += OnArrowInput;
    }

    private void UnsubscribeInput()
    {
        InputReader.OnArrowInput -= OnArrowInput;
    }

    private void OnArrowInput(Vector2 value)
    {
        if (hasArrived || inputReceived || eventCalled) return;

        KeyCode pressedKey = KeyCode.None;

        if (value == Vector2.up)
            pressedKey = KeyCode.UpArrow;
        else if (value == Vector2.down)
            pressedKey = KeyCode.DownArrow;
        else if (value == Vector2.left)
            pressedKey = KeyCode.LeftArrow;
        else if (value == Vector2.right)
            pressedKey = KeyCode.RightArrow;

        if (pressedKey != inputKey)
        {
            Debug.Log("Tecla incorrecta: se esperaba " + inputKey + " pero se presionó " + pressedKey);
            HandleFail();
            return;
        }

        PlaySound();
        CheckInput();
    }
    private void HandleFail()
    {
        if (eventCalled) return;

        OnFail?.Invoke();
        InvokeEventOnce();
    }

    private void PlaySound()
    {
        Onsfx?.Invoke();
        //switch (inputKey)
        //{
        //    case KeyCode.UpArrow:
        //        Up.PlayOneShoot();
        //        break;
        //    case KeyCode.DownArrow:
        //        Down.PlayOneShoot();
        //        break;
        //    case KeyCode.LeftArrow:
        //        Left.PlayOneShoot();
        //        break;
        //    case KeyCode.RightArrow:
        //        Right.PlayOneShoot();
        //        break;
        //}
    }
}