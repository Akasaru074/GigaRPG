using System;
using UnityEngine;

/// <summary>
/// Универсальный компонент здоровья.
/// </summary>
public class Health : MonoBehaviour {
    [Header("Settings")]
    public int maxHealth = 100;

    [Header("Knockback")]
    [SerializeField] private float knockbackForce = 5f;

    private int _currentHealth;
    private Rigidbody2D _rb;

    public Action<int, int> OnHealthChanged;

    public Action OnDeath;

    private void Start() {
        _currentHealth = maxHealth;
        _rb = GetComponent<Rigidbody2D>();

        OnHealthChanged?.Invoke(_currentHealth, maxHealth);
    }

    /// <summary>
    /// Нанесение урона объекту.
    /// </summary>
    public void TakeDamage(int damage) {
        _currentHealth -= damage;
        Debug.Log($"{gameObject.name} получил {damage} урона. HP: {_currentHealth}");

        OnHealthChanged?.Invoke(_currentHealth, maxHealth);

        if (_currentHealth <= 0) {
            Die();
        }
    }

    /// <summary>
    /// Нанесение урона с отталкиванием в указанном направлении.
    /// </summary>
    public void TakeDamage(int damage, Vector2 knockbackDirection) {
        TakeDamage(damage);

        IKnockbackable knockbackTarget = GetComponent<IKnockbackable>();
        knockbackTarget?.ApplyKnockback(knockbackDirection, knockbackForce);
    }

    public void Heal(int amount) {
        _currentHealth += amount;
        if (_currentHealth > maxHealth) _currentHealth = maxHealth;
        OnHealthChanged?.Invoke(_currentHealth, maxHealth);
        Debug.Log($"{gameObject.name} вылечился. HP: {_currentHealth}");
    }

    private void Die() {
        Debug.Log($"{gameObject.name} погиб!");
        OnDeath?.Invoke();

        // TODO: change this to game over screen
        Destroy(gameObject);
    }
}
