using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("Скорость передвижения игрока")]
    [SerializeField] private float moveSpeed = 5f;

    private Rigidbody2D _rb;
    private Vector2 _movement;

    /// <summary>
    /// Инициализация компонентов.
    /// </summary>
    private void Awake() {
        _rb = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Считывание ввода игрока каждый кадр.
    /// </summary>
    private void Update() {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        _movement = new Vector2(moveX, moveY).normalized;
    }

    /// <summary>
    /// Применение физического движения с фиксированной частотой.
    /// </summary>
    private void FixedUpdate() {
        _rb.linearVelocity = _movement * moveSpeed;
    }
}
