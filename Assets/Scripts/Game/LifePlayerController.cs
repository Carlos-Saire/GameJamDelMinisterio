using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifePlayerController : MonoBehaviour
{
    [SerializeField] private List<Image> childImages = new List<Image>();
    [SerializeField] private int vida;

    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            Image fort = child.GetComponent<Image>();
            if (fort != null)
            {
                childImages.Add(fort);
            }
        }
    }
    private void OnEnable()
    {
        PlayerGame.OnlifePlayer += UpdateLive;
        TrailRendererController.OnFail += UpdateLive;
    }
    private void OnDisable()
    {
        PlayerGame.OnlifePlayer -= UpdateLive;
        TrailRendererController.OnFail -= UpdateLive;
    }
    private void UpdateLive()
    {
        --vida;
        UpdateLive(vida);
        if (vida <= 0)
        {
            GameManager.instance.Fail();
        }
    }
    private void UpdateLive(int value)
    {
        for (int i = 0; i < childImages.Count; ++i)
        {
            childImages[i].enabled = i < value;
        }
    }
}
