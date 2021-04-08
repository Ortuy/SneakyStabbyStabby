﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class Inventory : MonoBehaviourPunCallbacks
{
    //public bool[] isFull;
    public GameObject[] slotSelectionDisplays;
    public int selectedSlot;
    public Image[] itemImages;
    public Player player;

    public Item currentTrap;
    public Item currentPassive;
    public Item[] currentActives;

    public UseMarker[] useMarkers;
    public Color unusedColor, usedColor;

    public void SetActiveItem(int slotID, Item newItem)
    {
        itemImages[slotID].gameObject.SetActive(true);
        itemImages[slotID].sprite = newItem.itemImage;
        currentActives[slotID] = newItem;
        
        if(newItem.maxUses > 1)
        {
            for(int i = 0; i < newItem.maxUses; i++)
            {
                useMarkers[slotID].markers[i].gameObject.SetActive(true);
                useMarkers[slotID].markers[i].color = unusedColor;
            }
        }
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

    public void ChangeSelectedSlot(float direction)
    {
        if(direction > 0)
        {
            slotSelectionDisplays[selectedSlot].SetActive(false);
            selectedSlot++;

            if(selectedSlot >= currentActives.Length)
            {
                selectedSlot = 0;
            }

            slotSelectionDisplays[selectedSlot].SetActive(true);
        }
        else
        {
            slotSelectionDisplays[selectedSlot].SetActive(false);
            selectedSlot--;

            if (selectedSlot < 0)
            {
                selectedSlot = currentActives.Length - 1;
            }

            slotSelectionDisplays[selectedSlot].SetActive(true);
        }
    }

    public void UseItem()
    {
        if (currentActives[selectedSlot] != null)
        {
            var effectID = currentActives[selectedSlot].effectID;

            switch (effectID)
            {
                case 0:
                    player.Spikepit();
                    break;
                case 1:
                    player.Tripwire();
                    break;
                case 2:
                    player.Blindingtrap();
                    break;
                case 3:
                    player.Bomb();
                    break;
                case 4:
                    player.Geltrap();
                    break;
                case 5:
                    player.Shoot();
                    break;
                case 6:
                    player.Blinking();
                    break;

            }           

            currentActives[selectedSlot].usesLeft--;
            useMarkers[selectedSlot].markers[currentActives[selectedSlot].usesLeft].color = usedColor;

            if(currentActives[selectedSlot].usesLeft == 0)
            {
                currentActives[selectedSlot] = null;
                itemImages[selectedSlot].gameObject.SetActive(false);

                for(int i = 0; i < useMarkers[selectedSlot].markers.Length; i++)
                {
                    useMarkers[selectedSlot].markers[i].gameObject.SetActive(false);
                }
            }
            
        }
        


    }
    public void UsePassiveItem()
    {
        if (currentPassive != null && player.canUsePotion)
        {
            var effectID = currentPassive.effectID;

            switch (effectID)
            {
                case 0:
                    player.SprintPotion();
                    break;
                case 1:
                    player.SeePotion();
                    break;
                case 2:
                    int variant = Random.Range(0, player.camoObjects.Length);
                    player.GetComponent<PhotonView>().RPC("CamoSpell", RpcTarget.AllBuffered,variant);
                    break;

            }
            currentPassive = null;
            itemImages[4].gameObject.SetActive(false);
        }



    }

}
