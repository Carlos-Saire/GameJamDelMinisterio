using TMPro;
using UnityEngine;

public class Invoker : Enemy
{
    [Header("EnemyPrefab")]
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int TimeGeneration;
    [SerializeField] private int countEnemy;


    protected override void Start()
    {
        base.Start();
        Invoke("GenerateEnemy", TimeGeneration);
    }
    protected override void Update()
    {
        base.Update();
    }
    private void UpdateCount(Enemy value)
    {
        --countEnemy;
        UpdateConbination();
        value.OnEventInvoked -= UpdateCount;
    }
    private void GenerateEnemy()
    {
        GameObject go=Instantiate(enemyPrefab, transform.transform.position, Quaternion.identity);
        go.GetComponent<Enemy>().OnEventInvoked += UpdateCount;
        ++countEnemy;
        UpdateConbination();
        Invoke("GenerateEnemy", TimeGeneration);
    }
    private void UpdateConbination()
    {
        if (countEnemy == 0)
        {
            spawnComandos.EnableComands(true);
            base.OnEnable();
        }
        else
        {
            spawnComandos.EnableComands(false);
            base.OnDisable();
        }
    }
}
