using UnityEngine;
using System.Collections;
using System;
using Random = UnityEngine.Random;

public class PlayerGame : MonoBehaviour
{
    [Header("Gizmos")]
    [SerializeField] private float radius;

    [Header("EnemyPrefab")]
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private OrdasSO[] ordas;
    [SerializeField] private float delayBetweenOrdas;
    private int currentOrda;
    [SerializeField] private int countEnemy;

    [Header("Characteristics")]
    [SerializeField] private int live;
    public static event Action<int> OnlifePlayer;

    [Header("Buff")]
    [SerializeField] private Buff buff;

    public static event Action OnNewHorde;

    private Vector2[] allowedDirections = new Vector2[]
    {
        Vector2.left,
        Vector2.right,
        (Vector2.left + Vector2.up).normalized,
        (Vector2.left + Vector2.down).normalized,
        (Vector2.right + Vector2.up).normalized,
        (Vector2.right + Vector2.down).normalized
    };

    private void Awake()
    {
        StartCoroutine(WaitForGameManager());
    }

    private void Start()
    {
        StartCurrentOrda();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //buff.ActiveBuff();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            live--;
            OnlifePlayer?.Invoke(live);

            if (live <= 0)
            {
                GameManager.instance.Fail();
            }
        }
    }

    private void OnEnable()
    {
        BuffLife.OnBuffLife += UpdateLife;
        Prueba.OnCountEnemy += CountEnemy;
    }

    private void OnDisable()
    {
        BuffLife.OnBuffLife -= UpdateLife;
        Prueba.OnCountEnemy -= CountEnemy;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    private void StartCurrentOrda()
    {
        if (currentOrda < ordas.Length)
        {
            countEnemy = 0;
            OnNewHorde?.Invoke();
            StartCoroutine(GenerateEnemiesCoroutine());
        }
        else
        {
            //GameManager.instance.StartCinematica();
            Debug.Log("Las hordas han terminado.");
        }
    }

    private IEnumerator GenerateEnemiesCoroutine()
    {
        int enemiesToSpawn = ordas[currentOrda].countEnemy;
        float spawnDelay = ordas[currentOrda].timeGeneration;

        for (int i = 0; i < enemiesToSpawn; i++)
        {
            GenerateEnemy();
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    private void GenerateEnemy()
    {
        int indexPrefab = Random.Range(0, enemyPrefabs.Length);
        Vector2 direction = allowedDirections[Random.Range(0, allowedDirections.Length)];
        Vector3 spawnPosition = transform.position + (Vector3)(direction * radius);

        GameObject enemy = Instantiate(enemyPrefabs[indexPrefab], spawnPosition, Quaternion.identity);
        MonstruoController controller = enemy.GetComponent<MonstruoController>();
        controller.SetTarget(transform);
        controller.OnDead += UpdateCountEnemy;

        countEnemy++;
    }

    private void UpdateCountEnemy()
    {
        countEnemy--;

        if (countEnemy <= 0)
        {
            currentOrda++;
            Invoke(nameof(StartCurrentOrda), delayBetweenOrdas);
        }
    }

    private void UpdateLife(int value)
    {
        live += value;
        OnlifePlayer?.Invoke(live);
    }

    private int CountEnemy()
    {
        return countEnemy;
    }
    private IEnumerator WaitForGameManager()
    {
        yield return new WaitUntil(() => GameManager.instance != null);
        GameManager.instance.SetPlayer(transform);
    }
}
