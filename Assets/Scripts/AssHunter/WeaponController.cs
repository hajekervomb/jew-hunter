using Unity.VisualScripting;
using UnityEngine;

namespace AssHunter
{
    public class WeaponController : MonoBehaviour
    {

        public static WeaponController Instance { get; private set; }

        // WeaponController implementation goes here
        [Header("Weapon Settings")]
        // 5 снарядов в обойме
        [SerializeField] private int magazineSize = 5;
        [SerializeField] private int magazineCount = 0;        
        [SerializeField] private float fireRate = 0.5f;
        private float lastFireTime = 0f;
        [SerializeField] private WeaponHitZone hitZone;
        private int currentAmmo;

        [SerializeField] private PlayerUI playerUI;

        public delegate void OnShootAction();
        public static OnShootAction onShoot;

        [SerializeField] private Animator weaponAnimator;

        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        void Start()
        {
            currentAmmo = magazineSize;

            playerUI.UpdateAmmoUI(currentAmmo, magazineSize);
            playerUI.UpdateMagazinesUI(magazineCount);
        }
        void Update()
        {
            if (lastFireTime >= 0)
            {
                lastFireTime -= Time.deltaTime;
            }
        }

        public void Shoot()
        {
            if (isShootingAvailable())
            {
                onShoot?.Invoke();

                lastFireTime = fireRate;
                currentAmmo--;
                hitZone.ActivateHitZone();

                playerUI.UpdateAmmoUI(currentAmmo, magazineSize);

                weaponAnimator.SetTrigger("Shoot");
                
                Debug.Log("Fired a shot! Remaining ammo: " + currentAmmo);
            }
            else
            {
                Debug.Log("Out of ammo! Reload needed.");
            }
        }

        public void Reload()
        {
            if (magazineCount <= 0)
            {
                Debug.Log("No magazines left to reload!");
                return;
            }

            SoundManager.Instance.PlayReloadSound();

            currentAmmo = magazineSize;
            magazineCount--;

            playerUI.UpdateAmmoUI(currentAmmo, magazineSize);
            playerUI.UpdateMagazinesUI(magazineCount);

            Debug.Log("Reloaded! Ammo refilled to: " + currentAmmo);
        }

        public void AddMagazine()
        {
            magazineCount++;

            playerUI.UpdateMagazinesUI(magazineCount);
            
            Debug.Log("Added a magazine. Total magazines: " + magazineCount);
        }

        public bool isShootingAvailable()
        {
            return currentAmmo > 0 && lastFireTime <= 0;
        }
    }    
}

