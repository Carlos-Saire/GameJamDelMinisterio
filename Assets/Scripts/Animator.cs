using UnityEngine;

public class Animatorcontrol : MonoBehaviour
{
    [SerializeField]private Animator animator;
    [SerializeField] private SpriteRenderer renderer;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            animator.SetBool("Right", true);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            animator.SetBool("Right", false);
        }
        if (Input.GetKey(KeyCode.W))
        {
            animator.SetBool("Up", true);
        }
        else
        {
            animator.SetBool("Up", false);
        }
        
        if (Input.GetKey(KeyCode.S))
        {
            animator.SetBool("Down", true);
        }
        else
        {
            animator.SetBool("Down", false);
        }
        bool isWalking = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) ||Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D);
        animator.SetBool("Walk", isWalking);
        if (animator.GetBool("Right"))
        {
           
        }
    }
}
