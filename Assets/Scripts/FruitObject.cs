using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitObject : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    public int type { get; private set; }
    public bool SendedMergeSignal { get; private set; }

    public void Prepare(Sprite sprite, int index, float scale)
    {
        spriteRenderer.sprite = sprite;
        type = index;
        transform.localScale = Vector3.one * scale;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        var fruiatObj = other.transform.GetComponent<FruitObject>();

        if (!fruiatObj) { return; }
        if (fruiatObj.type != type) { return; }
        if (fruiatObj.SendedMergeSignal) { return; }
        SendedMergeSignal = true;
        GameManager.Instance.Merge(this, fruiatObj);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("GameOverLine"))
        {
            GameManager.Instance.TriggerGameOver();
        }
        else
        {
            var fruiatObj = other.GetComponent<FruitObject>();
            if (!fruiatObj) { return; }
            if (fruiatObj.type != type) { return; }
            if (fruiatObj.SendedMergeSignal) { return; }
            SendedMergeSignal = true;
            GameManager.Instance.Merge(this, fruiatObj);
        }
    }

}
