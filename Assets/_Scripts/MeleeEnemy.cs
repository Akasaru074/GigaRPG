using UnityEngine;

public class MeleeEnemy : BaseEnemy {
    private float _lastAttackTime;
    public float attackCooldown = 1f;

    protected override void Attack() {
        rb.linearVelocity = Vector2.zero;

        if (Time.time - _lastAttackTime > attackCooldown) {
            _lastAttackTime = Time.time;

            Health playerHealth = player.GetComponent<Health>();
            if (playerHealth != null) {
                playerHealth.TakeDamage(damage);
            }
        }
    }
}
