using UnityEngine;

public class Obstacle : MonoBehaviour
{
    // Скорость движения препятствия
    private float moveSpeed;
        
    private void Start()
    {
        // Получаем скорость препятствия из GameManager
        moveSpeed = GameManager.Instance.ObstacleSpeed;
    }

    private void Update()
    {
        // Движение препятствия вниз по оси Y
        transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Если препятствие сталкивается с игроком, можно добавить логику здесь   
        // снимаем жизнь у игрока или заканчиваем игру
        if (other.GetComponent<PlayerController>() != null)   
        {
            GameManager.Instance.DecreasePlayerLives(1);
            Destroy(gameObject); // Удаляем препятствие при столкновении        
        }
        else if (other.gameObject.name == "ScoreCounter")
        {
            // Если препятствие достигает объекта - счетчика очков, то добавляем очки
            ScoreManager.Instance.AddScore(1);
        }
        else
        {
            // Если препятствие достигает нижней границы, просто уничтожаем его
            Destroy(gameObject);
        }
    }    
}
