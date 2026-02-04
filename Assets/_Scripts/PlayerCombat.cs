using UnityEngine;

/// <summary>
/// Отвечает за атаку игрока (удар в зоне перед attackPoint).
/// Урон и радиус берутся из экипированного WeaponData (если есть), иначе используются значения по умолчанию.
/// </summary>
public class PlayerCombat : MonoBehaviour {
    [Header("Attack origin")]
    [Tooltip("Точка, где появляется зона удара (обычно пустой объект перед игроком).")]
    [SerializeField] private Transform attackPoint;

    [Header("Defaults (no weapon)")]
    [Tooltip("Радиус удара, если оружие не экипировано.")]
    [SerializeField] private float defaultAttackRange = 0.6f;

    [Tooltip("Урон, если оружие не экипировано.")]
    [SerializeField] private int defaultDamage = 5;

    [Header("Targeting")]
    [Tooltip("Слои, которые считаются врагами.")]
    [SerializeField] private LayerMask enemyLayers;

    [Header("Timing")]
    [Tooltip("Минимальная пауза между ударами (сек).")]
    [SerializeField] private float attackCooldown = 0.25f;

    private InventoryController _inventory;
    private float _lastAttackTime;

    private void Awake() {
        _inventory = GetComponent<InventoryController>();
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            TryAttack();
        }
    }

    private void TryAttack() {
        if (attackPoint == null) {
            Debug.LogWarning("PlayerCombat: attackPoint не задан в инспекторе.");
            return;
        }

        if (Time.time - _lastAttackTime < attackCooldown) return;
        _lastAttackTime = Time.time;

        int damage = GetCurrentDamage();
        float range = GetCurrentRange();

        Collider2D[] hit = Physics2D.OverlapCircleAll(attackPoint.position, range, enemyLayers);
        foreach (Collider2D col in hit) {
            Health hp = col.GetComponent<Health>();
            if (hp != null) hp.TakeDamage(damage);
        }
    }

    private int GetCurrentDamage() {
        if (_inventory != null && _inventory.currentWeapon != null)
            return _inventory.currentWeapon.damage;

        return defaultDamage;
    }

    private float GetCurrentRange() {
        if (_inventory != null && _inventory.currentWeapon != null)
            return _inventory.currentWeapon.attackRange;

        return defaultAttackRange;
    }

    private void OnDrawGizmosSelected() {
        if (attackPoint == null) return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackPoint.position, defaultAttackRange);
    }
}
