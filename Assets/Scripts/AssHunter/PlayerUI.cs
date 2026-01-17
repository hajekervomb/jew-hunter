using TMPro;
using UnityEngine;

namespace AssHunter
{
    public class PlayerUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI ammoText;
        [SerializeField] private TextMeshProUGUI magazineText;
        
        public void UpdateAmmoUI(int currentAmmo, int magazineSize)
        {
            ammoText.text = $"{currentAmmo} / {magazineSize}";
        }

        public void UpdateMagazinesUI(int magazineCount)
        {
            // Implement magazine count UI update if needed
            magazineText.text = $"Magazines: {magazineCount}";
        }
    }
}

