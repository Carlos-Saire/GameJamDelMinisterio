using System;
using UnityEngine;
[RequireComponent (typeof(BoxCollider2D))]
public class Sectores : MonoBehaviour
{
    public static event Action<string> OnNameSector;
    [SerializeField] string nameSector;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            OnNameSector?.Invoke(nameSector);
        }
    }
}
