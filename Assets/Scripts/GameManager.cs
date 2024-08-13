using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private FruitObjectSettings settings;
    [SerializeField] private GameArea gameArea;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private Transform gameOverLine; // Çizgi objesi
    [SerializeField] private float gameOverDelay = 1f;
    [SerializeField] private float debounceTime = 10f; // Debounce süresi

    private bool isGameOver;
    private bool isDebouncing;
    private bool Isclick => Input.GetMouseButtonDown(0);
    private readonly Vector2Int fruitRange = new Vector2Int(0, 4);

    private void Awake()
    {
        Instance = this;
    }

    private float GetInputHorizontalPosion()
    {
        var inputPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        var limit = gameArea.GetBorderPosHorizontal();
        var result = Mathf.Clamp(inputPosition, min: limit.x, max: limit.y);
        return result;
    }

    private void OnClick()
    {
        if (isDebouncing) return;

        var index = Random.Range(fruitRange.x, fruitRange.y);
        var spawnPoision = new Vector2(GetInputHorizontalPosion(), spawnPoint.position.y);
        SpawnFurit(index, spawnPoision);
        StartCoroutine(DebounceCoroutine());
    }

    private void SpawnFurit(int index, Vector2 position)
    {
        var prefab = settings.SpawnObject;
        var furiatobj = Instantiate(prefab, position, Quaternion.identity);
        var sprite = settings.GetSprite(index);
        var scale = settings.GetScale(index);

        furiatobj.Prepare(sprite, index, scale);
    }

    public void Merge(FruitObject first, FruitObject second)
    {
        var type = first.type + 1;
        var spawnPosition = (first.transform.position + second.transform.position) / 2f;
        Destroy(first.gameObject);
        Destroy(second.gameObject);
        SpawnFurit(type, spawnPosition);
    }

    private void Update()
    {
        if (Isclick)
        {
            OnClick();
        }
    }

    public void TriggerGameOver()
    {
        if (!isGameOver)
        {
            StartCoroutine(GameOverSequence());
        }
    }

    private IEnumerator GameOverSequence()
    {
        isGameOver = true;
        gameOverPanel.SetActive(true);
        Debug.Log("Game Over Panel Activated");
        yield return new WaitForSeconds(gameOverDelay);
        Time.timeScale = 0;
        Debug.Log("Game Over Triggered");
    }

    public void CheckGameOver(FruitObject fruit)
    {
        if (fruit.transform.position.y >= gameOverLine.position.y)
        {
            TriggerGameOver();
        }
    }

    private IEnumerator DebounceCoroutine()
    {
        isDebouncing = true;
        yield return new WaitForSeconds(debounceTime);
        isDebouncing = false;
    }
}
