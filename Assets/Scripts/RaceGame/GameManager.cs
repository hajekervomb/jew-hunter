using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    //singleton instance
    public static GameManager Instance { get; private set; }

    
    // Скорость спавна препятствий
    private float spawnInterval;
    [SerializeField] private float obstacleSpeed = 5f;
    public float ObstacleSpeed => obstacleSpeed;
    
    

    //жизни игрока можно добавить здесь
    [SerializeField] private int playerLives = 3;
    public int PlayerLives => playerLives;
    [SerializeField] private TextMeshProUGUI livesText;

    //логика игровой паузы может быть добавлена здесь
    private bool isPaused = false;

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
        UpdateLivesUI();
    }    
    

    public void SetObstacleSpeed(float newObstacleSpeed, float newSpawnInterval)
    {
        obstacleSpeed = newObstacleSpeed;
        spawnInterval = newSpawnInterval;
    }   
    

    //уменьшение жизней игрока
    public void DecreasePlayerLives(int amount)
    {
        playerLives -= amount;
        UpdateLivesUI();
        if (playerLives <= 0)
        {
            //логика окончания игры
            Debug.Log("Game Over!");
        }
    }

    private void UpdateLivesUI()
    {
        //логика обновления UI с текущими жизнями
        livesText.text = "Lives: " + playerLives.ToString();
    }
}
