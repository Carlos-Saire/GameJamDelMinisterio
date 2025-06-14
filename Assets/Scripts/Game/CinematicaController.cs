using System.Collections;
using UnityEngine;

public class CinematicaController : MonoBehaviour
{
    [SerializeField] private GameObject[] cameras;
    [SerializeField] private float delay = 2f;

    private int currentCameraIndex;

    private void Start()
    {
        GameManager.instance.SetCinematica(this);
        StartCinematica();
    }

    public void StartCinematica()
    {
        StartCoroutine(CinematicaRoutine());
    }

    private IEnumerator CinematicaRoutine()
    {
        while (currentCameraIndex < cameras.Length - 1)
        {
            if (currentCameraIndex >= 0 && currentCameraIndex < cameras.Length)
            {
                cameras[currentCameraIndex].SetActive(false);
            }

            currentCameraIndex++;

            cameras[currentCameraIndex].SetActive(true);

            yield return new WaitForSeconds(delay);
        }

        Debug.Log("Cinemática terminada");
    }
}
