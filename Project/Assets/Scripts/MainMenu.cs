using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class MainMenu : MonoBehaviourPunCallbacks
{
    //[SerializeField] private string versionName = "2021.02.22";

    //[SerializeField] private GameObject connectPanel;
    //[SerializeField] private InputField createGameInput;
    [SerializeField] private InputField joinGameInput;

    [SerializeField] private GameObject loadingPanel, tutorialPanel, joinPanel, hostPanel, mainPanel, creditsPanel, optionsPanel;
    [SerializeField] private GameObject[] devCredits;
    [SerializeField] private Text roomCodeText;

    private string roomName;
    public int mapNum;
    public byte playerNum = 2; 

    //[SerializeField] private GameObject startButton;

    private void Awake()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.JoinLobby(TypedLobby.Default);
        loadingPanel.SetActive(false);
        Debug.Log("I'm Connected!");
    }

    public void CreateGame()
    {
        PhotonNetwork.CreateRoom(roomName, new RoomOptions() { MaxPlayers = playerNum }, null);
    }

    public void JoinGame()
    {
        AkSoundEngine.PostEvent("ui_click_wood_panel_exit", gameObject, gameObject);
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = playerNum;
        //PhotonNetwork.JoinOrCreateRoom(joinGameInput.text, roomOptions, TypedLobby.Default);
        PhotonNetwork.JoinRoom(joinGameInput.text);
    }

    public void ToggleJoinPanel()
    {
        AkSoundEngine.PostEvent("UIClickWoodPanel", gameObject, gameObject);
        if (joinPanel.activeInHierarchy)
        {
            joinPanel.SetActive(false);
            mainPanel.SetActive(true);
        }
        else
        {
            joinPanel.SetActive(true);
            mainPanel.SetActive(false);
        }
    }

    public void ToggleCreditsPanel()
    {
        AkSoundEngine.PostEvent("UIClickWoodPanel", gameObject, gameObject);
        if (creditsPanel.activeInHierarchy)
        {
            creditsPanel.SetActive(false);
            mainPanel.SetActive(true);
        }
        else
        {
            ReshuffleDevs();
            creditsPanel.SetActive(true);
            mainPanel.SetActive(false);
        }
    }

    //Shamelessly copied from unity forums
    void ReshuffleDevs()
    {
        for (int i = 0; i < devCredits.Length; i++)
        {
            var tmp = devCredits[i];
            int r = Random.Range(i, devCredits.Length);
            devCredits[i] = devCredits[r];
            devCredits[r] = tmp;
        }

        for (int i = 0; i < devCredits.Length; i++)
        {
            devCredits[i].transform.SetSiblingIndex(i);
        }
    }

    public void ToggleHostPanel()
    {
        AkSoundEngine.PostEvent("UIClickWoodPanel", gameObject, gameObject);
        if (hostPanel.activeInHierarchy)
        {
            hostPanel.SetActive(false);
            mainPanel.SetActive(true);
        }
        else
        {
            char c = (char)('A' + Random.Range(0, 26));
            roomName = c.ToString();

            for (int i = 0; i < 3; i++)
            {
                c = (char)('A' + Random.Range(0, 26));
                roomName += c.ToString();
            }

            roomCodeText.text = roomName;

            hostPanel.SetActive(true);
            mainPanel.SetActive(false);
        }
    }

    public override void OnJoinedRoom()
    {
        if (mapNum == 0)
        {
            PhotonNetwork.LoadLevel("Arena");
        }
        if (mapNum == 1)
        {
            PhotonNetwork.LoadLevel("ArenaLarge");
        }

    }

    public void Exit()
    {
        AkSoundEngine.PostEvent("ui_click_wood_panel_exit", gameObject, gameObject);

        Application.Quit();
    }

    public void ToggleTutorial()
    {
        AkSoundEngine.PostEvent("UIClickWoodPanel", gameObject, gameObject);
        if (tutorialPanel.activeInHierarchy)
        {
            tutorialPanel.SetActive(false);
            mainPanel.SetActive(true);
        }
        else
        {
            tutorialPanel.SetActive(true);
            mainPanel.SetActive(false);
        }
    }

    public void ToggleOptions()
    {
        AkSoundEngine.PostEvent("UIClickWoodPanel", gameObject, gameObject);
        if (optionsPanel.activeInHierarchy)
        {
            optionsPanel.SetActive(false);
            mainPanel.SetActive(true);
        }
        else
        {
            optionsPanel.SetActive(true);
            mainPanel.SetActive(false);
        }
    }
    public void MapMinus()
    {
        mapNum = 0;
        playerNum = 2;
    }
    public void MapPlus()
    {
        mapNum = 1;
        playerNum = 6;
    }
}

