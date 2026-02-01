using UnityEngine.UI;
using TMPro;
using UnityEngine;

namespace AssHunter
{
    public class PlayerUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI ammoText;
        [SerializeField] private TextMeshProUGUI magazineText;
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private GameObject itemPlayerHealthPrefab;
        [SerializeField] private Transform playerHealthContainer;
        
        public void UpdateAmmoUI(int currentAmmo, int magazineSize)
        {
            ammoText.text = $"{currentAmmo} / {magazineSize}";
        }

        public void UpdateMagazinesUI(int magazineCount)
        {
            // Implement magazine count UI update if needed
            magazineText.text = $": {magazineCount}";
        }

        public void UpdateScoreUI(int score)
        {
            // Implement score UI update if needed
            scoreText.text = $"{score}";
        }    

        public void UpdatePlayerHealthUI(int currentHealthCount)
        {
            // удалить существующие иконки здоровья
            RemovePlayerHealthItem();
            // добавить новые иконки здоровья
            AddPlayerHealthItem(currentHealthCount);
        }

        private void AddPlayerHealthItem(int healthCount)
        {
            for (int i = 0; i < healthCount; i++)
            {
                Image healthItemImage = Instantiate(itemPlayerHealthPrefab, playerHealthContainer).GetComponent<Image>();
                healthItemImage.enabled = true;
            }
        }

        private void RemovePlayerHealthItem()
        {
            foreach (Transform child in playerHealthContainer)
            {
                Destroy(child.gameObject);
            }
        }
    }
}

