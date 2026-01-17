using UnityEngine;
using UnityEngine.SceneManagement;

namespace AssHunter
{
    public class LevelManager : MonoBehaviour
    {
        public static LevelManager Instance;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void LoadNextLevel()
        {
            // Логика загрузки следующего уровня
            Debug.Log("Loading next level...");
            // Например, можно использовать SceneManager.LoadScene для загрузки следующей сцены
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}

