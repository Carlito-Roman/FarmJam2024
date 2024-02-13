using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public abstract class InventoryDisplay : MonoBehaviour
{

    [SerializeField] CursorItemData cursorInventoryItem;
    protected InventorySystem inventorySystem;
    protected Dictionary<InventorySlotUI, InventorySlot> slotDictionary;

    public InventorySystem InventorySystem => inventorySystem;
    public Dictionary<InventorySlotUI, InventorySlot> SlotDictionary => slotDictionary;

    protected virtual void Start()
    {

    }

    public abstract void AssignSlot(InventorySystem inventoryToDisplay);

    protected virtual void UpdateSlot(InventorySlot updatedSlot)
    {
        foreach(var slot in SlotDictionary)
        {
            if(slot.Value == updatedSlot) // Slot Value - Back-End slot "InventorySlot"
            {
                slot.Key.UpdateUISlot(updatedSlot); //Key Value - Front-End slot "InventorySlotUI"
            }
        }
    }

    public void SlotClicked(InventorySlotUI clickedSlot)
    {
        Debug.Log("Slot Clicked");
    }

}
