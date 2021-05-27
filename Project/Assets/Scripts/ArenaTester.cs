using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class ArenaTester : MonoBehaviourPunCallbacks
{
    public string sceneToLoad;

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.JoinLobby(TypedLobby.Default);
        Debug.Log("I'm Connected!");
        
    }

    public override void OnJoinedLobby()
    {
        PhotonNetwork.JoinOrCreateRoom("DUPA", new RoomOptions() { MaxPlayers = 2 }, null);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel(sceneToLoad);
    }
}
