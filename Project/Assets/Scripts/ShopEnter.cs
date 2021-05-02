using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class ShopEnter : InteractableObject
{
    public Player player;
    public GameObject Shop;

    public int blidingtrapprice = 10;
    public int blinkPrice = 10;
    public int bombPrice = 10;
    public int camoPrice = 10;
    public int crossbowPrice = 10;
    public int detectorPrice = 10;
    public int silentPotionPrice = 10;
    public int gelTrapPrice = 10;
    public int spikePitPrice = 10;
    public int tripWireTrapPrice = 10;
    public int seePotionPrice = 10;
    public int speedPotionPrice = 10;

    public int shopSlotRange = 0;
    public int shopSlotRange1 = 0;
    public int shopSlotRange2 = 0;

    public GameObject buyBlinding, buyBlink, buyBomb, buyCamo, buyCrossbow, buyDetector, buySilentPotion, buyGelTrap, buySpikePit, buyTripWire, buySeePotion, buySpeedPotion;
    public GameObject buyBlinding1, buyBlink1, buyBomb1, buyCamo1, buyCrossbow1, buyDetector1, buySilentPotion1, buyGelTrap1, buySpikePit1, buyTripWire1, buySeePotion1, buySpeedPotion1;
    public GameObject buyBlinding2, buyBlink2, buyBomb2, buyCamo2, buyCrossbow2, buyDetector2, buySilentPotion2, buyGelTrap2, buySpikePit2, buyTripWire2, buySeePotion2, buySpeedPotion2;

    public void Awake()
    {

        Shop.SetActive(false);

        buyBlinding.SetActive(false);
        buyBlink.SetActive(false);
        buyBomb.SetActive(false);
        buyCamo.SetActive(false);
        buyCrossbow.SetActive(false);
        buyDetector.SetActive(false);
        buySilentPotion.SetActive(false);
        buyGelTrap.SetActive(false);
        buySpikePit.SetActive(false);
        buyTripWire.SetActive(false);
        buySeePotion.SetActive(false);
        buySpeedPotion.SetActive(false);

        buyBlinding1.SetActive(false);
        buyBlink1.SetActive(false);
        buyBomb1.SetActive(false);
        buyCamo1.SetActive(false);
        buyCrossbow1.SetActive(false);
        buyDetector1.SetActive(false);
        buySilentPotion1.SetActive(false);
        buyGelTrap1.SetActive(false);
        buySpikePit1.SetActive(false);
        buyTripWire1.SetActive(false);
        buySeePotion1.SetActive(false);
        buySpeedPotion1.SetActive(false);

        buyBlinding2.SetActive(false);
        buyBlink2.SetActive(false);
        buyBomb2.SetActive(false);
        buyCamo2.SetActive(false);
        buyCrossbow2.SetActive(false);
        buyDetector2.SetActive(false);
        buySilentPotion2.SetActive(false);
        buyGelTrap2.SetActive(false);
        buySpikePit2.SetActive(false);
        buyTripWire2.SetActive(false);
        buySeePotion2.SetActive(false);
        buySpeedPotion2.SetActive(false);

        //if (photonView.IsMine)
        //{
        //    shopSlotRange = Random.Range(0, 12);
        //    shopSlotRange1 = Random.Range(0, 12);
        //    shopSlotRange2 = Random.Range(0, 12);
        //}
        if(PhotonNetwork.IsMasterClient)
        {
            RandomItem();
            RandomItem1();
            RandomItem2();
        }
        

    }

    public void Update()
    {
        if (shopSlotRange == 1)
        {
            buyBlinding.SetActive(true);

        }
        if (shopSlotRange == 2)
        {
            buyBlink.SetActive(true);


        }
        if (shopSlotRange == 3)
        {
            buyBomb.SetActive(true);

        }
        if (shopSlotRange == 4)
        {
            buyCamo.SetActive(true);

        }
        if (shopSlotRange == 5)
        {
            buyCrossbow.SetActive(true);

        }
        if (shopSlotRange == 6)
        {
            buyDetector.SetActive(true);

        }
        if (shopSlotRange == 7)
        {
            buySilentPotion.SetActive(true);

        }
        if (shopSlotRange == 8)
        {
            buyGelTrap.SetActive(true);

        }
        if (shopSlotRange == 9)
        {
            buySpikePit.SetActive(true);

        }
        if (shopSlotRange == 10)
        {
            buyTripWire.SetActive(true);

        }
        if (shopSlotRange == 11)
        {
            buySeePotion.SetActive(true);

        }
        if (shopSlotRange == 12)
        {
            buySpeedPotion.SetActive(true);
        }
        if (shopSlotRange1 == 1)
        {
            buyBlinding1.SetActive(true);

        }
        if (shopSlotRange1 == 2)
        {
            buyBlink1.SetActive(true);


        }
        if (shopSlotRange1 == 3)
        {
            buyBomb1.SetActive(true);

        }
        if (shopSlotRange1 == 4)
        {
            buyCamo1.SetActive(true);

        }
        if (shopSlotRange1 == 5)
        {
            buyCrossbow1.SetActive(true);

        }
        if (shopSlotRange1 == 6)
        {
            buyDetector1.SetActive(true);

        }
        if (shopSlotRange1 == 7)
        {
            buySilentPotion1.SetActive(true);

        }
        if (shopSlotRange1 == 8)
        {
            buyGelTrap1.SetActive(true);

        }
        if (shopSlotRange1 == 9)
        {
            buySpikePit1.SetActive(true);

        }
        if (shopSlotRange1 == 10)
        {
            buyTripWire1.SetActive(true);

        }
        if (shopSlotRange1 == 11)
        {
            buySeePotion1.SetActive(true);

        }
        if (shopSlotRange1 == 12)
        {
            buySpeedPotion1.SetActive(true);
        }
        if (shopSlotRange2 == 1)
        {
            buyBlinding2.SetActive(true);

        }
        if (shopSlotRange2 == 2)
        {
            buyBlink2.SetActive(true);


        }
        if (shopSlotRange2 == 3)
        {
            buyBomb2.SetActive(true);

        }
        if (shopSlotRange2 == 4)
        {
            buyCamo2.SetActive(true);

        }
        if (shopSlotRange2 == 5)
        {
            buyCrossbow2.SetActive(true);

        }
        if (shopSlotRange2 == 6)
        {
            buyDetector2.SetActive(true);

        }
        if (shopSlotRange2 == 7)
        {
            buySilentPotion2.SetActive(true);

        }
        if (shopSlotRange2 == 8)
        {
            buyGelTrap2.SetActive(true);

        }
        if (shopSlotRange2 == 9)
        {
            buySpikePit2.SetActive(true);

        }
        if (shopSlotRange2 == 10)
        {
            buyTripWire2.SetActive(true);

        }
        if (shopSlotRange2 == 11)
        {
            buySeePotion2.SetActive(true);

        }
        if (shopSlotRange2 == 12)
        {
            buySpeedPotion2.SetActive(true);
        }
    }
    public void RandomItem()
    {


            var temp = Random.Range(1, 13);
            photonView.RPC("SyncItemNumber", RpcTarget.AllBuffered, temp);

    }
    public void RandomItem1()
    {

            var temp1 = Random.Range(1, 13);
            photonView.RPC("SyncItemNumber1", RpcTarget.AllBuffered, temp1);

    }
    public void RandomItem2()
    {


            var temp2 = Random.Range(1, 13);

            photonView.RPC("SyncItemNumber2", RpcTarget.AllBuffered, temp2);

    }
    [PunRPC]
    private void SyncItemNumber(int number)
    {
        shopSlotRange = number;
    }
    [PunRPC]
    private void SyncItemNumber1(int number)
    {
        shopSlotRange1 = number;
    }
    [PunRPC]
    private void SyncItemNumber2(int number)
    {
        shopSlotRange2 = number;
    }


    [PunRPC]
    public void SyncBuyBlinding()
    {
        buyBlinding.SetActive(false);
    }
    [PunRPC]
    public void SyncBuyBlink()
    {
        buyBlink.SetActive(false);
    }
    [PunRPC]
    public void SyncBuyBomb()
    {
        buyBomb.SetActive(false);
    }
    [PunRPC]
    public void SyncBuyCamo()
    {
        buyCamo.SetActive(false);
    }
    [PunRPC]
    public void SyncBuyCrossbow()
    {
        buyCrossbow.SetActive(false);
    }
    [PunRPC]
    public void SyncBuyDetector()
    {
        buyDetector.SetActive(false);
    }
    [PunRPC]
    public void SyncBuySilentPotion()
    {
        buySilentPotion.SetActive(false);
    }
    [PunRPC]
    public void SyncBuyGelTrap()
    {
        buyGelTrap.SetActive(false);
    }
    [PunRPC]
    public void SyncBuySpikePit()
    {
        buySpikePit.SetActive(false);
    }
    [PunRPC]
    public void SyncBuyTripWire()
    {
        buyTripWire.SetActive(false);
    }
    [PunRPC]
    public void SyncBuySeePotion()
    {
        buySeePotion.SetActive(false);
    }
    [PunRPC]
    public void SyncBuySpeedPotion()
    {
        buySpeedPotion.SetActive(false);
    }


    [PunRPC]
    public void SyncBuyBlinding1()
    {
        buyBlinding1.SetActive(false);
    }
    [PunRPC]
    public void SyncBuyBlink1()
    {
        buyBlink1.SetActive(false);
    }
    [PunRPC]
    public void SyncBuyBomb1()
    {
        buyBomb1.SetActive(false);
    }
    [PunRPC]
    public void SyncBuyCamo1()
    {
        buyCamo1.SetActive(false);
    }
    [PunRPC]
    public void SyncBuyCrossbow1()
    {
        buyCrossbow1.SetActive(false);
    }
    [PunRPC]
    public void SyncBuyDetector1()
    {
        buyDetector1.SetActive(false);
    }
    [PunRPC]
    public void SyncBuySilentPotion1()
    {
        buySilentPotion1.SetActive(false);
    }
    [PunRPC]
    public void SyncBuyGelTrap1()
    {
        buyGelTrap1.SetActive(false);
    }
    [PunRPC]
    public void SyncBuySpikePit1()
    {
        buySpikePit1.SetActive(false);
    }
    [PunRPC]
    public void SyncBuyTripWire1()
    {
        buyTripWire1.SetActive(false);
    }
    [PunRPC]
    public void SyncBuySeePotion1()
    {
        buySeePotion1.SetActive(false);
    }
    [PunRPC]
    public void SyncBuySpeedPotion1()
    {
        buySpeedPotion1.SetActive(false);
    }


    [PunRPC]
    public void SyncBuyBlinding2()
    {
        buyBlinding2.SetActive(false);
    }
    [PunRPC]
    public void SyncBuyBlink2()
    {
        buyBlink2.SetActive(false);
    }
    [PunRPC]
    public void SyncBuyBomb2()
    {
        buyBomb2.SetActive(false);
    }
    [PunRPC]
    public void SyncBuyCamo2()
    {
        buyCamo2.SetActive(false);
    }
    [PunRPC]
    public void SyncBuyCrossbow2()
    {
        buyCrossbow2.SetActive(false);
    }
    [PunRPC]
    public void SyncBuyDetector2()
    {
        buyDetector2.SetActive(false);
    }
    [PunRPC]
    public void SyncBuySilentPotion2()
    {
        buySilentPotion2.SetActive(false);
    }
    [PunRPC]
    public void SyncBuyGelTrap2()
    {
        buyGelTrap2.SetActive(false);
    }
    [PunRPC]
    public void SyncBuySpikePit2()
    {
        buySpikePit2.SetActive(false);
    }
    [PunRPC]
    public void SyncBuyTripWire2()
    {
        buyTripWire2.SetActive(false);
    }
    [PunRPC]
    public void SyncBuySeePotion2()
    {
        buySeePotion2.SetActive(false);
    }
    [PunRPC]
    public void SyncBuySpeedPotion2()
    {
        buySpeedPotion2.SetActive(false);
    }

    protected override void StartInteraction()
    {
        player = targetPV.GetComponent<Player>();
        Shop.SetActive(true);
        player.stabLock = true;
        base.StartInteraction();
    }

    protected override void EndInteraction()
    {
        player = targetPV.GetComponent<Player>();
        Shop.SetActive(false);
        player.stabLock = false;
        base.EndInteraction();
    }

    //public void OnTriggerEnter2D(Collider2D collision)
    //{

    //    if (collision.CompareTag("Player") && collision.GetComponent<PhotonView>().IsMine)
    //    {
    //        player = collision.GetComponent<Player>();
    //        Shop.SetActive(true);
    //    }

    //}
    //public void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Player") && collision.GetComponent<PhotonView>().IsMine)
    //    {
    //        player = collision.GetComponent<Player>();
    //        Shop.SetActive(false);
    //    }

    //}
    public void BuyBlinding()
    {
        if (player.gold >= blidingtrapprice)
        {
            PhotonNetwork.Instantiate(player.pickup1.name, new Vector2(player.paintPos.transform.position.x, player.paintPos.transform.position.y), Quaternion.identity, 0);
            player.gold -= blidingtrapprice;
            buyBlinding.SetActive(false);
            photonView.RPC("SyncBuyBlinding", RpcTarget.AllBuffered);
            RandomItem();
        }

    }
    public void BuyBlink()
    {
        if (player.gold >= blinkPrice)
        {
            PhotonNetwork.Instantiate(player.pickup2.name, new Vector2(player.paintPos.transform.position.x, player.paintPos.transform.position.y), Quaternion.identity, 0);
            player.gold -= blinkPrice;
            buyBlink.SetActive(false);
            photonView.RPC("SyncBuyBlink", RpcTarget.AllBuffered);
            RandomItem();
        }

    }
    public void BuyBomb()
    {
        if (player.gold >= bombPrice)
        {
            PhotonNetwork.Instantiate(player.pickup3.name, new Vector2(player.paintPos.transform.position.x, player.paintPos.transform.position.y), Quaternion.identity, 0);
            player.gold -= bombPrice;
            buyBomb.SetActive(false);
            photonView.RPC("SyncBuyBomb", RpcTarget.AllBuffered);
            RandomItem();
        }

    }
    public void BuyCamo()
    {
        if (player.gold >= camoPrice)
        {
            PhotonNetwork.Instantiate(player.pickup4.name, new Vector2(player.paintPos.transform.position.x, player.paintPos.transform.position.y), Quaternion.identity, 0);
            player.gold -= camoPrice;
            buyCamo.SetActive(false);
            photonView.RPC("SyncBuyCamo", RpcTarget.AllBuffered);
            RandomItem();
        }

    }
    public void BuyCrossbow()
    {
        if (player.gold >= crossbowPrice)
        {
            PhotonNetwork.Instantiate(player.pickup5.name, new Vector2(player.paintPos.transform.position.x, player.paintPos.transform.position.y), Quaternion.identity, 0);
            player.gold -= crossbowPrice;
            buyCrossbow.SetActive(false);
            photonView.RPC("SyncBuyCrossbow", RpcTarget.AllBuffered);
            RandomItem();
        }

    }
    public void BuyDetector()
    {
        if (player.gold >= detectorPrice)
        {
            PhotonNetwork.Instantiate(player.pickup6.name, new Vector2(player.paintPos.transform.position.x, player.paintPos.transform.position.y), Quaternion.identity, 0);
            player.gold -= detectorPrice;
            buyDetector.SetActive(false);
            photonView.RPC("SyncBuyDetector", RpcTarget.AllBuffered);
            RandomItem();
        }

    }
    public void BuySilentPotion()
    {
        if (player.gold >= silentPotionPrice)
        {
            PhotonNetwork.Instantiate(player.pickup7.name, new Vector2(player.paintPos.transform.position.x, player.paintPos.transform.position.y), Quaternion.identity, 0);
            player.gold -= silentPotionPrice;
            buySilentPotion.SetActive(false);
            photonView.RPC("SyncBuySilentPotion", RpcTarget.AllBuffered);
            RandomItem();
        }

    }
    public void BuyGelTrap()
    {
        if (player.gold >= gelTrapPrice)
        {
            PhotonNetwork.Instantiate(player.pickup8.name, new Vector2(player.paintPos.transform.position.x, player.paintPos.transform.position.y), Quaternion.identity, 0);
            player.gold -= gelTrapPrice;
            buyGelTrap.SetActive(false);
            photonView.RPC("SyncBuyGelTrap", RpcTarget.AllBuffered);
            RandomItem();
        }

    }
    public void BuySpikePit()
    {
        if (player.gold >= spikePitPrice)
        {
            PhotonNetwork.Instantiate(player.pickup9.name, new Vector2(player.paintPos.transform.position.x, player.paintPos.transform.position.y), Quaternion.identity, 0);
            player.gold -= spikePitPrice;
            buySpikePit.SetActive(false);
            photonView.RPC("SyncBuySpikePit", RpcTarget.AllBuffered);
            RandomItem();
        }

    }
    public void BuyTripWire()
    {
        if (player.gold >= tripWireTrapPrice)
        {
            PhotonNetwork.Instantiate(player.pickup10.name, new Vector2(player.paintPos.transform.position.x, player.paintPos.transform.position.y), Quaternion.identity, 0);
            player.gold -= tripWireTrapPrice;
            buyTripWire.SetActive(false);
            photonView.RPC("SyncBuyTripWire", RpcTarget.AllBuffered);
            RandomItem();
        }

    }
    public void BuySeePotion()
    {
        if (player.gold >= seePotionPrice)
        {
            PhotonNetwork.Instantiate(player.pickup11.name, new Vector2(player.paintPos.transform.position.x, player.paintPos.transform.position.y), Quaternion.identity, 0);
            player.gold -= seePotionPrice;
            buySeePotion.SetActive(false);
            photonView.RPC("SyncBuySeePotion", RpcTarget.AllBuffered);
            RandomItem();
        }

    }
    public void BuySpeedPotion()
    {
        if (player.gold >= speedPotionPrice)
        {
            PhotonNetwork.Instantiate(player.pickup12.name, new Vector2(player.paintPos.transform.position.x, player.paintPos.transform.position.y), Quaternion.identity, 0);
            player.gold -= speedPotionPrice;
            buySpeedPotion.SetActive(false);
            photonView.RPC("SyncBuySpeedPotion", RpcTarget.AllBuffered);
            RandomItem();
        }

    }
    public void BuyBlinding1()
    {
        if (player.gold >= blidingtrapprice)
        {
            PhotonNetwork.Instantiate(player.pickup1.name, new Vector2(player.paintPos.transform.position.x, player.paintPos.transform.position.y), Quaternion.identity, 0);
            player.gold -= blidingtrapprice;
            buyBlinding1.SetActive(false);
            photonView.RPC("SyncBuyBlinding1", RpcTarget.AllBuffered);
            RandomItem1();
        }

    }
    public void BuyBlink1()
    {
        if (player.gold >= blinkPrice)
        {
            PhotonNetwork.Instantiate(player.pickup2.name, new Vector2(player.paintPos.transform.position.x, player.paintPos.transform.position.y), Quaternion.identity, 0);
            player.gold -= blinkPrice;
            buyBlink1.SetActive(false);
            photonView.RPC("SyncBuyBlink1", RpcTarget.AllBuffered);
            RandomItem1();
        }

    }
    public void BuyBomb1()
    {
        if (player.gold >= bombPrice)
        {
            PhotonNetwork.Instantiate(player.pickup3.name, new Vector2(player.paintPos.transform.position.x, player.paintPos.transform.position.y), Quaternion.identity, 0);
            player.gold -= bombPrice;
            buyBomb1.SetActive(false);
            photonView.RPC("SyncBuyBomb1", RpcTarget.AllBuffered);
            RandomItem1();
        }

    }
    public void BuyCamo1()
    {
        if (player.gold >= camoPrice)
        {
            PhotonNetwork.Instantiate(player.pickup4.name, new Vector2(player.paintPos.transform.position.x, player.paintPos.transform.position.y), Quaternion.identity, 0);
            player.gold -= camoPrice;
            buyCamo1.SetActive(false);
            photonView.RPC("SyncBuyCamo1", RpcTarget.AllBuffered);
            RandomItem1();
        }

    }
    public void BuyCrossbow1()
    {
        if (player.gold >= crossbowPrice)
        {
            PhotonNetwork.Instantiate(player.pickup5.name, new Vector2(player.paintPos.transform.position.x, player.paintPos.transform.position.y), Quaternion.identity, 0);
            player.gold -= crossbowPrice;
            buyCrossbow1.SetActive(false);
            photonView.RPC("SyncBuyCrossbow1", RpcTarget.AllBuffered);
            RandomItem1();
        }

    }
    public void BuyDetector1()
    {
        if (player.gold >= detectorPrice)
        {
            PhotonNetwork.Instantiate(player.pickup6.name, new Vector2(player.paintPos.transform.position.x, player.paintPos.transform.position.y), Quaternion.identity, 0);
            player.gold -= detectorPrice;
            buyDetector1.SetActive(false);
            photonView.RPC("SyncBuyDetector1", RpcTarget.AllBuffered);
            RandomItem1();
        }

    }
    public void BuySilentPotion1()
    {
        if (player.gold >= silentPotionPrice)
        {
            PhotonNetwork.Instantiate(player.pickup7.name, new Vector2(player.paintPos.transform.position.x, player.paintPos.transform.position.y), Quaternion.identity, 0);
            player.gold -= silentPotionPrice;
            buySilentPotion1.SetActive(false);
            photonView.RPC("SyncBuySilentPotion1", RpcTarget.AllBuffered);
            RandomItem1();
        }

    }
    public void BuyGelTrap1()
    {
        if (player.gold >= gelTrapPrice)
        {
            PhotonNetwork.Instantiate(player.pickup8.name, new Vector2(player.paintPos.transform.position.x, player.paintPos.transform.position.y), Quaternion.identity, 0);
            player.gold -= gelTrapPrice;
            buyGelTrap1.SetActive(false);
            photonView.RPC("SyncBuyGelTrap1", RpcTarget.AllBuffered);
            RandomItem1();
        }

    }
    public void BuySpikePit1()
    {
        if (player.gold >= spikePitPrice)
        {
            PhotonNetwork.Instantiate(player.pickup9.name, new Vector2(player.paintPos.transform.position.x, player.paintPos.transform.position.y), Quaternion.identity, 0);
            player.gold -= spikePitPrice;
            buySpikePit1.SetActive(false);
            photonView.RPC("SyncBuySpikePit1", RpcTarget.AllBuffered);
            RandomItem1();
        }

    }
    public void BuyTripWire1()
    {
        if (player.gold >= tripWireTrapPrice)
        {
            PhotonNetwork.Instantiate(player.pickup10.name, new Vector2(player.paintPos.transform.position.x, player.paintPos.transform.position.y), Quaternion.identity, 0);
            player.gold -= tripWireTrapPrice;
            buyTripWire1.SetActive(false);
            photonView.RPC("SyncBuyTripWire1", RpcTarget.AllBuffered);
            RandomItem1();
        }

    }
    public void BuySeePotion1()
    {
        if (player.gold >= seePotionPrice)
        {
            PhotonNetwork.Instantiate(player.pickup11.name, new Vector2(player.paintPos.transform.position.x, player.paintPos.transform.position.y), Quaternion.identity, 0);
            player.gold -= seePotionPrice;
            buySeePotion1.SetActive(false);
            photonView.RPC("SyncBuySeePotion1", RpcTarget.AllBuffered);
            RandomItem1();
        }

    }
    public void BuySpeedPotion1()
    {
        if (player.gold >= speedPotionPrice)
        {
            PhotonNetwork.Instantiate(player.pickup12.name, new Vector2(player.paintPos.transform.position.x, player.paintPos.transform.position.y), Quaternion.identity, 0);
            player.gold -= speedPotionPrice;
            buySpeedPotion1.SetActive(false);
            photonView.RPC("SyncBuySpeedPotion1", RpcTarget.AllBuffered);
            RandomItem1();
        }

    }
    public void BuyBlinding2()
    {
        if (player.gold >= blidingtrapprice)
        {
            PhotonNetwork.Instantiate(player.pickup1.name, new Vector2(player.paintPos.transform.position.x, player.paintPos.transform.position.y), Quaternion.identity, 0);
            player.gold -= blidingtrapprice;
            buyBlinding2.SetActive(false);
            photonView.RPC("SyncBuyBlinding2", RpcTarget.AllBuffered);
            RandomItem2();
        }

    }
    public void BuyBlink2()
    {
        if (player.gold >= blinkPrice)
        {
            PhotonNetwork.Instantiate(player.pickup2.name, new Vector2(player.paintPos.transform.position.x, player.paintPos.transform.position.y), Quaternion.identity, 0);
            player.gold -= blinkPrice;
            buyBlink2.SetActive(false);
            photonView.RPC("SyncBuyBlink2", RpcTarget.AllBuffered);
            RandomItem2();
        }

    }
    public void BuyBomb2()
    {
        if (player.gold >= bombPrice)
        {
            PhotonNetwork.Instantiate(player.pickup3.name, new Vector2(player.paintPos.transform.position.x, player.paintPos.transform.position.y), Quaternion.identity, 0);
            player.gold -= bombPrice;
            buyBomb2.SetActive(false);
            photonView.RPC("SyncBuyBomb2", RpcTarget.AllBuffered);
            RandomItem2();
        }

    }
    public void BuyCamo2()
    {
        if (player.gold >= camoPrice)
        {
            PhotonNetwork.Instantiate(player.pickup4.name, new Vector2(player.paintPos.transform.position.x, player.paintPos.transform.position.y), Quaternion.identity, 0);
            player.gold -= camoPrice;
            buyCamo2.SetActive(false);
            photonView.RPC("SyncBuyCamo2", RpcTarget.AllBuffered);
            RandomItem2();
        }

    }
    public void BuyCrossbow2()
    {
        if (player.gold >= crossbowPrice)
        {
            PhotonNetwork.Instantiate(player.pickup5.name, new Vector2(player.paintPos.transform.position.x, player.paintPos.transform.position.y), Quaternion.identity, 0);
            player.gold -= crossbowPrice;
            buyCrossbow2.SetActive(false);
            photonView.RPC("SyncBuyCrossbow2", RpcTarget.AllBuffered);
            RandomItem2();
        }

    }
    public void BuyDetector2()
    {
        if (player.gold >= detectorPrice)
        {
            PhotonNetwork.Instantiate(player.pickup6.name, new Vector2(player.paintPos.transform.position.x, player.paintPos.transform.position.y), Quaternion.identity, 0);
            player.gold -= detectorPrice;
            buyDetector2.SetActive(false);
            photonView.RPC("SyncBuyDetector2", RpcTarget.AllBuffered);
            RandomItem2();
        }

    }
    public void BuySilentPotion2()
    {
        if (player.gold >= silentPotionPrice)
        {
            PhotonNetwork.Instantiate(player.pickup7.name, new Vector2(player.paintPos.transform.position.x, player.paintPos.transform.position.y), Quaternion.identity, 0);
            player.gold -= silentPotionPrice;
            buySilentPotion2.SetActive(false);
            photonView.RPC("SyncBuySilentPotion2", RpcTarget.AllBuffered);
            RandomItem2();
        }

    }
    public void BuyGelTrap2()
    {
        if (player.gold >= gelTrapPrice)
        {
            PhotonNetwork.Instantiate(player.pickup8.name, new Vector2(player.paintPos.transform.position.x, player.paintPos.transform.position.y), Quaternion.identity, 0);
            player.gold -= gelTrapPrice;
            buyGelTrap2.SetActive(false);
            photonView.RPC("SyncBuyGelTrap2", RpcTarget.AllBuffered);
            RandomItem2();
        }

    }
    public void BuySpikePit2()
    {
        if (player.gold >= spikePitPrice)
        {
            PhotonNetwork.Instantiate(player.pickup9.name, new Vector2(player.paintPos.transform.position.x, player.paintPos.transform.position.y), Quaternion.identity, 0);
            player.gold -= spikePitPrice;
            buySpikePit2.SetActive(false);
            photonView.RPC("SyncBuySpikePit2", RpcTarget.AllBuffered);
            RandomItem2();
        }

    }
    public void BuyTripWire2()
    {
        if (player.gold >= tripWireTrapPrice)
        {
            PhotonNetwork.Instantiate(player.pickup10.name, new Vector2(player.paintPos.transform.position.x, player.paintPos.transform.position.y), Quaternion.identity, 0);
            player.gold -= tripWireTrapPrice;
            buyTripWire2.SetActive(false);
            photonView.RPC("SyncBuyTripWire2", RpcTarget.AllBuffered);
            RandomItem2();
        }

    }
    public void BuySeePotion2()
    {
        if (player.gold >= seePotionPrice)
        {
            PhotonNetwork.Instantiate(player.pickup11.name, new Vector2(player.paintPos.transform.position.x, player.paintPos.transform.position.y), Quaternion.identity, 0);
            player.gold -= seePotionPrice;
            buySeePotion2.SetActive(false);
            photonView.RPC("SyncBuySeePotion2", RpcTarget.AllBuffered);
            RandomItem2();
        }

    }
    public void BuySpeedPotion2()
    {
        if (player.gold >= speedPotionPrice)
        {
            PhotonNetwork.Instantiate(player.pickup12.name, new Vector2(player.paintPos.transform.position.x, player.paintPos.transform.position.y), Quaternion.identity, 0);
            player.gold -= speedPotionPrice;
            buySpeedPotion2.SetActive(false);
            photonView.RPC("SyncBuySpeedPotion2", RpcTarget.AllBuffered);
            RandomItem2();
        }

    }


}
