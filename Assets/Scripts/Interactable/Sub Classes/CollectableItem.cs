using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableItem : Interactable
{

    [SerializeField] private InventoryItemData itemData;


    private void Awake()
    {
        SetPrompt();
    }

    protected override void Interact()
    {
        base.Interact();
        var inventory = FindObjectOfType<InventoryHolder>();

        if (inventory == null) { return; }

        if (inventory.InventorySystem.AddToInventory(itemData, itemData.CollectResourceAmount()))
        {
            Destroy(this.gameObject);
        }
    }

    private void SetPrompt()
    {

        string actionPhrase = string.Empty;

        switch(itemData.itemType)
        {
            case ItemType.Plant:
                actionPhrase = "Harvest" + " ";
                break;
            case ItemType.Seed:
                actionPhrase = "Collect" + " ";
                break;
            case ItemType.Potion:
                actionPhrase = "Stow Away" + " ";
                break;
            default:
                actionPhrase = "Pick Up" + " ";
                break;
        }

        promptedMessage = actionPhrase + itemData.itemDisplayName;
    }
}
