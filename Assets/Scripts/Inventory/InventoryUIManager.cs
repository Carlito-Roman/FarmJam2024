using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryUIManager : MonoBehaviour
{

    public DynamicInventoryDisplay inventoryPanel;

    private void OnEnable()
    {
        InventoryHolder.OnInventoryDisplayRequest += DisplayInventory;
    }

    private void OnDisable()
    {
        InventoryHolder.OnInventoryDisplayRequest -= DisplayInventory;
    }

    // Update is called once per frame
    void Update()
    {
        if(Keyboard.current.bKey.wasPressedThisFrame)
        {
            DisplayInventory(new InventorySystem(27));
        }

        if(inventoryPanel.gameObject.activeInHierarchy && Keyboard.current.escapeKey.wasPressedThisFrame) { inventoryPanel.gameObject.SetActive(false); }
    }

    private void DisplayInventory(InventorySystem invToDisplay)
    {
        inventoryPanel.gameObject.SetActive(true);
        inventoryPanel.RefreshDynamicInventory(invToDisplay);
    }
}
