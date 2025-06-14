using UnityEngine;
using DG.Tweening;
using System;
public class DoMove : MonoBehaviour
{
    private enum Option
    {
        Transform,
        RectTransform
    }
    [Header("Characteristics")]
    private Vector2 positionInitial;
    private RectTransform rectTransform;

    [Header("Dotween")]
    [SerializeField] private Vector2 target;
    [SerializeField] private float time;
    [SerializeField] private Ease ease;
    private Tween currentTween;

    [Header("Option")]
    [SerializeField] private Option option;
    private void Start()
    {
        switch (option)
        {
            case Option.Transform:
                positionInitial = transform.position;
                break;
            case Option.RectTransform:
                rectTransform = GetComponent<RectTransform>();
                positionInitial = rectTransform.anchoredPosition;
                break;
        }
    }
    private void OnEnable()
    {
    }
    private void OnDisable()
    {
    }
    private void OnDestroy()
    {
        currentTween.Kill();
    }
    public void Go(Vector2 value)
    {
        switch (option)
        {
            case Option.Transform:
                currentTween = transform.DOMove(value, time).SetEase(ease).SetUpdate(true);
                break;
            case Option.RectTransform:
                currentTween = rectTransform.DOAnchorPos(value, time).SetEase(ease).SetUpdate(true);
                break;
        }
    }
    public void Go(params Action[] onCompleteActions)
    {
        switch (option)
        {
            case Option.Transform:
                currentTween = transform.DOMove(target, time)
                    .SetEase(ease).SetUpdate(true)
                    .OnComplete(() => InvokeActions(onCompleteActions));
                break;

            case Option.RectTransform:
                currentTween = rectTransform.DOAnchorPos(target, time)
                    .SetEase(ease).SetUpdate(true)
                    .OnComplete(() => InvokeActions(onCompleteActions));
                break;
        }
    }

    public void Return(params Action[] onCompleteActions)
    {
        switch (option)
        {
            case Option.Transform:
                currentTween = transform.DOMove(positionInitial, time)
                    .SetEase(ease).SetUpdate(true)
                    .OnComplete(() => InvokeActions(onCompleteActions));
                break;

            case Option.RectTransform:
                currentTween = rectTransform.DOAnchorPos(positionInitial, time)
                    .SetEase(ease).SetUpdate(true)
                    .OnComplete(() => InvokeActions(onCompleteActions));
                break;
        }
    }
    private void InvokeActions(Action[] actions)
    {
        foreach (var action in actions)
            action?.Invoke();
    }
    public void SetTarget(Vector2 value)
    {
        target = value;
    }
}
