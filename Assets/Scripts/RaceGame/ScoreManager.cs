using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance {get; private set;}
    private int score = 0;
    public int Score => score;

    [SerializeField] private TextMeshProUGUI scoreText;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void AddScore (int points)
    {
        score += points;
        //Debug.Log("Score: " + score);
        UpdateScoreUI();       
    }

    private void UpdateScoreUI()
    {
        //логика обновления UI с текущим счетом
        scoreText.text = "Score: " + score.ToString();
    }
}
