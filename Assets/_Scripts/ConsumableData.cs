using UnityEngine;

[CreateAssetMenu(fileName = "New Consumable", menuName = "Inventory/Consumable")]
public class ConsumableData : ItemData {
    [Header("Effect")]
    public int healAmount = 20;

    public override bool Use(GameObject source) {
        Health health = source.GetComponent<Health>();

        if (health != null) {

            health.Heal(healAmount);

            Debug.Log($"Выпили {itemName} и восстановили {healAmount} HP");
            return true;
        }

        return false;
    }
}
