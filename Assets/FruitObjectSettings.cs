using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create FruitObjectSettings", fileName = "FruitObjectSettings", order = 0)] 
public class FruitObjectSettings : ScriptableObject 
{
    [SerializeField] private List<Sprite> sprites;
    [SerializeField] private List<float> scales;
    [SerializeField] private FruitObject prefab;

    public FruitObject SpawnObject => prefab;

    public Sprite GetSprite(int index)
    {
        if (index < 0 || index >= sprites.Count)
        {

            Debug.LogError("Out of Range");
        }
        return sprites[index];
    }
    public float GetScale(int index)
    {
        if (index < 0 || index >= scales.Count) {

            Debug.LogError("Out of Range");
        }

        return scales[index];
    }

    [ContextMenu(nameof(SetScaleData))]
    public void SetScaleData()
    {
        scales.Clear();
        for (int i = 0; i < sprites.Count; i++)
        {
            scales.Add((i + 1) * .25f);

        }
    }


}
