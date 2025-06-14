using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class Prueba : MonoBehaviour
{
    [SerializeField] private List<RectTransform> childRects = new List<RectTransform>();
    [SerializeField] private GameObject notePrefab;
    [SerializeField] private KeyCode KeyCode;
    [SerializeField] private float delay = 0.5f;
    [SerializeField] private float offsetY = 100f;

    private Queue<TrailRendererController> trails = new Queue<TrailRendererController>();
    private TrailRendererController currentTrail;

    [Header("Sprites")]
    [SerializeField] private Sprite arrowUp;
    [SerializeField] private Sprite arrowDown;
    [SerializeField] private Sprite arrowLeft;
    [SerializeField] private Sprite arrowRight;

    [Header("Keys")]
    [SerializeField] private KeyCode[] combinations;
    [SerializeField] private List<KeyCode> shuffledCombination = new List<KeyCode>();
    private int CurrentPositon;

    [Header("Prueba2")]
    [SerializeField] private Prueba2[] prueba;

    [SerializeField] private int repete;
    private int currentRepeatCount = 0;

    public static event Action OnRepete;

    private void Start()
    {
        currentRepeatCount = 0;

        for (int i = 0; i < transform.childCount; i++)
        {
            RectTransform rect = transform.GetChild(i).GetComponent<RectTransform>();
            childRects.Add(rect);
        }
    }

    private void OnEnable()
    {
        LoadSceneManager.OnLoadScene += ResetValues;
        PlayerGame.OnNewHorde += OnNewHordeStart;
    }

    private void OnDisable()
    {
        LoadSceneManager.OnLoadScene -= ResetValues;
        PlayerGame.OnNewHorde -= OnNewHordeStart;
    }

    private void OnDestroy()
    {
        if (currentTrail != null)
        {
            currentTrail.OnKeyPressed -= OnCurrentNoteKeyPressed;
        }
    }

    private void ResetValues()
    {
        shuffledCombination.Clear();
        CurrentPositon = 0;
    }

    private void OnNewHordeStart()
    {
        StopAllCoroutines();
        shuffledCombination.Clear();
        CurrentPositon = 0;
        GenerateRandomCombination();
        StartCoroutine(GenerateAllNotes());
    }

    public void GenerateRandomCombination()
    {
        shuffledCombination.Clear();
        CurrentPositon = 0;

        if (combinations.Length == 0) return;

        for (int i = 0; i < childRects.Count; i++)
        {
            int randomIndex = Random.Range(0, combinations.Length);
            KeyCode selectedKey = combinations[randomIndex];
            prueba[i].DrawComands(GetSpriteForKey(selectedKey));
            shuffledCombination.Add(selectedKey);
        }
    }

    private Sprite GetSpriteForKey(KeyCode key)
    {
        switch (key)
        {
            case KeyCode.UpArrow: return arrowUp;
            case KeyCode.DownArrow: return arrowDown;
            case KeyCode.LeftArrow: return arrowLeft;
            case KeyCode.RightArrow: return arrowRight;
            default: return null;
        }
    }

    private IEnumerator GenerateAllNotes()
    {
        trails.Clear();
        for (int i = 0; i < childRects.Count; i++)
        {
            RectTransform target = childRects[i];
            GenerateNote(target);
            yield return new WaitForSeconds(delay);
        }
    }

    private void GenerateNote(RectTransform target)
    {
        GameObject noteObj = Instantiate(notePrefab, target.parent);
        RectTransform noteRect = noteObj.GetComponent<RectTransform>();

        noteRect.pivot = target.pivot;
        Vector2 targetAnchoredPos = target.anchoredPosition;
        Vector2 spawnAnchoredPos = targetAnchoredPos + new Vector2(0f, offsetY);
        noteRect.anchoredPosition = spawnAnchoredPos;

        TrailRendererController controller = noteObj.GetComponent<TrailRendererController>();
        controller.SetTarget(target, shuffledCombination[CurrentPositon]);

        ++CurrentPositon;
        trails.Enqueue(controller);

        if (trails.Count == 1 && currentTrail == null)
        {
            TryActivateNextNote();
        }
    }

    private void TryActivateNextNote()
    {
        if (trails.Count == 0)
        {
            currentTrail = null;
            return;
        }

        currentTrail = trails.Dequeue();
        currentTrail.OnKeyPressed += OnCurrentNoteKeyPressed;
        currentTrail.StartMove();
    }

    private void OnCurrentNoteKeyPressed()
    {
        currentTrail.OnKeyPressed -= OnCurrentNoteKeyPressed;
        TryActivateNextNote();

        if (trails.Count == 0 && currentTrail == null)
        {
            currentRepeatCount++;
            Debug.Log("Se completó la ronda " + currentRepeatCount);

            if (currentRepeatCount < repete)
            {
                OnRepete?.Invoke();
                GenerateRandomCombination();
                StartCoroutine(GenerateAllNotes());
            }
            else
            {
                Debug.Log("¡Todas las repeticiones han terminado!");
            }
        }
    }
}
