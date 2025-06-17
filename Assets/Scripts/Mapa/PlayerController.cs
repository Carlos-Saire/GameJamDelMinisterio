using System.Collections.Generic;
using TMPro;
using UnityEngine;
[RequireComponent (typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Characteristics")]
    private Rigidbody2D rb2D;
    private Vector2 inputMove;
    [SerializeField] private float speed;

    [Header("CircleCast")]
    [SerializeField] private float radius;
    [SerializeField] private LayerMask layer;

    [Header("Interactue Data")]
    [SerializeField]private List<Interactue> interactueList;
    private Interactue currentInteractue;
    private float minDistance;
    private float currentDistance;

    [Header("ParticleSystem Data")]
    private ParticleSystem particle;

    [Header("Text Data")]
   [SerializeField] private TMP_Text text; 
    private Animator animatoranimator;
    private SpriteRenderer spriteRenderer;
    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        particle = GetComponentInChildren<ParticleSystem>();
        text = GetComponentInChildren<TMP_Text>();
        GameManager.instance.SetPlayer(this.transform,text);
        animatoranimator=GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        //text.enabled = false;
        GetPosition();
        particle.Stop();
    }
    private void Update()
    {
        LookForInteractue();
    }
    private void FixedUpdate()
    {
        if(currentInteractue == null)
        {
            Collider2D[] overlaps = Physics2D.OverlapCircleAll(transform.position, radius, layer);
            interactueList.Clear();
            for (int i = 0; i < overlaps.Length; ++i)
            {
                Interactue interactue = overlaps[i].GetComponent<Interactue>();
                interactueList.Add(interactue);
            }
        }
        rb2D.linearVelocity = inputMove * speed;
    }
    private void OnEnable()
    {
        InputReader.OnInputMove += SetMove;
    }
    private void OnDisable()
    {
        InputReader.OnInputMove -= SetMove;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, radius);
    }
    private void OnApplicationQuit()
    {
        PlayerPrefs.DeleteKey("PositionX");
        PlayerPrefs.DeleteKey("PositionY");
    }
    private void SetMove(Vector2 value)
    {
        inputMove = value;
        Animaiton(value);
        if (value ==Vector2.zero)
        {
            particle.Stop();
        }
        else
        {
            particle.Play();
        }
    }
    private void Animaiton(Vector2 value)
    {
        if (value.magnitude < 0.1f)
        {
            animatoranimator.Play("Idle");
            return;
        }

        if (Mathf.Abs(value.x) > Mathf.Abs(value.y))
        {
            animatoranimator.Play("Derecha");
            spriteRenderer.flipX = value.x < 0;
        }
        else
        {
            if (value.y > 0)
            {
                animatoranimator.Play("Arriba");
            }
            else
            {
                animatoranimator.Play("Abajo");
            }
        }
    }

    private void SavePostion()
    {
        PlayerPrefs.SetFloat("PositionX",transform.position.x);
        PlayerPrefs.SetFloat("PositionY",transform.position.y);
        PlayerPrefs.Save();
    }
    private void GetPosition()
    {
        float positionX = PlayerPrefs.GetFloat("PositionX",transform.position.x);
        float positionY = PlayerPrefs.GetFloat("PositionY", transform.position.y);
        transform.position=new Vector2 (positionX,positionY);
    }
    private void LookForInteractue()
    {
        if (currentInteractue == null)
        {
            if (interactueList == null || interactueList.Count == 0) return;
            minDistance = float.MaxValue;
            for (int i = 0; i < interactueList.Count; ++i)
            {
                currentDistance = Vector2.Distance(transform.position, interactueList[i].gameObject.transform.position);
                if (minDistance > currentDistance)
                {
                    minDistance = currentDistance;
                    currentInteractue = interactueList[i];
                }
            }
            currentInteractue.Input(true);
        }
        else
        {
            if(Vector2.Distance(currentInteractue.gameObject.transform.position,transform.position)> radius)
            {
                currentInteractue.Input(false);
                currentInteractue = null;
            }
        }
    }
}
