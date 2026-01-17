using System.Collections;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
   public static SpawnerManager Instance { get; private set; }

    
    
    [Header("Object Settings")]
    // Ссылка на префаб препятствия
    [SerializeField] private GameObject obstaclePrefab;
    [SerializeField] private GameObject coinPrefab;     
    [SerializeField] private GameObject objectsContainer;
    [SerializeField] private float patternCommonInterval = 0.35f; // секунды для Common
    [SerializeField] private float patternFastInterval = 0.15f; // секунды для Fast
    private bool isSpawningPattern = false;

    public enum SpawnableType { Obstacle, Coin }
    public enum SpawnSide { Left, Right, Random }
    public enum PatternTime { Common, Fast }

    [System.Serializable]
    public class PatternItem
    {
        public SpawnSide side = SpawnSide.Random;
        public SpawnableType type = SpawnableType.Obstacle;
        public PatternTime time = PatternTime.Common;
    }

    [System.Serializable]
    public class SpawnPattern
    {
        public string name = "Pattern";
        public PatternItem[] items = new PatternItem[0];
    }

    [SerializeField] private SpawnPattern[] spawnPatterns;
    
    [Header("Spawner Settings")]
    // Ссылка на спавнеры: левый и правый
    [SerializeField] private GameObject leftSpawner;
    [SerializeField] private GameObject rightSpawner;
    [SerializeField] private float spawnInterval = 2f;
    
    private float obstacleTimer = 0f;
    private float spawnTimer = 0f;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        //задаем старт значение таймера спавна
        spawnTimer = spawnInterval;
        // Если в инспекторе нет паттернов — задаём несколько примеров (можно удалить/отредактировать в инспекторе)
        if (spawnPatterns == null || spawnPatterns.Length == 0)
        {
            spawnPatterns = new SpawnPattern[] {
                // 1. Слева: obstacle, coin, obstacle
                new SpawnPattern { name = "Left: O,C,O", items = new PatternItem[] {
                    new PatternItem { side = SpawnSide.Left, type = SpawnableType.Obstacle, time = PatternTime.Fast },
                    new PatternItem { side = SpawnSide.Left, type = SpawnableType.Coin, time = PatternTime.Fast },
                    new PatternItem { side = SpawnSide.Left, type = SpawnableType.Obstacle, time = PatternTime.Common }
                } },
                // 2. Слева: obstacle, Справа: obstacle, Слева: obstacle
                new SpawnPattern { name = "L:O, R:O, L:O", items = new PatternItem[] {
                    new PatternItem { side = SpawnSide.Left, type = SpawnableType.Obstacle, time = PatternTime.Common },
                    new PatternItem { side = SpawnSide.Right, type = SpawnableType.Obstacle, time = PatternTime.Fast },
                    new PatternItem { side = SpawnSide.Left, type = SpawnableType.Obstacle, time = PatternTime.Common }
                } },
                // 3. Случайно: obstacle (одиночный)
                new SpawnPattern { name = "Random single", items = new PatternItem[] {
                    new PatternItem { side = SpawnSide.Random, type = SpawnableType.Obstacle, time = PatternTime.Common }
                } },
                // 4. Слева: obstacle, Слева: obstacle, Справа: obstacle
                new SpawnPattern { name = "L:O, L:O, R:O", items = new PatternItem[] {
                    new PatternItem { side = SpawnSide.Left, type = SpawnableType.Obstacle, time = PatternTime.Fast },
                    new PatternItem { side = SpawnSide.Left, type = SpawnableType.Obstacle, time = PatternTime.Fast },
                    new PatternItem { side = SpawnSide.Right, type = SpawnableType.Obstacle, time = PatternTime.Common }
                } },
                // 5. Справа: obstacle, coin, obstacle
                new SpawnPattern { name = "Right: O,C,O", items = new PatternItem[] {
                    new PatternItem { side = SpawnSide.Right, type = SpawnableType.Obstacle },
                    new PatternItem { side = SpawnSide.Right, type = SpawnableType.Coin },
                    new PatternItem { side = SpawnSide.Right, type = SpawnableType.Obstacle }
                } },
                // 6. Справа: obstacle, Слева: obstacle, Слева: obstacle
                new SpawnPattern { name = "R:O, L:O, L:O", items = new PatternItem[] {
                    new PatternItem { side = SpawnSide.Right, type = SpawnableType.Obstacle },
                    new PatternItem { side = SpawnSide.Left, type = SpawnableType.Obstacle },
                    new PatternItem { side = SpawnSide.Left, type = SpawnableType.Obstacle }
                } }
            };
        }
    }

    private void Update()
    {
        //через определённый промежуток времени вызывать SpawnObstacle слева или справа
        obstacleTimer += Time.deltaTime;

        if (obstacleTimer >= spawnTimer)
        {
            obstacleTimer = 0f;
            // если есть паттерны и сейчас не идёт их выполнение — запускаем случайный паттерн
            if (spawnPatterns != null && spawnPatterns.Length > 0 && !isSpawningPattern)
            {
                var pattern = spawnPatterns[Random.Range(0, spawnPatterns.Length)];
                StartCoroutine(SpawnPatternCoroutine(pattern));
                Debug.Log("Spawning pattern: " + pattern.name);
            }            
        }        
    }   

    private IEnumerator SpawnPatternCoroutine(SpawnPattern pattern)
    {
        if (pattern == null || pattern.items == null || pattern.items.Length == 0)
            yield break;

        isSpawningPattern = true;
        foreach (var item in pattern.items)
        {
            var side = item.side;
            if (side == SpawnSide.Random)
                side = (Random.value > 0.5f) ? SpawnSide.Left : SpawnSide.Right;

            SpawnByType(item.type, side);
            float wait = (item.time == PatternTime.Fast) ? patternFastInterval : patternCommonInterval;
            yield return new WaitForSeconds(wait);
        }

        isSpawningPattern = false;
    }

    private void SpawnByType(SpawnableType type, SpawnSide side)
    {
        GameObject prefab = null;
        switch (type)
        {
            case SpawnableType.Coin:
                prefab = coinPrefab;
                break;
            case SpawnableType.Obstacle:
                prefab = obstaclePrefab;
                break;
        }

        bool isLeft = side == SpawnSide.Left;
        if (prefab != null)
            SpawnPrefab(prefab, isLeft);
    }

    private void SpawnPrefab(GameObject prefab, bool isLeft)
    {
        if (isLeft)
        {
            Instantiate(prefab, leftSpawner.transform.position, Quaternion.identity, objectsContainer.transform);
        }
        else
        {
            Instantiate(prefab, rightSpawner.transform.position, Quaternion.identity, objectsContainer.transform);
        }
        
    }            
}

    
        

    
