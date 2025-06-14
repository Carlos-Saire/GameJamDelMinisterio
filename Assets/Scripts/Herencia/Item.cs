using UnityEngine;

public class Item : Interactue
{
    protected override void Interaction()
    {
        Destroy(gameObject);
    }
}
