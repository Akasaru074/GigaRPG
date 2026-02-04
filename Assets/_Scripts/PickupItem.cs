using UnityEngine;

/// <summary>
/// Вешается на объект в мире. При столкновении с игроком добавляет предмет в инвентарь.
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class PickupItem : MonoBehaviour {
    [Tooltip("Какой предмет мы получим")]
    public ItemData itemData;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            InventoryController inventory = other.GetComponent<InventoryController>();

            if (inventory != null) {
                bool wasPickedUp = inventory.AddItem(itemData);

                if (wasPickedUp) {
                    Destroy(gameObject);
                }
            }
        }
    }
}
