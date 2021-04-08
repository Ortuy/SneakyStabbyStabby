using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Chest : MonoBehaviourPunCallbacks
{
    public Player player;
    public StabHitBox stabHitBox;
    public float itemNum = 0;
    public bool chestlIsActive = true;
    public float spawnTime;
    public GameObject dropPos;
    //public GameObject dropPos1;
    public GameObject pickup1;
    public GameObject pickup2;
    public GameObject pickup3;
    public GameObject pickup4;
    public GameObject pickup5;
    public GameObject pickup6;
    public GameObject pickup7;
    public GameObject pickup8;
    public GameObject pickup9;
    public GameObject pickup10;



    //private void OnTriggerStay2D(Collider2D Player)
    //{

    //    if (Input.GetButtonDown("Fire1") && stabHitBox.canOpenChest == true && chestlIsActive == true)
    //    {

    //        itemNum = Random.Range(1, 10);
    //        if (itemNum >= 1 && itemNum <= 10)
    //        {
    //            CreateItem();
    //            StartCoroutine("ItemMaking");
    //            chestlIsActive = false;
    //        }


    //    }


    //}
    [PunRPC]
    private void RandomItem()
    {
        itemNum = Random.Range(1, 10);
        CreateItem();
        StartCoroutine("ItemMaking");
        chestlIsActive = false;
    }
        
    private void CreateItem()
    {


            if (itemNum == 1)
            {
                PhotonNetwork.Instantiate(pickup1.name, new Vector2(dropPos.transform.position.x, dropPos.transform.position.y), Quaternion.identity, 0);
            }
            if (itemNum == 2)
            {
                PhotonNetwork.Instantiate(pickup2.name, new Vector2(dropPos.transform.position.x, dropPos.transform.position.y), Quaternion.identity, 0);
            }
            if (itemNum == 3)
            {
                PhotonNetwork.Instantiate(pickup3.name, new Vector2(dropPos.transform.position.x, dropPos.transform.position.y), Quaternion.identity, 0);
            }
            if (itemNum == 4)
            {
                PhotonNetwork.Instantiate(pickup4.name, new Vector2(dropPos.transform.position.x, dropPos.transform.position.y), Quaternion.identity, 0);
            }
            if (itemNum == 5)
            {
                PhotonNetwork.Instantiate(pickup5.name, new Vector2(dropPos.transform.position.x, dropPos.transform.position.y), Quaternion.identity, 0);
            }
            if (itemNum == 6)
            {
                PhotonNetwork.Instantiate(pickup6.name, new Vector2(dropPos.transform.position.x, dropPos.transform.position.y), Quaternion.identity, 0);
            }
            if (itemNum == 7)
            {
                PhotonNetwork.Instantiate(pickup7.name, new Vector2(dropPos.transform.position.x, dropPos.transform.position.y), Quaternion.identity, 0);
            }
            if (itemNum == 8)
            {
                PhotonNetwork.Instantiate(pickup8.name, new Vector2(dropPos.transform.position.x, dropPos.transform.position.y), Quaternion.identity, 0);
            }
            if (itemNum == 9)
            {
                PhotonNetwork.Instantiate(pickup9.name, new Vector2(dropPos.transform.position.x, dropPos.transform.position.y), Quaternion.identity, 0);
            }
            if (itemNum == 10)
            {
                PhotonNetwork.Instantiate(pickup10.name, new Vector2(dropPos.transform.position.x, dropPos.transform.position.y), Quaternion.identity, 0);
            }


    }
    IEnumerator ItemMaking()
    {
        yield return new WaitForSeconds(spawnTime);
        chestlIsActive = true;
    }

}
