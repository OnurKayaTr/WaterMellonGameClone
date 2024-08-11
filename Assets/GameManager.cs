using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;




public class GameManager : MonoBehaviour
{

    [SerializeField] private FruitObjectSettings settings;
    [SerializeField] private GameArea gameArea;

    [SerializeField] private Transform spawnPoint;
    private bool Isclick => Input.GetMouseButtonDown(0);

    private readonly Vector2Int fruitRange = new Vector2Int(0, 4);


    private float GetInputHorizontalPosion() {


        var inputPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        var limit = gameArea.GetBorderPosHorizontal();
        var result = Mathf.Clamp(inputPosition, min: limit.x, max: limit.y);
        return result;
    }

    private void SpawnFurit()
    {
        var prefab = settings.SpawnObject;
        var spawnPoision = new Vector2(GetInputHorizontalPosion(),spawnPoint.position.y);

        var furiatobj = Instantiate(prefab, spawnPoision, Quaternion.identity);



        var index = Random.Range(fruitRange.x, fruitRange.y);
        var sprite = settings.GetSprite(index);
        var scale = settings.GetScale(index);
        
        furiatobj.Prepare(sprite,index, scale);
    }

    private void Update()
    {
        if (Isclick)
        {
            SpawnFurit();
        }
    }

}

