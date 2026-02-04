using UnityEngine;

/// <summary>
/// Базовый класс для всех врагов. Отвечает за поиск игрока.
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public abstract class BaseEnemy : MonoBehaviour {
    [Header("Stats")]
    public float moveSpeed = 3f;
    public int damage = 10;
    public float attackRange = 1f;
    public float detectionRange = 10f;

    protected Transform player;
    protected Rigidbody2D rb;

    protected virtual void Start() {
        rb = GetComponent<Rigidbody2D>();
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null) {
            player = playerObj.transform;
        }
    }

    protected virtual void FixedUpdate() {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance < detectionRange && distance > attackRange) {
            MoveTowardsPlayer();
        } else if (distance <= attackRange) {
            Attack();
        } else {
            rb.linearVelocity = Vector2.zero;
        }
    }

    /// <summary>
    /// Логика движения. Можно переопределить у разных врагов.
    /// </summary>
    protected virtual void MoveTowardsPlayer() {
        Vector2 direction = (player.position - transform.position).normalized;
        rb.linearVelocity = direction * moveSpeed;
    }

    /// <summary>
    /// Логика атаки. Обязательна для реализации в наследниках.
    /// </summary>
    protected abstract void Attack();
}
