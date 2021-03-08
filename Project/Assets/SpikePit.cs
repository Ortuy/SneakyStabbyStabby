using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpikePit : MonoBehaviourPunCallbacks
{

    public Rigidbody2D rb;
    public float spikeDamage;

    [PunRPC]
    public void DestroyObject()
    {
        Destroy(this.gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        PhotonView target = collision.gameObject.GetComponent<PhotonView>();
        
        {
            if (target.tag == "Player")
            {
                target.RPC("ReduceHealth", RpcTarget.AllBuffered, spikeDamage);
            }

            this.GetComponent<PhotonView>().RPC("DestroyObject", RpcTarget.AllBuffered);
        }
    }
}
