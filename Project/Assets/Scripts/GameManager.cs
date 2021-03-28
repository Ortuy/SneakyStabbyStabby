using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class GameManager : MonoBehaviourPunCallbacks
{
    public static GameManager localInstance;
    public GameObject playerPrefab;
    public GameObject gameCanvas;
    public GameObject sceneCamera;
    [HideInInspector]public GameObject localPlayer;
    public GameObject respawnMenu;
    private float timerAmount = 5f;
    private bool runSpawnTimer = false;

    public Text victoryText;
    public Text secondaryText;

    public Text stabCooldownText;

    public Text pingText;

    public int playerAmount;

    public Color[] playerColors;

    public GameObject[] spawnPoints;
    private bool countdownStarted;

    public GameObject decorHolder;

    private IEnumerator currentBigTextCoroutine;

    public void Awake()
    {
        localInstance = this;
        gameCanvas.SetActive(true);
    }

    private void Update()
    {
        //pingText.text = "Ping: " + PhotonNetwork.GetPing();

        pingText.text = "Players: " + playerAmount;

        if (runSpawnTimer)
        {
            StartRespawn();
        }

        if(!countdownStarted && playerAmount > 1)
        {
            StartCoroutine(StartCountdown());
        }

        if(!countdownStarted && Input.GetKeyDown(KeyCode.O))
        {
            StartCoroutine(StartCountdown());
        }
    }
    public void EnableRespawn()
    {
        timerAmount = 5f;
        runSpawnTimer = true;
        //respawnMenu.SetActive(true);
    }
    private void StartRespawn()
    {
        timerAmount -= Time.deltaTime;
        if(timerAmount <= 0)
        {
            localPlayer.GetComponent<PhotonView>().RPC("Respawn", RpcTarget.AllBuffered);
            localPlayer.GetComponent<Health>().EnableInput();
            respawnMenu.SetActive(false);
            runSpawnTimer = false;
        }
    }

    public void SpawnPlayer()
    {
        float randomValueX = Random.Range(-2f, 2f);
        float randomValueY = Random.Range(-2f, 2f);
        
        PhotonNetwork.Instantiate(playerPrefab.name, spawnPoints[playerAmount].transform.position, Quaternion.identity, 0);
        
        gameCanvas.SetActive(false);
        StartCoroutine(SetPlayerColor());
        sceneCamera.SetActive(false);
    }

    [PunRPC]
    public void SpawnDecor()
    {
        PhotonNetwork.Instantiate("DecorHolder", Vector3.zero, Quaternion.identity);
        if (photonView.IsMine)
        {
            //PhotonNetwork.Instantiate("DecorHolder", Vector3.zero, Quaternion.identity);
            //Instantiate(decorHolder, Vector3.zero, Quaternion.identity);
        }
    }

    [PunRPC]
    public void DissolveStartPoints()
    {
        spawnPoints[0].gameObject.SetActive(false);
        spawnPoints[1].gameObject.SetActive(false);
    }

    public void ReloadScene()
    {
        PhotonNetwork.LoadLevel("Arena");
    }

    public void DisappearText(float time)
    {
        if(currentBigTextCoroutine != null)
        {
            StopCoroutine(currentBigTextCoroutine);
        }

        currentBigTextCoroutine = StopText(time);

        StartCoroutine(currentBigTextCoroutine);
    }

    IEnumerator StopText(float time)
    {
        yield return new WaitForSeconds(time);
        victoryText.gameObject.SetActive(false);
        secondaryText.gameObject.SetActive(false);
    }

    IEnumerator StartCountdown()
    {
        //photonView.RPC("KeepOneDecorSet", RpcTarget.AllBuffered);

        if (PhotonNetwork.IsMasterClient)
        {
            //photonView.RPC("SpawnDecor", RpcTarget.AllBuffered);
            PhotonNetwork.InstantiateRoomObject("DecorHolder", Vector3.zero, Quaternion.identity);
            //SpawnDecor();
        }

        countdownStarted = true;
        victoryText.gameObject.SetActive(true);
        victoryText.text = "3";
        yield return new WaitForSeconds(1f);
        victoryText.text = "2";
        yield return new WaitForSeconds(1f);
        victoryText.text = "1";
        yield return new WaitForSeconds(1f);
        victoryText.gameObject.SetActive(false);
        photonView.RPC("DissolveStartPoints", RpcTarget.AllBuffered);
    }

    [PunRPC]
    public void KeepOneDecorSet()
    {
        Debug.LogWarning("FUCKING WORK");
        var decors = FindObjectsOfType<DecorHolder>();

        if(decors.Length > 1)
        {
            //decors[1].gameObject.SetActive(false);
        }
    }

    IEnumerator SetPlayerColor()
    {
        yield return null;
        var c = playerColors[Random.Range(0, playerColors.Length)];
        localPlayer.GetComponent<PhotonView>().RPC("SetColor", RpcTarget.AllBuffered, c.r, c.g, c.b);
    }
}
