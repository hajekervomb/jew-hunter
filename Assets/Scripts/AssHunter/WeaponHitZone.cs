using UnityEngine;

namespace AssHunter
{
    public class WeaponHitZone : MonoBehaviour
    {

        private bool hasHit;

        void Start()
        {
            DisableHitZone();
        }

        

        void OnDisable()
        {
            // Optionally, you can add logic here if needed when the hit zone is disabled
        }

        void OnEnable()
        {
            hasHit = false;
            // Disable the hit zone after a short delay to simulate a hit
            Invoke("DisableHitZone", 0.05f);
        }

        private void DisableHitZone()
        {
            gameObject.SetActive(false);
        }

        public void ActivateHitZone()
        {
            gameObject.SetActive(true);
        }
    }
}

