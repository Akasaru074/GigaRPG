using UnityEngine;

/// <summary>
/// Описание оружия. Наследуется от ItemData.
/// Добавляет урон и тип атаки.
/// </summary>
[CreateAssetMenu(fileName = "New Weapon", menuName = "Inventory/New Weapon")]
public class WeaponData : ItemData {
    [Header("Weapon Stats")]
    public int damage = 10;
    public float attackRange = 1.5f;

    public override void Use(GameObject source) {

        InventoryController inventory = source.GetComponent<InventoryController>();

        // 2. Если нашли — просим экипировать это оружие
        if (inventory != null) {
            inventory.EquipWeapon(this);
        }

    }

}
