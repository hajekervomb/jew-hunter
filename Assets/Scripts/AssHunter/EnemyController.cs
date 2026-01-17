using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace AssHunter
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 2f;
        [SerializeField] private float rotationSpeed = 100f;
        [SerializeField] private float detectionRange = 5f;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Sprite destroyedSprite;

        public static event Action OnEnemyDestroyed;

        private Rigidbody2D rb;
        private Transform playerTransform;

        private bool isChasing = false;
        [SerializeField] private Animator enemyAnimator;
        [SerializeField] private GameObject enemiDieEffectPrefab;

        public UnityEvent onEnemyDestroyed;

        private bool isDestroyed = false;

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }

        void FixedUpdate()
        {
            // Движется к игроку, если удаленность игрока менее N единиц
            
            //Дистанция до игрока
            float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);
            if (distanceToPlayer <= detectionRange)
            {
                if (!isChasing)
                {
                    enemyAnimator.SetBool("isMoving", true);
                    isChasing = true;
                    SoundManager.Instance.PlayEnemyChaseSound();
                    detectionRange = detectionRange * 3f;
                }
                

                //Направление к игроку
                Vector2 direction = (playerTransform.position - transform.position).normalized;
                rb.MovePosition(rb.position + direction *moveSpeed*Time.fixedDeltaTime);

                // 2. Вычисляем угол (в градусах). 
                // Mathf.Atan2 возвращает радианы, поэтому переводим их в градусы.
                // Вычитаем 90 градусов, если ствол спрайта направлен вверх (Y+)
                float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

                // 3. Плавно поворачиваемся к цели
                float angle =  Mathf.MoveTowardsAngle (rb.rotation, targetAngle, rotationSpeed * Time.fixedDeltaTime);
                rb.MoveRotation(angle);
            }

        }

        public void DestroyEnemy()
        {
            if (!isDestroyed)
            {
                isDestroyed = true;
                OnEnemyDestroyed?.Invoke();
                onEnemyDestroyed?.Invoke();
                gameObject.GetComponent<CircleCollider2D>().enabled = false;
                gameObject.GetComponent<BoxCollider2D>().enabled = false;  
                
                enemyAnimator.SetBool("isMoving", false);
                enemyAnimator.SetBool("isDead", true);
                spriteRenderer.sprite = destroyedSprite;
                gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 180f);
                Instantiate(enemiDieEffectPrefab, transform.position, Quaternion.Euler(0f, 0f, UnityEngine.Random.Range(-90f, 90f)));
                this.enabled = false;
                
            }            
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                // Логика при столкновении с игроком
                Debug.Log("Enemy collided with Player!");
                collision.gameObject.GetComponent<PlayerController>().DestroyPlayer();
            }
            
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.GetComponent<WeaponHitZone>() != null)
            {
                Debug.Log("Enemy destroyed by weapon hit!");
                DestroyEnemy();
                
            }          
        }

        public void OnGameOver()
        {
            Debug.Log("Game Over - Enemy Stopping");
        }

        
    }
}

