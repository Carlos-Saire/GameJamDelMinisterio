using UnityEngine;

public class MusicGameplay : MonoBehaviour
{
    [SerializeField] private AudioClipSO audioClipSO;
    private void OnEnable()
    {
        TrailRendererController.Onsfx += SFX;
    }
    private void OnDisable()
    {
        TrailRendererController.Onsfx -= SFX;
    }
    private void SFX()
    {
        audioClipSO.PlayOneShoot();
    }
}
