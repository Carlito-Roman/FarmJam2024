using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryManager : InventoryHolder
{

    [SerializeField] protected int secondaryInventorySize;
    [SerializeField] protected InventorySystem secondaryInventorySystem;

    public InventorySystem SecondaryInventorySystem => secondaryInventorySystem;

    protected override void Awake()
    {
        base.Awake();

        secondaryInventorySystem = new InventorySystem(secondaryInventorySize);
    }

    public void OpenBackpackUI()
    {
        OnInventoryDisplayRequest?.Invoke(secondaryInventorySystem);
    }

    public bool AddToInventory()
    {

    }

}
