using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingSlider : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private AudioMixer mainMixer;
    [SerializeField] private string key;
    private float volume;

    [Header("Slider")]
    private Slider slider;

    [Header("Sprites")]
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private Image image;
    private void Awake()
    {
        slider = GetComponent<Slider>();
        slider.onValueChanged.AddListener(UpdateValue);
    }

    private void Start()
    {
        volume = PlayerPrefs.GetFloat(key, 1f); 
        slider.value = Mathf.Clamp01(volume);
        UpdateValue(volume);
    }
    private void OnApplicationQuit()
    {
        PlayerPrefs.Save();
    }

    private void UpdateValue(float value)
    {
        volume = Mathf.Clamp(value, 0.0001f, 1f);
        mainMixer.SetFloat(key, Mathf.Log10(volume) * 20);

        if (volume == 1)
        {
            image.sprite = sprites[2];
        }
        else if (volume >= 0.5f)
        {
            image.sprite = sprites[1];
        }
        else
        {
            image.sprite = sprites[0];
        }

        PlayerPrefs.SetFloat(key, volume);
    }
}
