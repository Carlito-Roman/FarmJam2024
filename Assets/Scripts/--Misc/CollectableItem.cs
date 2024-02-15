using Com.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollectableItem : Promptable, IInteractable
{

    [SerializeField] private InventoryItemData itemData;

    UnityAction<IInteractable> IInteractable.OnInteractionComplete { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    #region MonoBehaviour Callbacks

    private void Awake() {
        SetPrompt();
    }

    #endregion

    public void Interact(PlayerInteractionManager interactionManager, out bool successfulInteraction)
    {
        var inventory = FindObjectOfType<PlayerInventoryManager>();

        if (!inventory) { successfulInteraction = false; }

        if (inventory.PrimaryInventorySystem.AddToInventory(itemData, itemData.CollectResourceAmount())) {
            Destroy(this.gameObject);       
        }
        successfulInteraction = true;

    }

    public void EndInteraction()
    {

    }

    #region Private Methods

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

    #endregion
}
