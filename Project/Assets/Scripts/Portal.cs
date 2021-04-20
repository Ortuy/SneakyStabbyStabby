﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Portal : MonoBehaviourPunCallbacks
{
    public bool portalIsActive = false;
    public float spawnTime = 4;
    public int itemNum = 0;
    public int itemNum1 = 0;
    public int itemNum2 = 0;
    public GameObject dropPos;
    public GameObject dropPos1;
    public GameObject dropPos2;
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
    public GameObject pickup11;
    public GameObject pickup12;

    private Animator animator;

    [SerializeField] private GameObject[] pickups;

    //private void Awake()
    //{
    //    //portalIsActive = true;
    //}
    //private void nTriggerStay2D(Collider2D collision)
    //{

    //}

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.P))
        //{
        //    portalIsActive = true;
        //}
        if (portalIsActive == true && PhotonNetwork.IsMasterClient)
        {
            //StartCoroutine("ItemMaking");
            //StartCoroutine("ItemMaking1");
            //StartCoroutine("ItemMaking2");
            StartCoroutine(WaitAndSpawnItems());
            itemNum = Random.Range(0, 12);
            itemNum1 = Random.Range(0, 12);
            itemNum2 = Random.Range(0, 12);
            portalIsActive = false;
        }
        
    }

    IEnumerator WaitAndSpawnItems()
    {
        yield return new WaitForSeconds(spawnTime);

        animator.SetBool("Open", true);

        GetComponent<PhotonView>().RPC("ShowSupplyText", RpcTarget.AllBuffered);

        PhotonNetwork.InstantiateRoomObject(pickups[itemNum].name, new Vector2(dropPos.transform.position.x, dropPos.transform.position.y), Quaternion.identity, 0);
        PhotonNetwork.InstantiateRoomObject(pickups[itemNum1].name, new Vector2(dropPos1.transform.position.x, dropPos1.transform.position.y), Quaternion.identity, 0);
        PhotonNetwork.InstantiateRoomObject(pickups[itemNum2].name, new Vector2(dropPos2.transform.position.x, dropPos2.transform.position.y), Quaternion.identity, 0);

        portalIsActive = true;

        yield return null;

        animator.SetBool("Open", false);

        
    }

    [PunRPC]
    private void ShowSupplyText()
    {
        GameManager.localInstance.victoryText.gameObject.SetActive(true);
        GameManager.localInstance.victoryText.text = "Supplies Arrived!";
        GameManager.localInstance.DisappearText(2.4f);
    }

    IEnumerator ItemMaking()
    {
        yield return new WaitForSeconds(spawnTime);
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

        portalIsActive = true;
    }
    IEnumerator ItemMaking1()
    {
        yield return new WaitForSeconds(spawnTime);
        if (itemNum == 1)
        {
            PhotonNetwork.Instantiate(pickup1.name, new Vector2(dropPos1.transform.position.x, dropPos1.transform.position.y), Quaternion.identity, 0);
        }
        if (itemNum == 2)
        {
            PhotonNetwork.Instantiate(pickup2.name, new Vector2(dropPos1.transform.position.x, dropPos1.transform.position.y), Quaternion.identity, 0);
        }
        if (itemNum == 3)
        {
            PhotonNetwork.Instantiate(pickup3.name, new Vector2(dropPos1.transform.position.x, dropPos1.transform.position.y), Quaternion.identity, 0);
        }
        if (itemNum == 4)
        {
            PhotonNetwork.Instantiate(pickup4.name, new Vector2(dropPos1.transform.position.x, dropPos1.transform.position.y), Quaternion.identity, 0);
        }
        if (itemNum == 5)
        {
            PhotonNetwork.Instantiate(pickup5.name, new Vector2(dropPos1.transform.position.x, dropPos1.transform.position.y), Quaternion.identity, 0);
        }
        if (itemNum == 6)
        {
            PhotonNetwork.Instantiate(pickup6.name, new Vector2(dropPos1.transform.position.x, dropPos1.transform.position.y), Quaternion.identity, 0);
        }
        if (itemNum == 7)
        {
            PhotonNetwork.Instantiate(pickup7.name, new Vector2(dropPos1.transform.position.x, dropPos1.transform.position.y), Quaternion.identity, 0);
        }
        if (itemNum == 8)
        {
            PhotonNetwork.Instantiate(pickup8.name, new Vector2(dropPos1.transform.position.x, dropPos1.transform.position.y), Quaternion.identity, 0);
        }
        if (itemNum == 9)
        {
            PhotonNetwork.Instantiate(pickup9.name, new Vector2(dropPos1.transform.position.x, dropPos1.transform.position.y), Quaternion.identity, 0);
        }
        if (itemNum == 10)
        {
            PhotonNetwork.Instantiate(pickup10.name, new Vector2(dropPos1.transform.position.x, dropPos1.transform.position.y), Quaternion.identity, 0);
        }

        portalIsActive = true;
    }IEnumerator ItemMaking2()
    {
        yield return new WaitForSeconds(spawnTime);
        if (itemNum == 1)
        {
            PhotonNetwork.Instantiate(pickup1.name, new Vector2(dropPos2.transform.position.x, dropPos2.transform.position.y), Quaternion.identity, 0);
        }
        if (itemNum == 2)
        {
            PhotonNetwork.Instantiate(pickup2.name, new Vector2(dropPos2.transform.position.x, dropPos2.transform.position.y), Quaternion.identity, 0);
        }
        if (itemNum == 3)
        {
            PhotonNetwork.Instantiate(pickup3.name, new Vector2(dropPos2.transform.position.x, dropPos2.transform.position.y), Quaternion.identity, 0);
        }
        if (itemNum == 4)
        {
            PhotonNetwork.Instantiate(pickup4.name, new Vector2(dropPos2.transform.position.x, dropPos2.transform.position.y), Quaternion.identity, 0);
        }
        if (itemNum == 5)
        {
            PhotonNetwork.Instantiate(pickup5.name, new Vector2(dropPos2.transform.position.x, dropPos2.transform.position.y), Quaternion.identity, 0);
        }
        if (itemNum == 6)
        {
            PhotonNetwork.Instantiate(pickup6.name, new Vector2(dropPos2.transform.position.x, dropPos2.transform.position.y), Quaternion.identity, 0);
        }
        if (itemNum == 7)
        {
            PhotonNetwork.Instantiate(pickup7.name, new Vector2(dropPos2.transform.position.x, dropPos2.transform.position.y), Quaternion.identity, 0);
        }
        if (itemNum == 8)
        {
            PhotonNetwork.Instantiate(pickup8.name, new Vector2(dropPos2.transform.position.x, dropPos2.transform.position.y), Quaternion.identity, 0);
        }
        if (itemNum == 9)
        {
            PhotonNetwork.Instantiate(pickup9.name, new Vector2(dropPos2.transform.position.x, dropPos2.transform.position.y), Quaternion.identity, 0);
        }
        if (itemNum == 10)
        {
            PhotonNetwork.Instantiate(pickup10.name, new Vector2(dropPos2.transform.position.x, dropPos2.transform.position.y), Quaternion.identity, 0);
        }

        portalIsActive = true;
    }
    
}
