using System;
using UnityEngine;

namespace AssHunter
{
    public class GameManager : MonoBehaviour
    {
        //синглтон
        public static GameManager Instance;

        
        
        [SerializeField] private PlayerUI playerUI;
        [SerializeField] private int score = 0;

        [SerializeField] private int playerHealthCount = 2;
        private int currentPlayerHealth;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                transform.parent = null;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }

            InitPlayer();
            
        }
        

        public void InitPlayer()
        {
            playerUI.UpdateScoreUI(score);

            if (currentPlayerHealth == 0)
            {
                currentPlayerHealth = playerHealthCount;
            }
            // update player health UI
            playerUI.UpdatePlayerHealthUI(currentPlayerHealth);
        }

        

        public void AddScore()
        {
            score++;
            playerUI.UpdateScoreUI(score);
        }

        void OnEnable()
        {
            EnemyController.OnEnemyDestroyed += AddScore;
        }

        void OnDisable()
        {
            EnemyController.OnEnemyDestroyed -= AddScore;
        }

        public void AddPlayerHealth()
        {
            currentPlayerHealth++;
            playerUI.UpdatePlayerHealthUI(currentPlayerHealth);
        }
    }
}

