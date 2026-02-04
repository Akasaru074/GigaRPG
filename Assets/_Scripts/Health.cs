using System;
using UnityEngine;

/// <summary>
/// Универсальный компонент здоровья.
/// </summary>
public class Health : MonoBehaviour {
    [Header("Settings")]
    [SerializeField] private int maxHealth = 100;

    private int _currentHealth;

    public Action OnDeath;

    private void Start() {
        _currentHealth = maxHealth;
    }

    /// <summary>
    /// Нанесение урона объекту.
    /// </summary>
    public void TakeDamage(int damage) {
        _currentHealth -= damage;
        Debug.Log($"{gameObject.name} получил {damage} урона. HP: {_currentHealth}");

        if (_currentHealth <= 0) {
            Die();
        }
    }

    private void Die() {
        Debug.Log($"{gameObject.name} погиб!");
        OnDeath?.Invoke();

        // TODO: change this to game over screen
        Destroy(gameObject);
    }
}
