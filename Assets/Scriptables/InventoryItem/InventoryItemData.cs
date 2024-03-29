using UnityEngine;

[CreateAssetMenu(fileName = "New Item Data", menuName = "Inventory/ Item Data")]
public class InventoryItemData : ScriptableObject
{

    #region Variables

    [Header("Meta Data")]
    public int itemID;
    public Sprite itemIcon;

    [Header("Name/Description")]
    public ItemType itemType = ItemType.Undefined;
    public string itemDisplayName;
    [TextArea(4, 4)]
    public string itemDescription;

    [Header("Collection Rate")]
    public Vector2 collectionRate;

    [Header("Max Stack Limit")]
    public int maxStackCount = 5;

    [Header("Value")]
    public int goldValue;

    #endregion

    #region Public Methods

    public int CollectResourceAmount()
    {
        return Random.Range((int)collectionRate.x, (int)collectionRate.y + 1);
    }

    #endregion
}
