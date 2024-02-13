using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CursorItemData : MonoBehaviour
{

    public Image itemSprite;
    public TextMeshProUGUI itemCountText;

    private void Awake() {
        InitializeSlot();
    }

    private void InitializeSlot()
    {
        itemSprite.color = Color.clear;
        itemCountText.text = string.Empty;
    }
}
