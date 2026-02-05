using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/New Item")]
public class ItemData : ScriptableObject
{
    [Header("Basic Info")]
    public string itemName;
    public Sprite icon;
    [TextArea] public string description;

    /// <summary>
    /// Метод использования предмета. Переопределяется в наследниках.
    /// </summary>
    public virtual bool Use(GameObject source) {
        Debug.Log($"Использован предмет: {itemName}");
        return false;
    }
}
