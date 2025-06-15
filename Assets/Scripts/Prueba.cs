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

    public static event Action OnRepete;
    public static event Func<int> OnCountEnemy;

    private float Timeboss;

    private void Start()
    {
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
        MuquiController.OnStart += OnNewHordeStart;
    }

    private void OnDisable()
    {
        LoadSceneManager.OnLoadScene -= ResetValues;
        PlayerGame.OnNewHorde -= OnNewHordeStart;
        MuquiController.OnStart -= OnNewHordeStart;
    }

    private void OnDestroy()
    {
        if (currentTrail != null)
            currentTrail.OnKeyPressed -= OnCurrentNoteKeyPressed;
    }

    private void ResetValues()
    {
        shuffledCombination.Clear();
        CurrentPositon = 0;
    }

    private void OnNewHordeStart()
    {
        GenerateRandomCombination();
        StartCoroutine(GenerateAllNotes());
    }

    private void OnNewHordeStart(float timeToTarget)
    {
        GenerateRandomCombination();
        StartCoroutine(GenerateAllNotes(timeToTarget));
        Timeboss = timeToTarget;
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
        return key switch
        {
            KeyCode.UpArrow => arrowUp,
            KeyCode.DownArrow => arrowDown,
            KeyCode.LeftArrow => arrowLeft,
            KeyCode.RightArrow => arrowRight,
            _ => null,
        };
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
        Vector2 spawnPos = target.anchoredPosition + new Vector2(0f, offsetY);
        noteRect.anchoredPosition = spawnPos;

        TrailRendererController controller = noteObj.GetComponent<TrailRendererController>();
        controller.SetTarget(target, shuffledCombination[CurrentPositon]);

        ++CurrentPositon;
        trails.Enqueue(controller);

        if (trails.Count == 1 && currentTrail == null)
            TryActivateNextNote();
    }

    private IEnumerator GenerateAllNotes(float timeToTarget)
    {
        trails.Clear();
        for (int i = 0; i < childRects.Count; i++)
        {
            RectTransform target = childRects[i];
            GenerateNote(target, timeToTarget);
            yield return new WaitForSeconds(delay);
        }
    }

    private void GenerateNote(RectTransform target, float timeToTarget)
    {
        GameObject noteObj = Instantiate(notePrefab, target.parent);
        RectTransform noteRect = noteObj.GetComponent<RectTransform>();

        noteRect.pivot = target.pivot;
        Vector2 spawnPos = target.anchoredPosition + new Vector2(0f, offsetY);
        noteRect.anchoredPosition = spawnPos;

        TrailRendererController controller = noteObj.GetComponent<TrailRendererController>();
        float speed = timeToTarget / 4;
        controller.SetTarget(target, shuffledCombination[CurrentPositon], speed);

        ++CurrentPositon;
        trails.Enqueue(controller);

        if (trails.Count == 1 && currentTrail == null)
            TryActivateNextNoteBoss(); 
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

    private void TryActivateNextNoteBoss()
    {
        if (trails.Count == 0)
        {
            currentTrail = null;
            return;
        }

        currentTrail = trails.Dequeue();
        currentTrail.OnKeyPressed += OnCurrentNoteKeyPressedBoss;
        currentTrail.StartMove();
    }

    private void OnCurrentNoteKeyPressed()
    {
        currentTrail.OnKeyPressed -= OnCurrentNoteKeyPressed;
        TryActivateNextNote();

        if (trails.Count == 0 && currentTrail == null)
        {
            OnRepete?.Invoke();
            StartCoroutine(CheckEnemyAndRepeat());
        }
    }

    private void OnCurrentNoteKeyPressedBoss()
    {
        currentTrail.OnKeyPressed -= OnCurrentNoteKeyPressedBoss;
        TryActivateNextNoteBoss(); 

        if (trails.Count == 0 && currentTrail == null)
        {
            OnRepete?.Invoke();
            StartCoroutine(CheckEnemyAndRepeatBoss());
        }
    }

    private IEnumerator CheckEnemyAndRepeat()
    {
        yield return null;

        int count = OnCountEnemy?.Invoke() ?? 0;
        if (count > 0)
        {
            GenerateRandomCombination();
            StartCoroutine(GenerateAllNotes());
        }
    }

    private IEnumerator CheckEnemyAndRepeatBoss()
    {
        yield return null;

        int count = OnCountEnemy?.Invoke() ?? 0;
        if (count > 0)
        {
            GenerateRandomCombination();
            StartCoroutine(GenerateAllNotes(Timeboss));
        }
    }
}
