using UnityEngine;
using UnityEngine.UI;

public class BarController : MonoBehaviour
{
    [Header("Boss Data")]
    private int countEnemy;

    [Header("Characteristics")]
    private Image image;
    private int coundEliminated;
    private void Awake()
    {
        image = GetComponent<Image>();
    }
    private void OnEnable()
    {
        Enemy.OnEventDead += UpdateCountEnemy;
        BossController.OnHordeFinished += SetCountEnemy;
    }
    private void OnDisable()
    {
        Enemy.OnEventDead -= UpdateCountEnemy;
        BossController.OnHordeFinished -= SetCountEnemy;
    }
    private void UpdateCountEnemy()
    {
        --coundEliminated;
        UpdateBar();
    }
    private void UpdateBar()
    {
        image.fillAmount =(float)coundEliminated / countEnemy;
    }
    private void SetCountEnemy(int count)
    {
        countEnemy= count;
        coundEliminated = count;
        UpdateBar();
    }
}
