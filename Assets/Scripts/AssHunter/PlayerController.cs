using UnityEngine;
using UnityEngine.InputSystem;

namespace AssHunter
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Movement Settings")]
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private float rotationSpeed = 200f;
        
        [SerializeField] private WeaponController weaponController;
        private float moveInput;
        private float rotationInput;        
        private InputAction shootAction;
        private InputAction reloadAction;
        private Rigidbody2D rb;

        [SerializeField] private Animator playerAnimator;

        // void Awake()
        // {
        //     GameObject[] objs = GameObject.FindGameObjectsWithTag("Player");
        //     if (objs.Length > 1)
        //         Destroy(this.gameObject);

        //     DontDestroyOnLoad(gameObject);
        // }

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            // Find the actions defined in the Input System            
            shootAction = InputSystem.actions.FindAction("Shoot");
            reloadAction = InputSystem.actions.FindAction("Reload");
        }

        void Update()
        {
            if (shootAction.WasPressedThisFrame())
            {
                // Handle shoot action
                weaponController.Shoot();
            }

            if (reloadAction.WasPressedThisFrame())
            {
                // Handle reload action
                weaponController.Reload();
            }
        }

        void FixedUpdate()
        {
            //Логика исключения
            //Если персонаж движется, то он не может поворачивать
            //Если персонаж поворачивается, то он не может двигаться
            float currentMove = moveInput;
            float currentRotation = rotationInput;

            if (Mathf.Abs(currentMove) > 0.1f)
            {
                // если персонаж движется, то он не может поворачивать
                currentRotation = 0f;                
            }
            else if (Mathf.Abs(currentRotation) > 0.1f)
            {
                // если персонаж поворачивается, то он не может двигаться
                currentMove = 0f;                
            }
            
            bool isMoving = Mathf.Abs(currentMove) > 0.1f;
            
            playerAnimator.SetBool("isMoving", isMoving);

            // Rotate the player based on rotation input
            float rotation = currentRotation * rotationSpeed * Time.fixedDeltaTime;
            rb.MoveRotation(rb.rotation - rotation);

            // Move the player based on move input
            Vector2 movement = transform.up * currentMove * moveSpeed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + movement);
        }

        private void OnRotate(InputValue value)
        {
            rotationInput = value.Get<float>();
        }

        private void OnMove(InputValue value)
        {
            moveInput = value.Get<float>();
        }

        public void DestroyPlayer()
        {
            // Логика уничтожения игрока
            playerAnimator.SetBool("isDead", true);
            enabled = false;
        }
    }

}
