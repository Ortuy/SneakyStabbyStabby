using System.Collections;
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
    public GameObject respawnMenu, pauseMenu, optionsMenu, tipsMenu;
    public GameObject waitRoomPortal;
    public GameObject waitRoomPortal1;
    public GameObject waitRoom1Portal;
    public GameObject waitRoom1Portal1;
    public GameObject waitRoom1Portal2;
    public GameObject waitRoom1Portal3;
    public GameObject waitRoom1Portal4;
    public GameObject waitRoom1Portal5;
    private float timerAmount = 5f;
    public int numerOfPlayers = 0;
    private bool runSpawnTimer = false;
    public bool readyToStart = false;
    public bool readyToStart1 = false;
    public bool readyToStart3 = false;
    public bool readyToStart4 = false;
    public bool readyToStart5 = false;
    public bool readyToStart6 = false;
    public bool readyToStart7= false;
    public bool readyToStart8 = false;
    public bool map1 = false;
    public bool map2 = false;
    public GameObject mapPanel;
    public GameObject mapCamera;
    public PlayerStatusDisplay[] statusPanels;

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

    public Tilemap stoneMask, woodMask;

    private bool onoff = false;

    public bool mapOut;
    private bool gameStarted, mapInitialised;

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
        if (!countdownStarted && /*playerAmount > 1*/ readyToStart3 == true && readyToStart4 == true && readyToStart5 == true && readyToStart6 == true && readyToStart7 == true && readyToStart8 == true)
        {
            StartCoroutine(StartCountdown());
        }

        //if (!countdownStarted && Input.GetKeyDown(KeyCode.O))
        //{
        //    StartCoroutine(StartCountdown());
        //}

        if(Input.GetKeyDown(KeyCode.Escape) && !localPlayer.GetComponent<Player>().isInteracting)
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
            if (tipsMenu.activeInHierarchy)
            {
                ToggleTips();
            }
            if (!optionsMenu.activeInHierarchy && !mapOut)
            {
                TogglePauseMenu();
            }
            else if(!mapOut)
            {
                ToggleOptions();
            }
            else
            {
                ToggleMap();
            }
        }

    }

    public void TogglePauseMenu()
    {
        Player player = localPlayer.GetComponent<Player>();

        if (pauseMenu.activeInHierarchy)
        {
            if (player.inWaitRoom==false)
            {
                player.stabLock = false;
            }

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
    public void ToggleTips()
    {
        AkSoundEngine.PostEvent("UIClickWoodPanel", gameObject, gameObject);
        TogglePauseMenu();
        if (tipsMenu.activeInHierarchy)
        {
            //optionsMenu.SetActive(false);
            tipsMenu.GetComponent<UIAnimator>().Hide();
        }
        else
        {
            tipsMenu.SetActive(true);
            tipsMenu.GetComponent<UIAnimator>().Show();
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

        //spawnPortalAnimators[playerAmount].SetBool("Open", true);
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
        //spawnPoints[0].gameObject.SetActive(false);
        //spawnPoints[1].gameObject.SetActive(false);
        foreach(GameObject spawnPoint in spawnPoints)
        {
            spawnPoint.SetActive(false);
        }
    }

    public void ReloadScene()
    {
        if (map1)
        {
            PhotonNetwork.LoadLevel("Arena");
        }
        if (map2)
        {
            PhotonNetwork.LoadLevel("ArenaLarge");
        }



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
            string tmp = "DecorHolder" + SceneManager.GetActiveScene().name;
            PhotonNetwork.InstantiateRoomObject(tmp, Vector3.zero, Quaternion.identity);
            
            photonView.RPC("KeepOneDecorSet", RpcTarget.AllBuffered);
            //SpawnDecor();
        }

        var chests = FindObjectsOfType<Chest>();
        foreach (Chest chest in chests)
        {
            chest.InitChest();
        }

        pingText.gameObject.SetActive(false);
        gameStarted = true;

        countdownStarted = true;
        victoryText.gameObject.SetActive(true);
        victoryText.text = "3";
        AkSoundEngine.PostEvent("ui_countdown", gameObject, gameObject);
        yield return new WaitForSeconds(1f);
        victoryText.text = "2";
        AkSoundEngine.PostEvent("ui_countdown", gameObject, gameObject);
        yield return new WaitForSeconds(1f);
        victoryText.text = "1";
        AkSoundEngine.PostEvent("ui_countdown", gameObject, gameObject);
        yield return new WaitForSeconds(1f);
        victoryText.gameObject.SetActive(false);

        AkSoundEngine.PostEvent("ui_start_game", gameObject, gameObject);
        AkSoundEngine.PostEvent("ui_walls_down", gameObject, gameObject);

        if (PhotonNetwork.IsMasterClient)
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
        //var c = playerColors[Random.Range(0, playerColors.Length)];
        //localPlayer.GetComponent<PhotonView>().RPC("SetColor", RpcTarget.AllBuffered, c.r, c.g, c.b);
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

            if (gameStarted)
            {
                if (!mapInitialised)
                {
                    var players = FindObjectsOfType<Health>();
                    for (int i = 0; i < players.Length; i++)
                    {
                        statusPanels[i].gameObject.SetActive(true);
                        statusPanels[i].InitDisplay(players[i]);
                    }
                    mapInitialised = true;
                }

                for (int i = 0; i < statusPanels.Length; i++)
                {
                    if (statusPanels[i].gameObject.activeInHierarchy)
                    {
                        statusPanels[i].StartTracking();
                    }
                }
            }  
        }
    }
    public void MapWin()
    {
        if(map1 == true)
        {
            if (numerOfPlayers == 1)
            {
                EnableRespawn();
            }
        }

        if (map2 == true)
        {
            if (numerOfPlayers == 5)
            {
                EnableRespawn();
            }
        }
            
    }
}
