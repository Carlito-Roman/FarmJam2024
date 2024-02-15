using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticInventoryDisplay : InventoryDisplay
{

    [SerializeField] private InventoryHolder inventoryHolder;
    [SerializeField] private InventorySlotUI[] slots;

    protected override void Start()
    {
        base.Start();

        if(inventoryHolder != null) {
            inventorySystem = inventoryHolder.PrimaryInventorySystem;
            inventorySystem.OnInventorySlotChange += UpdateSlot;
        } else {
            Debug.LogWarning($"No Inventory assigned to {this.gameObject}");
        }

        AssignSlot(inventorySystem);
    }

    public override void AssignSlot(InventorySystem inventoryToDisplay)
    {
        slotDictionary = new Dictionary<InventorySlotUI, InventorySlot>();

        if(slots.Length != inventorySystem.InventorySize) { Debug.Log($"Inventory slots out os sync"); }

        for(int i = 0; i < inventorySystem.InventorySize; i++)
        {
            slotDictionary.Add(slots[i], inventorySystem.InventorySlots[i]);
            slots[i].Init(inventorySystem.InventorySlots[i]);
        }
    }
}
