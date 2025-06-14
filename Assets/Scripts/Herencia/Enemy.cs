using System;
using UnityEngine;
[RequireComponent (typeof(Rigidbody2D))]
[RequireComponent (typeof(BoxCollider2D))]
public abstract class Enemy : MonoBehaviour
{
    public static event Action OnEventDead;
    public event Action<Enemy> OnEventInvoked;

    [Header("Player Data")]
    protected static Transform player;

    [Header("Characteristics")]
    private Rigidbody2D rb2D;
    [SerializeField] private float timeTarget;
    private float distanceTarget;
    private float speed;

    [Header("CombinationSO")]
    [SerializeField] private CombinationSO combinations;
    private CombinationSO currentConbinations;

    [Header("SpawnComandos")]
    protected SpawnComandos spawnComandos;

    protected virtual void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        spawnComandos = GetComponentInChildren<SpawnComandos>();
        currentConbinations = Instantiate(combinations);
    }
    protected virtual void Start()
    {
        if (player == null)
        {
            player = GameManager.instance.GetTransformPlayer();
        }
        distanceTarget = Vector2.Distance(transform.position, player.position);
        speed = distanceTarget / timeTarget;
        spawnComandos.SetDataCombinationSO(currentConbinations);
    }
    protected virtual void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
    }
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
    }
    protected virtual void OnEnable()
    {
        currentConbinations.OnFinishCombination += Dead;
        //InputReader.OnInputArrow += OnArrowInput;
        BuffArrows.OnArrowBuff += OnBuffArrowUsed;
    }
    protected virtual void OnDisable()
    {
        currentConbinations.OnFinishCombination -= Dead;
        //InputReader.OnInputArrow -= OnArrowInput;
        BuffArrows.OnArrowBuff -= OnBuffArrowUsed;
    }
    protected virtual void Dead()
    {
        OnEventDead?.Invoke();
        OnEventInvoked?.Invoke(this);
        Destroy(gameObject);
    }
    public void SetSpeed(float value)
    {
        if (player == null)
        {
            player = GameManager.instance.GetTransformPlayer();
        }
        distanceTarget = Vector2.Distance(transform.position, player.position);
        speed = distanceTarget / value;
    }
    private void OnArrowInput(Vector2 direction)
    {
        if (direction == Vector2.up)
            currentConbinations.UpdateCombinations(KeyCode.UpArrow);
        else if (direction == Vector2.down)
            currentConbinations.UpdateCombinations(KeyCode.DownArrow);
        else if (direction == Vector2.left)
            currentConbinations.UpdateCombinations(KeyCode.LeftArrow);
        else if (direction == Vector2.right)
            currentConbinations.UpdateCombinations(KeyCode.RightArrow);
    }
    private void OnBuffArrowUsed()
    {
        if (currentConbinations.HasRemainingKeys)
        {
            currentConbinations.SkipNextKey(); 
        }
    }
}
