using UnityEngine;

namespace AssHunter
{
    public class NextLevelGate : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                LevelManager.Instance.LoadNextLevel();
            }
        }
    }
}

