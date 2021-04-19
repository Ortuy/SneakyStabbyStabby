using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class MainMenu : MonoBehaviourPunCallbacks
{
    //[SerializeField] private string versionName = "2021.02.22";

    [SerializeField] private GameObject connectPanel;
    [SerializeField] private InputField createGameInput;
    [SerializeField] private InputField joinGameInput;

    [SerializeField] private GameObject loadingPanel, tutorialPanel, joinPanel, hostPanel, mainPanel;
    [SerializeField] private Text roomCodeText;

    private string roomName;

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
        PhotonNetwork.CreateRoom(roomName, new RoomOptions() { MaxPlayers = 2 }, null);
    }

    public void JoinGame()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2;
        //PhotonNetwork.JoinOrCreateRoom(joinGameInput.text, roomOptions, TypedLobby.Default);
        PhotonNetwork.JoinRoom(joinGameInput.text);
    }

    public void ToggleJoinPanel()
    {
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

    public void ToggleHostPanel()
    {
        if (hostPanel.activeInHierarchy)
        {
            hostPanel.SetActive(false);
            mainPanel.SetActive(true);
        }
        else
        {
            char c = (char)('A' + Random.Range(0, 26));
            roomName = c.ToString();

            for(int i = 0; i < 3; i++)
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
        PhotonNetwork.LoadLevel("Arena");
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void ToggleTutorial()
    {
        if(tutorialPanel.activeInHierarchy)
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
}
