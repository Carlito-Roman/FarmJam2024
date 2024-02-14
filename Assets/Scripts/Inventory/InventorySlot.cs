using UnityEngine;

[System.Serializable]
public class InventorySlot
{
    #region Variables

    [SerializeField] private InventoryItemData itemData;
    [SerializeField] private int stackSize;

    public InventoryItemData ItemData => itemData;
    public int StackSize => stackSize;

    #endregion

    #region Public Methods

    public InventorySlot(InventoryItemData source, int amount)
    {
        itemData = source;
        stackSize = amount;
    }

    public InventorySlot()
    {
        ClearSlot();
    }

    public void UpdateInventorySlot(InventoryItemData itemData, int amount)
    {
        this.itemData = itemData;
        stackSize = amount;
    }

    public void AssignItem(InventorySlot invSlot)
    {
        if (itemData == invSlot.itemData)
        {
            AddToStack(invSlot.stackSize);
        }
        else
        {
            itemData = invSlot.itemData;
            stackSize = 0;
            AddToStack(invSlot.stackSize);
        }
    }

    #region - Check if there is room left -

    public bool isThereRoomInStack(int amountToAdd, out int amountRemaining)
    {
        amountRemaining = itemData.maxStackCount - stackSize;
        return isThereRoomInStack(amountToAdd);
    }

    public bool isThereRoomInStack(int amountToAdd)
    {
        if(itemData == null || itemData != null && stackSize + amountToAdd <= itemData.maxStackCount) {
            return true;
        } else {
            return false;
        }
    }

    #endregion 

    #region - Clear / Add To / Remove From Stock in Inventory Slot -

    public void ClearSlot()
    {
        itemData = null;
        stackSize = -1;
    }

    public void AddToStack(int amount)
    {
        stackSize += amount;
    }

    public void RemoveFromStack(int amount)
    {
        stackSize -= amount;
    }

    #endregion


    #endregion

}
