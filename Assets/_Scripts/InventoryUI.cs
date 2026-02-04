using UnityEngine;

/// <summary>
/// Следит за данными инвентаря и перерисовывает UI.
/// </summary>
public class InventoryUI : MonoBehaviour {
    [Header("Links")]
    public Transform itemsContainer;
    public GameObject slotPrefab;
    public InventoryController playerInventory;

    private void Start() {
        if (playerInventory == null)
            playerInventory = FindObjectOfType<InventoryController>();

        if (playerInventory != null) {
            playerInventory.onItemChanged += UpdateUI;

            UpdateUI();
        }
    }

    private void OnDestroy() {
        if (playerInventory != null) {
            playerInventory.onItemChanged -= UpdateUI;
        }
    }

    /// <summary>
    /// Полная перерисовка инвентаря.
    /// Удаляет старые слоты и создает новые.
    /// </summary>
    public void UpdateUI() {
        foreach (Transform child in itemsContainer) {
            Destroy(child.gameObject);
        }

        foreach (ItemData item in playerInventory.items) {
            GameObject slotObj = Instantiate(slotPrefab, itemsContainer);
            InventorySlotUI slotScript = slotObj.GetComponent<InventorySlotUI>();
            slotScript.Setup(item, this);
        }
    }
}
