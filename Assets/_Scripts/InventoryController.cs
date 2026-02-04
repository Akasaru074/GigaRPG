using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Хранит список предметов игрока и управляет добавлением/удалением.
/// </summary>
public class InventoryController : MonoBehaviour
{
    public List<ItemData> items = new List<ItemData>();

    [SerializeField] private int capacity = 10;

    /// <summary>
    /// Пытается добавить предмет в инвентарь.
    /// </summary>
    /// <returns>True, если предмет добавлен. False, если места нет.</returns>
    public bool AddItem(ItemData item) {
        if (items.Count >= capacity) {
            Debug.Log("Инвентарь полон!");
            return false;
        }

        items.Add(item);
        Debug.Log($"Подобран предмет: {item.itemName}");
        return true;
    }

    /// <summary>
    /// Удаляет предмет из списка.
    /// </summary>
    public void RemoveItem(ItemData item) {
        items.Remove(item);
    }
}
