using UnityEngine;
using DG.Tweening;
public class DoScale : MonoBehaviour
{
    private enum Option
    {
        Transform,
        RectTransform
    }

    [Header("Characteristics")]
    private Vector2 scaleInitial;
    private RectTransform rectTransform;

    [Header("Dotween")]
    [SerializeField] private float scaleMultiplier = 1.2f;
    [SerializeField] private float duration = 0.5f;
    [SerializeField] private int repetitions;
    [SerializeField] private Ease ease;
    private Tween currentTween;

    [Header("Option")]
    [SerializeField] private Option option;

    private void Reset()
    {
        ease=Ease.InOutSine;
    }

    private void Start()
    {
        switch (option)
        {
            case Option.Transform:
                scaleInitial = transform.localScale;
                break;
            case Option.RectTransform:
                rectTransform = GetComponent<RectTransform>();
                scaleInitial = rectTransform.localScale;
                break;
        }
        BeginScale();
    }
    private void OnDestroy()
    {
        currentTween.Kill();
    }
    public void BeginScale(float scaleMultiplier)
    {
        switch (option)
        {
            case Option.Transform:
                currentTween?.Kill();
                currentTween = transform.DOScale(Vector3.one * scaleMultiplier, duration)
                    .SetEase(ease)
                    .SetUpdate(false);
                break;

            case Option.RectTransform:
                rectTransform = GetComponent<RectTransform>();
                currentTween?.Kill();
                currentTween = rectTransform.DOScale(Vector3.one * scaleMultiplier, duration)
                    .SetLoops(repetitions, LoopType.Yoyo)
                    .SetEase(ease)
                    .SetUpdate(UpdateType.Normal, true);
                break;
        }
    }
    public void BeginScale()
    {
        switch (option)
        {
            case Option.Transform:
                currentTween = transform.DOScale(Vector3.one * scaleMultiplier, duration)
            .SetLoops(repetitions, LoopType.Yoyo)
            .SetEase(ease).SetUpdate(true);
                break;
            case Option.RectTransform:
                rectTransform = GetComponent<RectTransform>();
                currentTween = rectTransform.DOScale(Vector3.one * scaleMultiplier, duration)
                .SetLoops(-1, LoopType.Yoyo)
                .SetEase(ease)
                .SetUpdate(true);
                break;
        }
    }
    public void ReturnScale()
    {
        switch (option)
        {
            case Option.Transform:
                currentTween = transform.DOScale(Vector3.one * scaleInitial, duration)
            .SetLoops(repetitions, LoopType.Yoyo)
            .SetEase(ease).SetUpdate(true);
                break;
            case Option.RectTransform:
                rectTransform = GetComponent<RectTransform>();
                currentTween = rectTransform.DOScale(Vector3.one * scaleInitial, duration)
                .SetLoops(-1, LoopType.Yoyo)
                .SetEase(ease)
                .SetUpdate(true);
                break;
        }
    }

}
