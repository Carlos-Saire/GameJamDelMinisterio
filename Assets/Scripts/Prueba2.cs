using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(GridLayoutGroup))]
public class Prueba2 : MonoBehaviour
{
    [Header("Prefab")]
    [SerializeField] private GameObject prefabComand;

    [Header("Cantidad")]
    [SerializeField] private int cantidadComandos = 6;


    public void DrawComands(Sprite value)
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
        for (int i = 0; i < cantidadComandos; i++)
        {
            GameObject newCmd = Instantiate(prefabComand, transform);
            newCmd.GetComponent<UpdateSpriteComand>().UpdateSprite(value);
        }
    }
    private void OnDestroy()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }
}
