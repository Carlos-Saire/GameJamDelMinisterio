using UnityEngine;
using System.Collections;

public class Boss : MonoBehaviour
{
    [Header("Delay")]
    [SerializeField] private float delay;
    private void Start()
    {
        GameManager.instance.SetBoos(this);
        gameObject.SetActive(false);
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
