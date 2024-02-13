using UnityEngine;

[System.Serializable]
public class InventorySlot
{
    #region Variables

    [SerializeField] private InventoryItemData data;
    [SerializeField] private int stackSize;

    public InventoryItemData Data => data;
    public int StackSize => stackSize;

    #endregion

    #region Public Methods

    public InventorySlot(InventoryItemData source, int amount)
    {
        data = source;
        stackSize = amount;
    }

    public InventorySlot()
    {
        ClearSlot();
    }

    public void UpdateInventorySlot(InventoryItemData itemData, int amount)
    {
        data = itemData;
        stackSize = amount;
    }

    public bool RoomLeftInStack(int amountToAdd, out int amountRemaining)
    {
        amountRemaining = data.maxStackCount - stackSize;
        return RoomLeftInStack(amountToAdd);
    }

    public bool RoomLeftInStack(int amountToAdd)
    {
        if(stackSize + amountToAdd <= data.maxStackCount) {
            return true;
        } else {
            return false;
        }
    }

    #region - Clear / Add To / Remove From Stock in Inventory Slot -

    public void ClearSlot()
    {
        data = null;
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
