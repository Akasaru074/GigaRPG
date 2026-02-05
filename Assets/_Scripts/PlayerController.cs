using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour, IKnockbackable {
    [Header("Settings")]
    [Tooltip("Скорость передвижения игрока")]
    [SerializeField] private float moveSpeed = 5f;

    [Header("Attack Direction")]
    [Tooltip("Точка атаки, которая будет поворачиваться в сторону движения")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackPointDistance = 0.5f;
    [SerializeField] private float stunTime = 0.15f;

    private Rigidbody2D _rb;
    private Vector2 _movement;
    private Vector2 _lastMoveDirection = Vector2.down;
    private bool _isStunned = false;

    public void ApplyKnockback(Vector2 direction, float force) {
        _isStunned = true;

        _rb.linearVelocity = Vector2.zero;
        _rb.AddForce(direction.normalized * force, ForceMode2D.Impulse);

        Invoke(nameof(RecoverFromStun), stunTime);
    }

    private void RecoverFromStun() {
        _isStunned = false;
    }

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

        if (_movement.magnitude > 0.1f) {
            _lastMoveDirection = _movement;
        }

        UpdateAttackPoint();
    }

    /// <summary>
    /// Применение физического движения с фиксированной частотой.
    /// </summary>
    private void FixedUpdate() {
        if (_isStunned) return;

        _rb.linearVelocity = _movement * moveSpeed;
    }

    /// <summary>
    /// Двигаем AttackPoint в направлении последнего движения игрока.
    /// </summary>
    private void UpdateAttackPoint() {
        if (attackPoint != null) {
            Vector3 newPosition = transform.position + (Vector3)_lastMoveDirection * attackPointDistance;
            attackPoint.position = newPosition;
        }
    }
}
