﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourPunCallbacks
{
    public static GameManager localInstance;
    public GameObject playerPrefab;
    public GameObject gameCanvas;
    public GameObject sceneCamera;
    [HideInInspector] public GameObject localPlayer;
    public GameObject respawnMenu, pauseMenu, optionsMenu;
    public GameObject waitRoomPortal;
    public GameObject waitRoomPortal1;
    private float timerAmount = 5f;
    public int numerOfPlayers = 0;
    public int numerOfPlayers1 = 1;
    private bool runSpawnTimer = false;
    public bool readyToStart = false;
    public bool readyToStart1 = false;
    public bool map1 = false;
    public bool map2 = false;
    public GameObject mapPanel;
    public GameObject mapCamera;

    public Text victoryText;
    public Text secondaryText;

    public Text stabCooldownText;

    public Text pingText;

    public int playerAmount;

    public Color[] playerColors;

    public GameObject[] spawnPoints;
    public GameObject[] spawnPointsWait;
    [SerializeField] private Animator[] spawnPortalAnimators;
    private bool countdownStarted;

    public GameObject decorHolder;

    private IEnumerator currentBigTextCoroutine;

    public Tilemap stoneMask;

    private bool onoff = false;

    public bool mapOut;

    public void Awake()
    {
        localInstance = this;
        //gameCanvas.SetActive(true);
        StartCoroutine(WaitAndSpawnPlayer());
    }

    IEnumerator WaitAndSpawnPlayer()
    {
        yield return null;
        SpawnPlayer();
    }

    private void Update()
    {
        //pingText.text = "Ping: " + PhotonNetwork.GetPing();

        pingText.text = PhotonNetwork.CurrentRoom.Name;

        if (runSpawnTimer)
        {
            StartRespawn();
        }

        if (!countdownStarted && /*playerAmount > 1*/ readyToStart == true && readyToStart1 == true)
        {
            StartCoroutine(StartCountdown());
        }

        if (!countdownStarted && Input.GetKeyDown(KeyCode.O))
        {
            StartCoroutine(StartCountdown());
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            onoff = !onoff;

            if (onoff)
            {
                AkSoundEngine.PostEvent("ui_wood_open_panel", gameObject, gameObject);
            }
            else
            {
                //sound for closing
            }


            TogglePauseMenu();
        }

    }

    public void TogglePauseMenu()
    {
        Player player = localPlayer.GetComponent<Player>();

        if (pauseMenu.activeInHierarchy)
        {
            player.stabLock = false;
            //pauseMenu.SetActive(false);
            pauseMenu.GetComponent<UIAnimator>().Hide();
        }
        else if(!player.isInteracting)
        {
            AkSoundEngine.PostEvent("UIClickWoodPanel", gameObject, gameObject);
            player.stabLock = true;
            pauseMenu.SetActive(true);
            pauseMenu.GetComponent<UIAnimator>().Show();
        }
    }

    public void QuitToMenu()
    {
        AkSoundEngine.PostEvent("UIClickWoodPanel", gameObject, gameObject);
        TerminateLocalPlayer();
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene(0);
    }

    public void QuitToDesktop()
    {
        AkSoundEngine.PostEvent("ui_click_wood_panel_exit", gameObject, gameObject);
        TerminateLocalPlayer();
        PhotonNetwork.LeaveRoom();
        Application.Quit();
    }

    public void ToggleOptions()
    {
        AkSoundEngine.PostEvent("UIClickWoodPanel", gameObject, gameObject);
        TogglePauseMenu();
        if(optionsMenu.activeInHierarchy)
        {
            //optionsMenu.SetActive(false);
            optionsMenu.GetComponent<UIAnimator>().Hide();
        }
        else
        {
            optionsMenu.SetActive(true);
            optionsMenu.GetComponent<UIAnimator>().Show();
        }
    }

    private void TerminateLocalPlayer()
    {
        localPlayer.GetComponent<PhotonView>().RPC("ReduceHealth", RpcTarget.AllBuffered, 10f);
    }

    public void EnableRespawn()
    {
        timerAmount = 5f;
        runSpawnTimer = true;
        //respawnMenu.SetActive(true);
    }

    public void StartRespawn()
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
        EffectsManager.instance.Unfade();

        float randomValueX = Random.Range(-2f, 2f);
        float randomValueY = Random.Range(-2f, 2f);

        spawnPortalAnimators[playerAmount].SetBool("Open", true);
        PhotonNetwork.Instantiate(playerPrefab.name, spawnPointsWait[playerAmount].transform.position, Quaternion.identity, 0);


        gameCanvas.SetActive(false);
        StartCoroutine(SetPlayerColor());
        sceneCamera.SetActive(false);
    }
    //public void SpawnPlayerWaitRoom()
    //{
    //    float randomValueX = Random.Range(-2f, 2f);
    //    float randomValueY = Random.Range(-2f, 2f);


    //    PhotonNetwork.Instantiate(playerPrefab.name, spawnPointsWait[playerAmount].transform.position, Quaternion.identity, 0);


    //    gameCanvas.SetActive(false);
    //    StartCoroutine(SetPlayerColor());
    //    sceneCamera.SetActive(false);
    //}

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
            //string tmp = "DecorHolder" + SceneManager.GetActiveScene().name;
            //PhotonNetwork.InstantiateRoomObject(tmp, Vector3.zero, Quaternion.identity);
            
            photonView.RPC("KeepOneDecorSet", RpcTarget.AllBuffered);
            //SpawnDecor();
        }

        var chests = FindObjectsOfType<Chest>();
        foreach (Chest chest in chests)
        {
            chest.InitChest();
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

        if(PhotonNetwork.IsMasterClient)
        {
            FindObjectOfType<Portal>().portalIsActive = true;
            var gChests = FindObjectsOfType<GoldChest>();
            foreach (GoldChest goldChest in gChests)
            {
                Debug.Log("gchest found");
                goldChest.chestIsActive = true;
            }
        }

        photonView.RPC("DissolveStartPoints", RpcTarget.AllBuffered);

    }

    [PunRPC]
    public void KeepOneDecorSet()
    {
        Debug.LogWarning("FUCKING WORK");
        var decors = FindObjectsOfType<DecorHolder>();

        for (int i = 0; i < decors.Length; i++)
        {
            Destroy(decors[i].gameObject, 2f);
        }

    }

    IEnumerator SetPlayerColor()
    {
        yield return null;
        var c = playerColors[Random.Range(0, playerColors.Length)];
        localPlayer.GetComponent<PhotonView>().RPC("SetColor", RpcTarget.AllBuffered, c.r, c.g, c.b);
        spawnPortalAnimators[playerAmount - 1].SetBool("Open", false);
    }
    public void ToggleMap()
    {
        if (mapPanel.activeInHierarchy)
        {
            //mapPanel.SetActive(false);
            mapPanel.GetComponent<UIAnimator>().Hide();
            mapOut = false;
            mapCamera.SetActive(false);
        }
        else
        {
            mapPanel.SetActive(true);
            mapPanel.GetComponent<UIAnimator>().Show();
            mapCamera.SetActive(true);
            mapOut = true;
        }
    }
    public void MapWin()
    {
        if(map1 = true)
        {
            if (numerOfPlayers == 1)
            {
                EnableRespawn();
            }
        }

        if (map2 = true)
        {
            if (numerOfPlayers == 5)
            {
                EnableRespawn();
            }
        }
            
    }
}
