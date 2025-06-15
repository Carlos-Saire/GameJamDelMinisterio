using System;
using UnityEngine;
public class MuquiController : Boss
{
    [Header("Position Initial")]
    private Vector2 positionInitial;

    [Header("Characteristc")]
    [SerializeField] private float timeToTarget;
    [SerializeField] private float timeReturn;
    [SerializeField] private bool isLife=true;
    [SerializeField] private bool isReturn;
    private float distance;
    [SerializeField] private float speed;
    [SerializeField] private float speedRetund;

    [SerializeField] private int life;
    public static event Action<float> OnStart;
    protected override void Start()
    {
        base.Start();
        positionInitial = transform.position;
        distance = Vector2.Distance(transform.position, player.position);
        speed = distance / timeToTarget;
        OnStart?.Invoke(timeToTarget);
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
        }
        if (!isLife)
            return;
        if (isReturn)
        {
            if (Vector2.Distance(transform.position, positionInitial) < 0.1f)
            {
                isReturn = false;
            }
            Destination(positionInitial,speedRetund);
        }
        else
        {
            Destination(player.position,speed);
        }
    }
    private void OnEnable()
    {
        Prueba.OnCountEnemy += GetCount;
        Prueba.OnRepete += Regresar;
    }
    private void OnDisable()
    {
        Prueba.OnCountEnemy -= GetCount;
        Prueba.OnRepete -= Regresar;
    }
    private int GetCount()
    {
        return life;
    }
    private void Destination(Vector2 value,float speed)
    {
        transform.position = Vector2.MoveTowards(transform.position, value, speed*Time.deltaTime);
    }
    private void Regresar()
    {
        isReturn = true;
        --life;
        distance = Vector2.Distance(transform.position, positionInitial);
        speedRetund = distance / timeReturn;
        if(life <= 0)
        {
            isLife=false;
            //GameManager.instance.Win();
        }
    }

}
