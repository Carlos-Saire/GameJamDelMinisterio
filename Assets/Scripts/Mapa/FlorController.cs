using System;
using UnityEngine;

public class FlorController : Item
{
    public static event Action OnInteractue;
    private bool isInteraction;
    protected override void Interaction()
    {
        if(isInteraction)
        {
            //base.Interaction();
            OnInteractue?.Invoke();
        }
        else
        {
            OnInteractue?.Invoke();
            isInteraction = true;
        }
    }
}
