using Unity.VisualScripting;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }
    [SerializeField] private AudioSource backgroundMusicSource;
    [SerializeField] private AudioSource shootSoundSource;
    [SerializeField] private AudioSource pickupSoundSource;
    [SerializeField] private AudioSource enemyChaseSoundSource;
    [SerializeField] private AudioSource enemyDeathSoundSource;
    [SerializeField] private AudioSource reloadSoundSource;

    private void Awake()
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
    }

    private void Start()
    {
        PlayBackgroundMusic();
    }

    private void PlayBackgroundMusic()
    {
        if (backgroundMusicSource != null && !backgroundMusicSource.isPlaying)
        {
            backgroundMusicSource.loop = true;
            backgroundMusicSource.Play();
        }
    }
    
    private void PlayShootSound()
    {
        // Implement shoot sound playback logic here
        if (shootSoundSource != null)
        {
            shootSoundSource.PlayOneShot(shootSoundSource.clip);
        }
    }

    public void PlayPickupSound()
    {
        // Implement pickup sound playback logic here
        if (pickupSoundSource != null)
        {
            pickupSoundSource.PlayOneShot(pickupSoundSource.clip);
        }
    }

    public void PlayEnemyChaseSound()
    {
        // Implement enemy chase sound playback logic here
        if (enemyChaseSoundSource != null && !enemyChaseSoundSource.isPlaying)
        {
            enemyChaseSoundSource.loop = true;
            enemyChaseSoundSource.Play();
        }
    }

    public void StopEnemyChaseSound()
    {
        if (enemyChaseSoundSource != null && enemyChaseSoundSource.isPlaying)
        {
            enemyChaseSoundSource.Stop();
        }
    }

    private void PlayEnemyDeathSound()
    {
        // Implement enemy death sound playback logic here
        if (enemyDeathSoundSource != null)
        {
            enemyDeathSoundSource.PlayOneShot(enemyDeathSoundSource.clip);
        }
    }

    public void PlayReloadSound()
    {
        // Implement reload sound playback logic here
        if (reloadSoundSource != null)
        {
            reloadSoundSource.PlayOneShot(reloadSoundSource.clip);
        }
    }

    private void OnEnable()
    {
        AssHunter.WeaponController.onShoot += PlayShootSound;
        AssHunter.EnemyController.OnEnemyDestroyed += PlayEnemyDeathSound;
        AssHunter.EnemyController.OnEnemyDestroyed += StopEnemyChaseSound;
    }
    private void OnDisable()
    {
        AssHunter.WeaponController.onShoot -= PlayShootSound;
        AssHunter.EnemyController.OnEnemyDestroyed -= PlayEnemyDeathSound;
        AssHunter.EnemyController.OnEnemyDestroyed -= StopEnemyChaseSound;
    }

}
