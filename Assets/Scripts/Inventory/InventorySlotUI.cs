using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.TextCore.Text;

public class InventorySlotUI : MonoBehaviour
{

    #region Variables

    [SerializeField] private Image itemSprite;
    [SerializeField] private TextMeshProUGUI itemCount;
    [SerializeField] private InventorySlot assignedSlot;

    private Button button;

    public InventorySlot AssignedInventorySlot => assignedSlot;
    public InventoryDisplay ParentDisplay { get; private set; }

    #endregion

    #region MonoBehaviour Callbacks

    private void Awake()
    {
        ClearSlot();

        button = GetComponent<Button>();
        button?.onClick.AddListener(OnUISlotClick);

        ParentDisplay = transform.parent.GetComponent<InventoryDisplay>();

    }

    #endregion

    #region Public Methods

    public void Init(InventorySlot slot)
    {
        assignedSlot = slot;
        UpdateUISlot(slot);
    }

    public void UpdateUISlot(InventorySlot slot) {
        if (slot.ItemData != null) {
            itemSprite.sprite = slot.ItemData.itemIcon;
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

    #endregion

}
