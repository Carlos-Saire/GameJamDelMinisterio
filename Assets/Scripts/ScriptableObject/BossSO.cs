using UnityEngine;
[CreateAssetMenu(fileName = "BossSO", menuName = "Scriptable Objects/Boss/BossSO", order = 1)]
public class BossSO : MonoBehaviour
{
    [Header("Tabla")]
    [SerializeField] private float time;
    [SerializeField] private float Intentos;

    [Header("Speed")]
    [SerializeField] private float Time;
}
