using UnityEngine;

namespace AssHunter
{
    public class GameManager : MonoBehaviour
    {
        void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        public void StartGame()
        {
            // Implement game restart logic here
            UnityEngine.SceneManagement.SceneManager.LoadScene("AssHunter_Level1");
        }
    }
}

