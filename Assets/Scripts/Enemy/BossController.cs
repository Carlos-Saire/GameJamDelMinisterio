using System;
using UnityEngine;
using Random = UnityEngine.Random;
public class BossController : Enemy
{
    public static event Action<int> OnHordeFinished;

    [Header("EnemyPrefab")]
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private int countEnemy;
    [SerializeField] private int TimeGeneration;
    private int indexPrefab;

    protected override void Awake()
    {
        base.Awake();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void Start()
    {
        OnHordeFinished?.Invoke(countEnemy);
        Invoke("GenerateEnemy", TimeGeneration);
    }
    protected override void Update()
    {

    }
    protected override void Dead()
    {

    }
    private void GenerateEnemy()
    {
        --countEnemy;
        indexPrefab = Random.Range(0, enemyPrefabs.Length - 1);
        Instantiate(enemyPrefabs[indexPrefab],transform.transform.position,Quaternion.identity);
        if(countEnemy > 0)
            Invoke("GenerateEnemy", TimeGeneration);
    }
}
