using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DynamicInventoryDisplay : InventoryDisplay
{

    [SerializeField] protected InventorySlotUI slotPrefab;

    protected override void Start()
    {
        base.Start();
    }

    public void RefreshDynamicInventory(InventorySystem invToDisplay)
    {
        ClearSlots();
        inventorySystem = invToDisplay;
        AssignSlot(invToDisplay);
    }

    public override void AssignSlot(InventorySystem inventoryToDisplay)
    {
        slotDictionary = new Dictionary<InventorySlotUI, InventorySlot>();

        if(inventoryToDisplay == null) { return; }

        for(int i = 0; i < inventoryToDisplay.InventorySize; i++)
        {
            var uiSlot = Instantiate(slotPrefab, transform);
            slotDictionary.Add(uiSlot, inventoryToDisplay.InventorySlots[i]);
            uiSlot.Init(inventoryToDisplay.InventorySlots[i]);
            uiSlot.UpdateUISlot();
        }
    }

    private void ClearSlots()
    {
        foreach(var item in transform.Cast<Transform>())
        {
            Destroy(item.gameObject);
        }

        if(slotDictionary != null)
        {
            slotDictionary.Clear();
        }

    }
}
