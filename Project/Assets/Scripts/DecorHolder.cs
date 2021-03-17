using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class DecorHolder : MonoBehaviourPunCallbacks
{
    //public static DecorHolder instance;
    /*
    // Start is called before the first frame update
    void Start()
    {
        //if()
        if(instance != null)
        {
            photonView.RPC("DestroyForAllClients", RpcTarget.AllBuffered);
        }
        else
        {
            photonView.RPC("SetInstance", RpcTarget.AllBuffered);
            //instance = this;
        }
    }

    [PunRPC]
    public void DestroyForAllClients()
    {
        Destroy(gameObject);
        //gameObject.SetActive(false);
    }

    [PunRPC]
    public void SetInstance()
    {
        //Destroy(gameObject);
        //gameObject.SetActive(false);
        instance = this;
    }*/
}
