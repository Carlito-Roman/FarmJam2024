using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class InventorySystem
{

    #region Variables

    [SerializeField] private List<InventorySlot> inventorySlots;

    public List<InventorySlot> InventorySlots => inventorySlots;
    public int InventorySize => inventorySlots.Count;

    public UnityAction<InventorySlot> OnInventorySlotChange;

    #endregion

    #region Public Methods

    public InventorySystem(int size)
    {
        inventorySlots = new List<InventorySlot>(size);

        for (int i = 0; i < size; i++) {
            inventorySlots.Add(new InventorySlot());
        }
    }

    public bool AddToInventory(InventoryItemData itemToAdd, int amountToAdd)
    {

        //Checking for item existence in inventory
        if (ContainsItem(itemToAdd, out List<InventorySlot> invSlot)) {
            foreach(var slot in invSlot) {
                if(slot.isThereRoomInStack(amountToAdd)) {
                    slot.AddToStack(amountToAdd);
                    OnInventorySlotChange?.Invoke(slot);
                    return true;
                }
            }  
        }

        //Finds first open slot
        if (HasFreeSlot(out InventorySlot freeSlot)) {
            if (freeSlot.isThereRoomInStack(amountToAdd)) {
                freeSlot.UpdateInventorySlot(itemToAdd, amountToAdd);
                OnInventorySlotChange?.Invoke(freeSlot);
                return true;
            }
        }

        return false;
    }

    public bool ContainsItem(InventoryItemData itemToAdd, out List<InventorySlot> invSlot)
    {
        invSlot = inventorySlots.Where(i => i.ItemData == itemToAdd).ToList();
        return invSlot == null ? false : true;
    }

    public bool HasFreeSlot(out InventorySlot freeSlot)
    {
        freeSlot = inventorySlots.FirstOrDefault(i => i.ItemData == null);
        return freeSlot == null ? false : true;
    }

    #endregion

}
