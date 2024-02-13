using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlotUI : MonoBehaviour
{

    [SerializeField] private Image itemSprite;
    [SerializeField] private TextMeshProUGUI itemCount;
    [SerializeField] private InventorySlot assignedSlot;

    private Button button;

    public InventorySlot AssignedSlot => assignedSlot;
    public InventoryDisplay ParentDisplay { get; private set; }


    private void Awake()
    {
        ClearSlot();

        button = GetComponent<Button>();
        button?.onClick.AddListener(OnUISlotClick);

        ParentDisplay = transform.parent.GetComponent<InventoryDisplay>();

    }

    public void Init(InventorySlot slot)
    {
        assignedSlot = slot;
        UpdateUISlot(slot);
    }

    public void UpdateUISlot(InventorySlot slot) {
        if (slot.Data != null) {
            itemSprite.sprite = slot.Data.itemIcon;
            itemSprite.color = Color.white;

            itemCount.text = slot.StackSize > 1 ? slot.StackSize.ToString() : string.Empty;
        } else {
            ClearSlot();
        }
    }

    public void UpdateUISlot()
    {
        if(assignedSlot != null)
        {
            UpdateUISlot(assignedSlot);
        }
    }

    public void ClearSlot()
    {
        assignedSlot?.ClearSlot();
        itemSprite.sprite = null;
        itemSprite.color = Color.clear;
        itemCount.text = string.Empty;
    }

    public void OnUISlotClick()
    {
        ParentDisplay?.SlotClicked(this);
    }
}
