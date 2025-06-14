using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public abstract class MyButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    protected static bool interactue=true;
    protected Button button;
    public static bool IsInteractue => interactue;
    protected void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }
    protected abstract void OnClick();
    public void OnPointerEnter(PointerEventData eventData)
    {

    }
    public void OnPointerExit(PointerEventData eventData)
    {

    }
}
