using UnityEngine;
using TMPro;

public class HealthUI : MonoBehaviour {
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private Health playerHealth;

    private void Start() {
        if (playerHealth == null) {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null) playerHealth = player.GetComponent<Health>();
        }

        if (playerHealth != null) {
            playerHealth.OnHealthChanged += UpdateHealthText;
            UpdateHealthText(playerHealth.maxHealth, playerHealth.maxHealth);
        }
    }

    private void OnDestroy() {
        if (playerHealth != null) {
            playerHealth.OnHealthChanged -= UpdateHealthText;
        }
    }

    private void UpdateHealthText(int current, int max) {
        if (healthText != null) {
            healthText.text = $"HP: {current}";

            if (current <= max * 0.3f)
                healthText.color = Color.red;
            else
                healthText.color = Color.white;
        }
    }
}
