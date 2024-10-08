using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameArea : MonoBehaviour
{
    [SerializeField] private Transform left;
    [SerializeField] private Transform right;
    [SerializeField] private Transform down;
    [SerializeField] private Transform up;


    public  Vector2 GetBorderPosHorizontal()
    {

        return new Vector2(left.position.x, right.position.x);
    }

    public  Vector2 GetBorderPosVertical()
    {

        return new Vector2(down.position.y, up.position.y);
    }
}
