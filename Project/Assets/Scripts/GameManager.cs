using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject playerPrefab;
    public GameObject gameCanvas;
    public GameObject sceneCamera;
    [HideInInspector]public GameObject localPlayer;
    public GameObject respawnMenu;
    private float timerAmount = 5f;
    private bool runSpawnTimer = false;

    public Text victoryText;

    public Text stabCooldownText;

    public Text pingText;

    public int playerAmount;

    public void Awake()
    {
        instance = this;
        gameCanvas.SetActive(true);
    }

    private void Update()
    {
        pingText.text = "Ping: " + PhotonNetwork.GetPing();
        if (runSpawnTimer)
        {
            StartRespawn();
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
            localPlayer.GetComponent<PhotonView>().RPC("Respawn", PhotonTargets.AllBuffered);
            localPlayer.GetComponent<Health>().EnableInput();
            respawnMenu.SetActive(false);
            runSpawnTimer = false;
        }
    }

    public void SpawnPlayer()
    {
        float randomValueX = Random.Range(-2f, 2f);
        float randomValueY = Random.Range(-2f, 2f);      

        PhotonNetwork.Instantiate(playerPrefab.name, new Vector2(randomValueX, randomValueY), Quaternion.identity, 0);
        gameCanvas.SetActive(false);
        sceneCamera.SetActive(false);
    }

}
