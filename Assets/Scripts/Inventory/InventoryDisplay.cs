using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public abstract class InventoryDisplay : MonoBehaviour
{

    #region Variables

    [SerializeField] CursorItemData cursorInventoryItem;
    protected InventorySystem inventorySystem;
    protected Dictionary<InventorySlotUI, InventorySlot> slotDictionary;

    public InventorySystem InventorySystem => inventorySystem;
    public Dictionary<InventorySlotUI, InventorySlot> SlotDictionary => slotDictionary;

    #endregion

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

    #region - Clicked Slot Action Check -

    public void SlotClicked(InventorySlotUI clickedUISlot)
    {
        //If holding particular key, split stack

        //Clicked slot has an item - Mouse doesn't have an item
        if(clickedUISlot.AssignedInventorySlot.ItemData != null && cursorInventoryItem.assignedSlot.ItemData == null) {
            cursorInventoryItem.UpdateCursorSlot(clickedUISlot.AssignedInventorySlot);
            clickedUISlot.ClearSlot();
            return;
        }

        //Clicked slot doesn't have an item - Mouse has an item
        if(clickedUISlot.AssignedInventorySlot.ItemData == null && cursorInventoryItem.assignedSlot.ItemData != null)
        {
            clickedUISlot.AssignedInventorySlot.AssignItem(cursorInventoryItem.assignedSlot);
            clickedUISlot.UpdateUISlot();
            cursorInventoryItem.ClearSlot();
        }


        //Clicked slot has an item - Mouse has an item
        if (clickedUISlot.AssignedInventorySlot.ItemData != null && cursorInventoryItem.assignedSlot.ItemData != null)
        {
            bool isSameItem = clickedUISlot.AssignedInventorySlot.ItemData == cursorInventoryItem.assignedSlot.ItemData;

            //Combine if the same
            if (isSameItem && clickedUISlot.AssignedInventorySlot.isThereRoomInStack(cursorInventoryItem.assignedSlot.StackSize)) {
                clickedUISlot.AssignedInventorySlot.AssignItem(cursorInventoryItem.assignedSlot);
                clickedUISlot.UpdateUISlot();

                cursorInventoryItem.ClearSlot();
            }

            //Is slot + mouse > max stack size? Take from mouse
            else if (isSameItem && !clickedUISlot.AssignedInventorySlot.isThereRoomInStack(cursorInventoryItem.assignedSlot.StackSize, out int leftInStack)) {
                
                //Stack is full - Swap
                if(leftInStack < 1) { SwapSlots(clickedUISlot); } 

                //Slot is not at max count, add what is needed from mouse/cursor
                else { 
                    int remaingOnCursor = cursorInventoryItem.assignedSlot.StackSize - leftInStack;
                    clickedUISlot.AssignedInventorySlot.AddToStack(leftInStack);
                    clickedUISlot.UpdateUISlot();

                    var newItem = new InventorySlot(cursorInventoryItem.assignedSlot.ItemData, remaingOnCursor);
                    cursorInventoryItem.ClearSlot();
                    cursorInventoryItem.UpdateCursorSlot(newItem);
                }
            }

            //Swap if different
            if(clickedUISlot.AssignedInventorySlot.ItemData != cursorInventoryItem.assignedSlot.ItemData)
            {
                SwapSlots(clickedUISlot);
            }
        }

    }

    #endregion

    #region Private Methods

    private void SwapSlots(InventorySlotUI clickedUISlot)
    {
        var duplicateSlot = new InventorySlot(cursorInventoryItem.assignedSlot.ItemData, cursorInventoryItem.assignedSlot.StackSize);
        cursorInventoryItem.ClearSlot();

        cursorInventoryItem.UpdateCursorSlot(clickedUISlot.AssignedInventorySlot);
        clickedUISlot.ClearSlot();
        clickedUISlot.AssignedInventorySlot.AssignItem(duplicateSlot);
        clickedUISlot.UpdateUISlot();
    }

    #endregion

}
