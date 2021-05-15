using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class Chest : MonoBehaviourPunCallbacks
{
    public Player player;
    public StabHitBox stabHitBox;
    public int itemNum = 0;
    public bool chestlIsActive = true;
    public int spawnTime;
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
    public GameObject pickup11;
    public GameObject pickup12;

    [SerializeField] private SpriteRenderer itemImage;
    [SerializeField] private Sprite[] itemSprites;

    [SerializeField] private Text cooldownText;
    [SerializeField] private GameObject lid;

    [SerializeField] private ParticleSystem chestFX;

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

    private void Start()
    {
        chestlIsActive = false;
    }

    public void InitChest()
    {
        chestlIsActive = true;
        if (PhotonNetwork.IsMasterClient)
        {
            var temp = Random.Range(0, 12);
            photonView.RPC("SyncItemNumber", RpcTarget.AllBuffered, temp);
        }
        itemImage.sprite = itemSprites[itemNum];
    }

    public void RandomItem()
    {
        if (chestlIsActive)
        {
            Debug.LogWarning(photonView.IsMine);
            CreateItem();
            chestFX.Play();

            photonView.RPC("StartItemCoolDown", RpcTarget.AllBuffered);
            var temp = Random.Range(0, 12);
            photonView.RPC("SyncItemNumber", RpcTarget.AllBuffered, temp);
        }
    }

    [PunRPC]
    private void StartItemCoolDown()
    {
        StartCoroutine("ItemMaking");
        chestlIsActive = false;
        lid.gameObject.SetActive(false);
        itemImage.gameObject.SetActive(false);

    }
    [PunRPC]
    private void SyncItemNumber(int number)
    {
        itemNum = number;
    }

    private void CreateItem()
    {

        AkSoundEngine.PostEvent("sfx_box_destroy", gameObject, gameObject);
        if (itemNum == 0)
        {
            PhotonNetwork.Instantiate(pickup1.name, new Vector2(dropPos.transform.position.x, dropPos.transform.position.y), Quaternion.identity, 0);
        }
        if (itemNum == 1)
        {
            PhotonNetwork.Instantiate(pickup2.name, new Vector2(dropPos.transform.position.x, dropPos.transform.position.y), Quaternion.identity, 0);
        }
        if (itemNum == 2)
        {
            PhotonNetwork.Instantiate(pickup3.name, new Vector2(dropPos.transform.position.x, dropPos.transform.position.y), Quaternion.identity, 0);
        }
        if (itemNum == 3)
        {
            PhotonNetwork.Instantiate(pickup4.name, new Vector2(dropPos.transform.position.x, dropPos.transform.position.y), Quaternion.identity, 0);
        }
        if (itemNum == 4)
        {
            PhotonNetwork.Instantiate(pickup5.name, new Vector2(dropPos.transform.position.x, dropPos.transform.position.y), Quaternion.identity, 0);
        }
        if (itemNum == 5)
        {
            PhotonNetwork.Instantiate(pickup6.name, new Vector2(dropPos.transform.position.x, dropPos.transform.position.y), Quaternion.identity, 0);
        }
        if (itemNum == 6)
        {
            PhotonNetwork.Instantiate(pickup7.name, new Vector2(dropPos.transform.position.x, dropPos.transform.position.y), Quaternion.identity, 0);
        }
        if (itemNum == 7)
        {
            PhotonNetwork.Instantiate(pickup8.name, new Vector2(dropPos.transform.position.x, dropPos.transform.position.y), Quaternion.identity, 0);
        }
        if (itemNum == 8)
        {
            PhotonNetwork.Instantiate(pickup9.name, new Vector2(dropPos.transform.position.x, dropPos.transform.position.y), Quaternion.identity, 0);
        }
        if (itemNum == 9)
        {
            PhotonNetwork.Instantiate(pickup10.name, new Vector2(dropPos.transform.position.x, dropPos.transform.position.y), Quaternion.identity, 0);
        }
        if (itemNum == 10)
        {
            PhotonNetwork.Instantiate(pickup11.name, new Vector2(dropPos.transform.position.x, dropPos.transform.position.y), Quaternion.identity, 0);
        }
        if (itemNum == 11)
        {
            PhotonNetwork.Instantiate(pickup12.name, new Vector2(dropPos.transform.position.x, dropPos.transform.position.y), Quaternion.identity, 0);
        }


    }
    IEnumerator ItemMaking()
    {
        cooldownText.gameObject.SetActive(true);
        int timeLeft = spawnTime;
        while (timeLeft > 0)
        {
            cooldownText.text = timeLeft.ToString();
            yield return new WaitForSeconds(1);
            timeLeft--;
        }

        itemImage.gameObject.SetActive(true);
        cooldownText.gameObject.SetActive(false);
        lid.SetActive(true);
        itemImage.sprite = itemSprites[itemNum];
        chestlIsActive = true;
    }

}
