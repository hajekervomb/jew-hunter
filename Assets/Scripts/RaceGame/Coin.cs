using UnityEngine;

public class Coin : MonoBehaviour
{
    // Скорость движения монеты
    private float moveSpeed;
    [SerializeField] private int scoreAmount = 5;
        
    private void Start()
    {
        // Получаем скорость монеты из GameManager
        moveSpeed = GameManager.Instance.ObstacleSpeed;
    }

    private void Update()
    {
        // Движение монеты вниз по оси Y
        transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);        
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Если монета сталкивается с игроком
        // добавляем очки игроку
        if (other.GetComponent<PlayerController>() != null)   
        {
            ScoreManager.Instance.AddScore(scoreAmount);
            Destroy(gameObject); // Удаляем монету при столкновении        
        }
        else if (other.gameObject.name == "ObstacleBoundary")
        {
            // Если монета достигает нижней границы, просто уничтожаем его
            Destroy(gameObject);
        }     
            
    }   
}
