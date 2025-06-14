using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform target;
    public float velocidadCamara = 0.025f;
    public Vector3 desplazamiento;

    [Header("Límites de la cámara")]
    public Vector2 minClamp;
    public Vector2 maxClamp;
    private float currentX;
    private float currentY;
    private void Start()
    {
        target = GameManager.instance.GetTransformPlayer();
        transform.position = new Vector3(target.position.x,target.position.y,-10);
    }
    private void Update()
    {
        currentX = Mathf.Clamp(transform.position.x, minClamp.x, maxClamp.x);
        currentY = Mathf.Clamp(transform.position.y, minClamp.y, maxClamp.y);
        transform.position = new Vector3(currentX, currentY, -10);
    }
    private void FixedUpdate()
    {
        Vector3 posicionDeseada = target.position + desplazamiento;

        Vector3 posicionSuavizada = Vector3.Lerp(transform.position, posicionDeseada, velocidadCamara);

        transform.position = posicionSuavizada;
    }
}
