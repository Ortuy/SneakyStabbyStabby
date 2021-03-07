using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Pickup : MonoBehaviourPunCallbacks
{
    //private Inventory inventory;
    public GameObject itemButton;
    //public Sprite itemImage;

    public Item item;

    public GameObject particleEffect;

    private void Start()
    {
        var temp = item;
        item = ScriptableObject.CreateInstance<Item>();
        item.SetParameters(temp);
        //inventory = GameObject.FindGameObjectWithTag("GameController").GetComponent<Inventory>();
        //itemImage = item.itemImage;
        GetComponent<SpriteRenderer>().sprite = item.itemImage;
    }

    private void DestroyPickup()
    {
        var particle = Instantiate(particleEffect, transform.position, Quaternion.identity);
        Destroy(particle, 5f);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var inventory = other.GetComponent<Player>().inventory;

            switch(item.itemType)
            {
                case ItemType.ACTIVE:
                    for (int i = 0; i < inventory.currentActives.Length; i++)
                    {
                        if (inventory.currentActives[i] == null)
                        {
                            //inventory.isFull[i] = true;
                            //Instantiate(itemButton, inventory.slots[i].transform, false);
                            inventory.SetActiveItem(i, item);
                            DestroyPickup();
                            break;
                        }
                    }
                    break;
                case ItemType.PASSIVE:
                    if(inventory.currentPassive == null)
                    {
                        inventory.SetPassiveItem(item);
                        DestroyPickup();
                    }
                    break;
                case ItemType.TRAP:
                    if (inventory.currentTrap == null)
                    {
                        inventory.SetTrapItem(item);
                        DestroyPickup();
                    }
                    break;
            }

            /*
            for (int i = 0; i < inventory.slots.Length; i++)
            {
                if (inventory.isFull[i] == false)
                {
                    inventory.isFull[i] = true;
                    //Instantiate(itemButton, inventory.slots[i].transform, false);
                    inventory.SetItem(i, itemImage);
                    Destroy(gameObject);
                    break;
                }
            }*/
        }
    }
}
