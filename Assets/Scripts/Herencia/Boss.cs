using UnityEngine;
using System.Collections;

public class Boss : MonoBehaviour
{
    [Header("Delay")]
    [SerializeField] private float delay;

    [Header("Player Data")]
    [SerializeField] protected Transform player;


    protected virtual void Start()
    {
        GameManager.instance.SetBoos(this);
        //gameObject.SetActive(false);
        //player=GameManager.instance.GetTransformPlayer();
    }

    public void StartCombat()
    {
        StartCoroutine(DelayCinematica());
    }
    private IEnumerator DelayCinematica()
    {
        yield return new WaitForSeconds(delay);
        GameManager.instance.StartCinematica();
    }
}
