using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FruitObject : MonoBehaviour
{
    private int type;
    [SerializeField] private SpriteRenderer spriteRenderer;

    public void Prepare(Sprite sprite, int index, float scale)
    {
        spriteRenderer.sprite = sprite;
        type = index;
        transform.localScale = Vector3.one * scale;
    }

}
