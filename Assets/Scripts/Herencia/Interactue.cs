using UnityEngine;
[RequireComponent (typeof(BoxCollider2D))]
public abstract class Interactue : MonoBehaviour
{
    public void Input(bool value)
    {
        if(value)
        {
            InputReader.OnInputInteractue += Interaction;
        }
        else
        {
            InputReader.OnInputInteractue -= Interaction;
        }
    }
    protected abstract void Interaction();
    protected void UpdateLayer(string layerName)
    {
        int layerIndex = LayerMask.NameToLayer(layerName);
        if (layerIndex != -1)
        {
            gameObject.layer = layerIndex;
        }
        else
        {
            Debug.LogWarning("Layer no válido: " + layerName);
        }
    }
}
