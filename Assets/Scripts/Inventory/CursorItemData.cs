using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using TMPro;

public class CursorItemData : MonoBehaviour
{

    #region Variables

    [Header("Cursor Data Variables")]
    [SerializeField] private Image itemSprite;
    [SerializeField] private TextMeshProUGUI itemCountText;
    [HideInInspector] public InventorySlot assignedSlot;

    #endregion

    #region MonoBehaviour Callbacks

    private void Awake() {
        InitializeSlot();
    }

    private void Update()
    {
        if(assignedSlot.ItemData != null)
        {
            transform.position = Mouse.current.position.ReadValue();
            if(Mouse.current.leftButton.wasPressedThisFrame && !IsPointerOverUIObject())
            {
                ClearSlot();
            }
        }
    }

    #endregion

    #region Public Methods

    public void UpdateCursorSlot(InventorySlot invSlot)
    {
        assignedSlot.AssignItem(invSlot);
        itemSprite.sprite = invSlot.ItemData.itemIcon;
        itemCountText.text = invSlot.StackSize.ToString();
        itemSprite.color = Color.white;
    }

    public void ClearSlot()
    {
        assignedSlot.ClearSlot();
        itemCountText.text = string.Empty;
        itemSprite.color = Color.clear;
        itemSprite.sprite = null;
    }

    public static bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = Mouse.current.position.ReadValue();
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

    #endregion

    #region Private Methods

    private void InitializeSlot()
    {
        itemSprite.color = Color.clear;
        itemCountText.text = string.Empty;
    }

    #endregion

}
