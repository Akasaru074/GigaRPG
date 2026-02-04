using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Отвечает за отображение одной ячейки в UI инвентаря.
/// </summary>
public class InventorySlotUI : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    [SerializeField] private Button button;

    private ItemData _currentItem;
    private InventoryUI _parentUI;

    private void Awake() {
        if (button == null) button = GetComponent<Button>();
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(OnClick);
    }

    /// <summary>
    /// Инициализация слота. Вызывается при обновлении инвентаря.
    /// </summary>
    public void Setup(ItemData item, InventoryUI parentUI) {
        _currentItem = item;
        _parentUI = parentUI;

        iconImage.sprite = item.icon;
        iconImage.enabled = true;
    }

    /// <summary>
    /// На клик
    /// </summary>
    public void OnClick() {
        if (_currentItem != null) {
            _currentItem.Use();
            Debug.Log("Клик по предмету!");
        }
    }

    private void OnValidate() {
        if (iconImage == null) iconImage = GetComponent<Image>();
        if (button == null) button = GetComponent<Button>();
    }
}
