using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class DoFade : MonoBehaviour
{
    private enum Option
    {
        Image,
        TMP_Text,
        CanvasGroup,
        SpriteRenderer
    }

    private Image image;
    private TMP_Text text;
    private CanvasGroup canvasGroup;
    private SpriteRenderer spriteRenderer;

    [SerializeField] private float timeFadeIN;
    [SerializeField] private float timeFadeOut;

    public float TimeFadeIN => timeFadeIN;
    public float TimeFadeOut => timeFadeOut;
    [Header("Option")]
    [SerializeField] private Option option;

    private Tween currentTween; 

    private void Awake()
    {
        switch (option)
        {
            case Option.Image:
                image = GetComponent<Image>();
                break;
            case Option.TMP_Text:
                text = GetComponent<TMP_Text>();
                break;
            case Option.CanvasGroup:
                canvasGroup = GetComponent<CanvasGroup>();
                break;
            case Option.SpriteRenderer:
                spriteRenderer = GetComponent<SpriteRenderer>();
                break;
        }
    }

    private void OnDestroy()
    {
        if(currentTween != null && currentTween.IsActive())
        {
            currentTween.Kill(true); 
        }
    }

    public void FadeIN()
    {
        if (image != null) currentTween = ConfirmFadeIN(image);
        else if (text != null) currentTween = ConfirmFadeIN(text);
        else if (canvasGroup != null) currentTween = ConfirmFadeIN(canvasGroup);
        else if (spriteRenderer != null) currentTween = ConfirmFadeIN(spriteRenderer);
    }

    public void FadeOut()
    {
        if (image != null) currentTween = ConfirmFadeOut(image);
        else if (text != null) currentTween = ConfirmFadeOut(text);
        else if (canvasGroup != null) currentTween = ConfirmFadeOut(canvasGroup);
        else if (spriteRenderer != null) currentTween = ConfirmFadeOut(spriteRenderer);
    }

    private Tween ConfirmFadeIN(Image img) => img.DOFade(1, timeFadeIN).SetEase(Ease.Linear);
    private Tween ConfirmFadeOut(Image img) => img.DOFade(0, timeFadeOut).SetEase(Ease.Linear);

    private Tween ConfirmFadeIN(TMP_Text txt) => txt.DOFade(1, timeFadeIN).SetEase(Ease.Linear);
    private Tween ConfirmFadeOut(TMP_Text txt) => txt.DOFade(0, timeFadeOut).SetEase(Ease.Linear);

    private Tween ConfirmFadeIN(CanvasGroup canva) => canva.DOFade(1, timeFadeIN).SetEase(Ease.Linear);
    private Tween ConfirmFadeOut(CanvasGroup canva) => canva.DOFade(0, timeFadeOut).SetEase(Ease.Linear);

    private Tween ConfirmFadeIN(SpriteRenderer sprite) => sprite.DOFade(1, timeFadeIN).SetEase(Ease.Linear);
    private Tween ConfirmFadeOut(SpriteRenderer sprite) => sprite.DOFade(0, timeFadeOut).SetEase(Ease.Linear);
}
