using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

[RequireComponent(typeof(GridLayoutGroup))]
public class SpawnComandos : MonoBehaviour
{
    [Header("Prefab")]
    [SerializeField] private GameObject prefabComand;

    private GameObject[] arrows;
    private int currentVisibleIndex;

    [Header("CombinationSO Data")]
    public CombinationSO combinationSO;
    private int countPrefab;
    private List<KeyCode> keyCodesComand;

    public void SetDataCombinationSO(CombinationSO value)
    {
        combinationSO = value;
        combinationSO.GenerateRandomCombination();
        keyCodesComand = combinationSO.OriginalKeys;
        countPrefab = keyCodesComand.Count;

        Suscribir(true);
        DrawComands();
    }

    private void DrawComands()
    {
        arrows = new GameObject[countPrefab];

        for (int i = 0; i < countPrefab; i++)
        {
            GameObject go = Instantiate(prefabComand, transform, false);
            Sprite sprite = combinationSO.GetSpriteForKey(keyCodesComand[i]);

            go.GetComponent<UpdateSpriteComand>().UpdateSprite(sprite);
            arrows[i] = go;
        }

        currentVisibleIndex = 0;
    }

    private void HideNextArrow()
    {
        if (currentVisibleIndex < arrows.Length)
        {
            Image img = arrows[currentVisibleIndex].GetComponent<Image>();
            if (img != null) img.enabled = false;

            Transform currentArrow = arrows[currentVisibleIndex].transform;
            for (int i = 0; i < currentArrow.childCount; i++)
            {
                currentArrow.GetChild(i).gameObject.SetActive(false);
            }

            currentVisibleIndex++;
        }
    }

    public void Suscribir(bool value)
    {
        if (combinationSO == null) return;

        if (value)
            combinationSO.OnUpdateCombination += HideNextArrow;
        else
            combinationSO.OnUpdateCombination -= HideNextArrow;
    }

    public void EnableComands(bool value)
    {
        for (int i = value ? currentVisibleIndex : 0; i < arrows.Length; i++)
            arrows[i].SetActive(value);
    }
}
