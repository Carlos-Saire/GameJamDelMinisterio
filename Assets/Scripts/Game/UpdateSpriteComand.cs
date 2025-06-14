using UnityEngine;
using UnityEngine.UI;

public class UpdateSpriteComand : MonoBehaviour
{
    private Image image;
    private void Awake()
    {
        image = GetComponent<Image>();
    }
    public void UpdateSprite(Sprite value)
    {
        image.sprite = value;
    }
}
