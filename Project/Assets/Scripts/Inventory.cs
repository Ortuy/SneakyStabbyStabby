using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class Inventory : MonoBehaviourPunCallbacks
{
    //public bool[] isFull;
    public GameObject[] slots;
    public Image[] itemImages;

    public Item currentTrap;
    public Item currentPassive;
    public Item[] currentActives;

    public void SetActiveItem(int slotID, Item newItem)
    {
        itemImages[slotID].gameObject.SetActive(true);
        itemImages[slotID].sprite = newItem.itemImage;
        currentActives[slotID] = newItem;
    }

    public void SetPassiveItem(Item newItem)
    {
        itemImages[4].gameObject.SetActive(true);
        itemImages[4].sprite = newItem.itemImage;
        currentPassive = newItem;
    }

    public void SetTrapItem(Item newItem)
    {
        itemImages[3].gameObject.SetActive(true);
        itemImages[3].sprite = newItem.itemImage;
        currentTrap = newItem;
    }
}
