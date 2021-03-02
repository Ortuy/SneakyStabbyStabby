using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Bolt : MonoBehaviourPunCallbacks
{
    //public bool moveDir = false;
    public float moveSpeed;
    public float destroyTime;
    public Rigidbody2D rb;
    public float boltDamage;

    private void Awake()
    {
        StartCoroutine("DestroyByTime");
    }

    IEnumerator DestroyByTime()
    {
        yield return new WaitForSeconds(destroyTime);
        this.GetComponent<PhotonView>().RPC("DestroyObject", RpcTarget.AllBuffered);
    }

    [PunRPC]
    public void DestroyObject()
    {
        Destroy(this.gameObject);
    }
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!photonView.IsMine)
            return;
        PhotonView target = collision.gameObject.GetComponent<PhotonView>();
        if(target != null && (!target.IsMine || target.IsRoomView))
        {
            if(target.tag == "Player")
            {
                target.RPC("ReduceHealth", RpcTarget.AllBuffered, boltDamage);
            }

            this.GetComponent<PhotonView>().RPC("DestroyObject", RpcTarget.AllBuffered);
        }
    }
}

