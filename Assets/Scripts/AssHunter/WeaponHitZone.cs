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

        // void OnTriggerEnter2D(Collider2D other)
        // {
        //     if (other.GetComponent<EnemyController>() != null)
        //     {   
        //         //if (hasHit) return;
        //         var enemy = other.GetComponentInParent<EnemyController>();
        //         //hasHit = true;
        //         //if (enemy == null) return;
        //         Debug.Log("Enemy hit!");
        //         // Here you can add logic to deal damage to the enemy
                
        //         DisableHitZone();
        //         enemy.DestroyEnemy();
        //     }
        // }

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

