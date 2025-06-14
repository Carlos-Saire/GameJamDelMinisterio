using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(DoFade))]
public class MonstruoController : MonoBehaviour
{
    public event Action OnDead;

    [Header("Target")]
    [SerializeField] private Transform target;
    [SerializeField] private float timeTarget = 3f;
    [SerializeField] private float retrocesoDistancia = 1f;
    [SerializeField] private float retrocesoDuracion = 0.2f;
    [SerializeField] private int maxVida = 3;

    private int vidaActual;
    private float speed;
    private Rigidbody2D rb;
    private bool enRetroceso = false;
    private Vector2 puntoRetroceso;

    [Header("Dofade")]
    private DoFade fade;

    [Header("SpriteRenderer")]
    private SpriteRenderer spriteRenderer;

    [Header("Characteristis")]
    [SerializeField] private DoFade doFadeCanvas;
    private TMP_Text text_Vida;

    private void Awake()
    {
        fade=GetComponent<DoFade>();
        spriteRenderer=GetComponent<SpriteRenderer>();
        text_Vida = GetComponentInChildren<TMP_Text>();
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        vidaActual = maxVida;

        float distanceTarget = Vector2.Distance(transform.position, target.position);
        speed = distanceTarget / timeTarget;
        text_Vida.text = vidaActual.ToString();
    }
    private void Update()
    {
        if (vidaActual <= 0)
        {
            OnDead?.Invoke();
            fade.FadeOut();
            doFadeCanvas.FadeOut();
            Destroy(gameObject,fade.TimeFadeOut+0.5f);
            return;
        }

        if (target.position.x > transform.position.x)
            spriteRenderer.flipX = true;
        else
            spriteRenderer.flipX = false;

        if (!enRetroceso)
        {
            Vector2 nuevaPos = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            rb.MovePosition(nuevaPos);
        }
        else
        {
            Vector2 nuevaPos = Vector2.MoveTowards(transform.position, puntoRetroceso, speed * 2 * Time.deltaTime);
            rb.MovePosition(nuevaPos);

            if (Vector2.Distance(transform.position, puntoRetroceso) < 0.05f)
                enRetroceso = false;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            RecibirGolpe();
            //Destroy(gameObject);
        }
    }
    private void OnEnable()
    {
        Prueba.OnRepete += RecibirGolpe;
    }
    private void OnDisable()
    {
        Prueba.OnRepete -= RecibirGolpe;
    }
    private void OnDestroy()
    {
        OnDead = null;
    }
    public void SetTarget(Transform value)
    {
        target = value;
    }
    private void RecibirGolpe()
    {
        if (vidaActual <= 0 || enRetroceso)
            return;

        vidaActual--;
        text_Vida.text = vidaActual.ToString();
        Vector2 direccion = (transform.position - target.position).normalized;
        puntoRetroceso = (Vector2)transform.position + direccion * retrocesoDistancia;

        enRetroceso = true;

        Invoke(nameof(FinRetroceso), retrocesoDuracion);
    }
    private void FinRetroceso()
    {
        enRetroceso = false;
    }
}
