﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GoldChest : MonoBehaviourPunCallbacks
{
    public bool chestIsActive = false;
    public float spawnTime = 1;
    public int itemNum = 0;
    public int dropPosNum = 5;
    public GameObject dropPos;
    public GameObject dropPos1;
    public GameObject dropPos2;
    public GameObject dropPos3;
    public GameObject dropPos4;

    [SerializeField] private GameObject[] goldPickups;

    private GameManager[] managers;

    void Update()
    {
        if (chestIsActive == true && PhotonNetwork.IsMasterClient)
        {
            SpawnGold();
            Debug.LogWarning("ChestStartGold");
            StartCoroutine(WaitAndSpawnGold());
            itemNum = Random.Range(0, 3);
            //dropPosNum = Random.Range(1, 6);
            chestIsActive = false;
        }
    }
    IEnumerator WaitAndSpawnGold()
    {
        yield return new WaitForSeconds(spawnTime);

        if (PhotonNetwork.IsMasterClient)
        {
            Debug.LogWarning("Gold");
            SpawnGold();


            //yield return null;
            chestIsActive = true;

        }
    }

    private void SpawnGold()
    {
        if (dropPosNum == 5)
        {
            PhotonNetwork.Instantiate(goldPickups[itemNum].name, new Vector2(dropPos4.transform.position.x, dropPos4.transform.position.y), Quaternion.AngleAxis(Random.Range(0, 360), Vector3.forward), 0);
            dropPosNum--;
        }
        if (dropPosNum == 4)
        {
            PhotonNetwork.Instantiate(goldPickups[itemNum].name, new Vector2(dropPos3.transform.position.x, dropPos3.transform.position.y), Quaternion.AngleAxis(Random.Range(0, 360), Vector3.forward), 0);
            dropPosNum--;
        }
        if (dropPosNum == 3)
        {
            PhotonNetwork.Instantiate(goldPickups[itemNum].name, new Vector2(dropPos2.transform.position.x, dropPos2.transform.position.y), Quaternion.AngleAxis(Random.Range(0, 360), Vector3.forward), 0);
            dropPosNum--;
        }
        if (dropPosNum == 2)
        {
            PhotonNetwork.Instantiate(goldPickups[itemNum].name, new Vector2(dropPos1.transform.position.x, dropPos1.transform.position.y), Quaternion.AngleAxis(Random.Range(0, 360), Vector3.forward), 0);
            dropPosNum--;
        }
        if (dropPosNum == 1)
        {
            PhotonNetwork.Instantiate(goldPickups[itemNum].name, new Vector2(dropPos.transform.position.x, dropPos.transform.position.y), Quaternion.AngleAxis(Random.Range(0, 360), Vector3.forward), 0);
            dropPosNum = 5;
        }
    }
}
