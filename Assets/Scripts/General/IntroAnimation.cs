using UnityEngine;
using System.Collections;

public class IntroAnimation : MonoBehaviour
{
    [Header("Escalado")]
    [SerializeField] private float scaleMax = 1f;
    [SerializeField] private float duration = 4f;

    private Vector3 scaleInitial;
    private bool isAnimating;

    private void Awake()
    {
        scaleInitial = Vector3.one * 0.01f; // empieza muy pequeño pero no en 0
        transform.localScale = scaleInitial;
    }

    private void Start()
    {
        StartCoroutine(ScaleIn());
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            LoadSceneManager.LoadScene("a");
        }
    }

    private IEnumerator ScaleIn()
    {
        isAnimating = true;

        float timer = 0f;
        Vector3 targetScale = Vector3.one * scaleMax;

        while (timer < duration)
        {
            timer += Time.unscaledDeltaTime;
            float t = Mathf.Clamp01(timer / duration);

            float smoothT = t * t;

            transform.localScale = Vector3.LerpUnclamped(scaleInitial, targetScale, smoothT);
            yield return null;
        }

        transform.localScale = targetScale;
        isAnimating = false;
    }
}
