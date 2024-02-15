using Com.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ChestInventory : InventoryHolder, IInteractable
{

    public UnityAction<IInteractable> OnInteractionComplete;

    UnityAction<IInteractable> IInteractable.OnInteractionComplete { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public void Interact(PlayerInteractionManager interactionManager, out bool interactionSuccessful)
    {
        OnInventoryDisplayRequest?.Invoke(primaryInventorySystem);
        interactionSuccessful = true;
    }

    public void EndInteraction()
    {
        throw new System.NotImplementedException();
    }

}
