using System;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static event Action OnStartMove;
    private void Start()
    {
        //OnStartMove?.Invoke();
    }
}
