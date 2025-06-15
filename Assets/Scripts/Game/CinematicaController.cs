using UnityEngine;

public class CinematicaController : MonoBehaviour
{
    [SerializeField] private GameObject[] cameras;
    private int currentCameraIndex ;

    private void Start()
    {
        GameManager.instance.SetCinematica(this);
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            NextCamera();
        }
    }
    public void StartCinematica()
    {
        NextCamera(); 
    }

    public void NextCamera()
    {
        if (currentCameraIndex >= 0 && currentCameraIndex < cameras.Length)
        {
            cameras[currentCameraIndex].SetActive(false);
        }

        currentCameraIndex++;

        if (currentCameraIndex < cameras.Length)
        {
            cameras[currentCameraIndex].SetActive(true);
        }
        else
        {
            Debug.Log("Cinemática terminada");
        }
    }
}
