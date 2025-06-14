using System.Collections;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [Header("Music")]
    [SerializeField] private AudioSource costa;
    [SerializeField] private AudioSource sierra;
    [SerializeField] private AudioSource selva;
    [SerializeField] private string nameSector;
    [SerializeField] private float timeOff = 2f;

    private AudioSource currentSource;       

    private void Start()
    {
        UpdateSector(nameSector);
    }

    private void OnEnable()
    {
        Sectores.OnNameSector += UpdateSector;
    }

    private void OnDisable()
    {
        Sectores.OnNameSector -= UpdateSector;
    }

    private void UpdateSector(string value)
    {
        AudioSource nextSource = null;

        switch (value)
        {
            case "costa":
                nextSource = costa;
                break;
            case "sierra":
                nextSource = sierra;
                break;
            case "selva":
                nextSource = selva;
                break;
        }

        if (nextSource != null && nextSource != currentSource)
        {
            StartCoroutine(ChangeMusic(nextSource));
        }
    }

    private IEnumerator ChangeMusic(AudioSource nextSource)
    {
        float t = 0f;
        float startVolOff = currentSource != null ? currentSource.volume : 1f;

        nextSource.volume = 0f;
        nextSource.Play();

        while (t < timeOff)
        {
            t += Time.deltaTime;
            float progress = t / timeOff;

            if (currentSource != null)
                currentSource.volume = Mathf.Lerp(startVolOff, 0f, progress);

            nextSource.volume = Mathf.Lerp(0f, 1f, progress);

            yield return null;
        }

        if (currentSource != null)
        {
            currentSource.volume = 0f;
            currentSource.Stop();
        }

        nextSource.volume = 1f;
        currentSource = nextSource;
    }
}
