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

    [SerializeField] private GameObject loadingPanel;
    [SerializeField] private UIAnimator tutorialPanel, joinPanel, hostPanel, mainPanel, creditsPanel, optionsPanel, nameChoicePanel;
    [SerializeField] private GameObject[] devCredits;
    [SerializeField] private Text roomCodeText;

    [SerializeField] private Text mapNameText, mapPlayersText;
    [SerializeField] private Image mapImage;

    [SerializeField] private string[] mapNames, mapPlayerCounts;
    [SerializeField] private Sprite[] mapSprites;

    private string roomName;
    public int mapNum = 0;
    public byte playerNum = 2;

    [SerializeField] private OptionsMenu optionsMenu;

    //[SerializeField] private GameObject startButton;

    private void Awake()
    {
        optionsMenu = GetComponent<OptionsMenu>();
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.JoinLobby(TypedLobby.Default);
        loadingPanel.gameObject.SetActive(false);
        Debug.Log("I'm Connected!");
    }

    public void CreateGame()
    {
        AnalyticsManager.Get.SendHostGameEvent(roomName);
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
        if (joinPanel.gameObject.activeInHierarchy)
        {
            joinPanel.Hide();
            mainPanel.gameObject.SetActive(true);
            mainPanel.Show();
        }
        else
        {
            joinPanel.gameObject.SetActive(true);
            joinPanel.Show();
            mainPanel.Hide();
        }
    }

    public void ToggleNamePanel()
    {
        AkSoundEngine.PostEvent("UIClickWoodPanel", gameObject, gameObject);
        if (nameChoicePanel.gameObject.activeInHierarchy)
        {
            nameChoicePanel.Hide();
            mainPanel.gameObject.SetActive(true);
            mainPanel.Show();
        }
        else
        {
            nameChoicePanel.gameObject.SetActive(true);
            nameChoicePanel.Show();
            mainPanel.Hide();
        }
    }

    public void ToggleCreditsPanel()
    {
        AkSoundEngine.PostEvent("UIClickWoodPanel", gameObject, gameObject);
        if (creditsPanel.gameObject.activeInHierarchy)
        {
            creditsPanel.Hide();
            mainPanel.gameObject.SetActive(true);
            mainPanel.Show();
        }
        else
        {
            ReshuffleDevs();
            creditsPanel.gameObject.SetActive(true);
            creditsPanel.Show();
            mainPanel.Hide();
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
        if (hostPanel.gameObject.activeInHierarchy)
        {
            hostPanel.Hide();
            mainPanel.gameObject.SetActive(true);
            mainPanel.Show();
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

            hostPanel.gameObject.SetActive(true);
            hostPanel.Show();
            mainPanel.Hide();
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
        if (tutorialPanel.gameObject.activeInHierarchy)
        {
            tutorialPanel.gameObject.SetActive(false);
            mainPanel.gameObject.SetActive(true);
            mainPanel.Show();
        }
        else
        {
            tutorialPanel.gameObject.SetActive(true);
            mainPanel.Hide();
        }
    }

    public void ToggleOptions()
    {
        AkSoundEngine.PostEvent("UIClickWoodPanel", gameObject, gameObject);
        if (optionsPanel.gameObject.activeInHierarchy)
        {
            optionsPanel.Hide();
            mainPanel.gameObject.SetActive(true);
            mainPanel.Show();
        }
        else
        {
            optionsMenu.ShowCurrentName();

            optionsPanel.gameObject.SetActive(true);
            optionsPanel.Show();
            
            mainPanel.Hide();
        }
    }

    public void MapMinus()
    {
        AkSoundEngine.PostEvent("UIClickWoodPanel", gameObject, gameObject);
        mapNum = 0;
        playerNum = 2;
        mapNameText.text = mapNames[0];
        mapPlayersText.text = mapPlayerCounts[0];
        mapImage.sprite = mapSprites[0];
    }

    public void MapPlus()
    {
        AkSoundEngine.PostEvent("UIClickWoodPanel", gameObject, gameObject);
        mapNum = 1;
        playerNum = 6;
        mapNameText.text = mapNames[1];
        mapPlayersText.text = mapPlayerCounts[1];
        mapImage.sprite = mapSprites[1];
    }
}

