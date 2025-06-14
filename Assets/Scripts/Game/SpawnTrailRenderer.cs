using UnityEngine;

public class SpawnTrailRenderer : MonoBehaviour
{
    [Header("Gizmos")]
    [SerializeField] private Color color;
    [SerializeField] private float Ditance;
    private void OnDrawGizmos()
    {
        Gizmos.color = color;
        Gizmos.DrawLine(transform.localPosition, Vector2.up * Ditance);
    }
    public void Generate(GameObject go)
    {

    }

}
