using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "AudioClipSO", menuName = "Scriptable Objects/Audio/AudioClipSO", order = 1)]
public class AudioClipSO : ScriptableObject
{
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private AudioMixerGroup audioMixerGroup;
    [Range(0,1)][SerializeField] private float volume;
    [Range(0,1)][SerializeField] private float pitch;
    private void Reset()
    {
        volume = 1;
        pitch = 1;
    }
    public void PlayOneShoot()
    {
        GameObject audioObject = new GameObject();
        AudioSource audioSource = audioObject.AddComponent<AudioSource>();

        audioSource.clip = audioClip;
        audioSource.volume = volume;
        audioSource.pitch = pitch;
        audioSource.outputAudioMixerGroup = audioMixerGroup;

        audioSource.Play();
        Destroy(audioObject, audioClip.length);
    }
}