using UnityEngine;
using UnityEngine.InputSystem;  // 1. The Input System "using" statement

public class PlayerMovement : MonoBehaviour
{
    [Header("Настройки движения")]
    public float moveSpeed = 5f; // Скорость движения персонажа
    
    private Rigidbody2D rb; // Ссылка на Rigidbody2D компонент
    private Vector2 moveInput; // Вектор ввода движения
    
    void Start()
    {
        // Получаем компонент Rigidbody2D
        rb = GetComponent<Rigidbody2D>();
        
        // Если Rigidbody2D не найден, добавляем его
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
        }
        
        // Настраиваем Rigidbody2D для top-down движения
        rb.gravityScale = 0f; // Отключаем гравитацию
        rb.linearDamping = 0f; // Убираем сопротивление
        rb.angularDamping = 0f; // Убираем угловое сопротивление
    }

    void FixedUpdate()
    {
        // Применяем движение к Rigidbody2D
        rb.linearVelocity = moveInput * moveSpeed * Time.fixedDeltaTime;
    }
    
    // Метод для обработки ввода движения (вызывается новой Input System)
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
}

