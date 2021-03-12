using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class Blindingtrap : MonoBehaviourPunCallbacks
{
    public Rigidbody2D rb;
    public bool see = false;
    
    [PunRPC]
    public void DestroyObject()
    {
        Destroy(this.gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        PhotonView target = collision.gameObject.GetComponent<PhotonView>();
        if (target != null)
        {
            if (target.tag == "Player")
            {
                target.RPC("Blinded", RpcTarget.AllBuffered, see);
                

            }

            this.GetComponent<PhotonView>().RPC("DestroyObject", RpcTarget.AllBuffered);
        }
    }
    
}
