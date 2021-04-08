using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType { ACTIVE, PASSIVE, TRAP }

[CreateAssetMenu(fileName = "Item", menuName = "Item", order = 1)]
public class Item : ScriptableObject
{
    public Sprite itemImage;
    public string itemName;
    public string itemDesc;
    public ItemType itemType;
    public int effectID;
    public int maxUses;
    public int usesLeft;

    public void SetParameters(Item baseItem)
    {
        itemImage = baseItem.itemImage;
        itemName = baseItem.itemName;
        itemDesc = baseItem.itemDesc;
        itemType = baseItem.itemType;
        effectID = baseItem.effectID;
        maxUses = baseItem.maxUses;
        usesLeft = baseItem.maxUses;
    }
}
