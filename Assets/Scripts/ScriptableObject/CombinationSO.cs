using System;
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "CombinationSO", menuName = "Scriptable Objects/CombinationsEnemy/CombinationSO")]
public class CombinationSO : ScriptableObject
{
    [SerializeField] private KeyCode[] combinations; 
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private int combinacionesCantidad; 

    private Queue<KeyCode> currentCombinationQueue = new Queue<KeyCode>();
    private List<KeyCode> shuffledCombination = new List<KeyCode>();

    public List<KeyCode> OriginalKeys => shuffledCombination;
    public Sprite[] Sprites => sprites;

    public event Action OnFinishCombination;
    public event Action OnUpdateCombination;

    public bool HasRemainingKeys => currentCombinationQueue.Count > 0;
    public void GenerateRandomCombination()
    {
        shuffledCombination.Clear();
        currentCombinationQueue.Clear();

        if (combinations.Length == 0)
        {
            return;
        }

        for (int i = 0; i < combinacionesCantidad; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, combinations.Length);
            KeyCode selectedKey = combinations[randomIndex];
            shuffledCombination.Add(selectedKey);
            currentCombinationQueue.Enqueue(selectedKey);
        }

    }

    public void UpdateCombinations(KeyCode keyCode)
    {
        if (currentCombinationQueue.Count == 0)
            GenerateRandomCombination();

        if (currentCombinationQueue.Peek() == keyCode)
        {
            currentCombinationQueue.Dequeue();
            OnUpdateCombination?.Invoke();

            if (currentCombinationQueue.Count == 0)
                OnFinishCombination?.Invoke();
        }
    }

    public Sprite GetSpriteForKey(KeyCode keyCode)
    {
        for (int i = 0; i < combinations.Length; i++)
        {
            if (combinations[i] == keyCode)
                return sprites.Length > i ? sprites[i] : null;
        }
        return null;
    }
    public KeyCode PeekNextKey()
    {
        if (currentCombinationQueue.Count > 0)
            return currentCombinationQueue.Peek();
        else
            throw new InvalidOperationException("No hay teclas restantes en la combinación.");
    }

    public void SkipNextKey()
    {
        if (currentCombinationQueue.Count > 0)
            UpdateCombinations(currentCombinationQueue.Peek());
    }
}
