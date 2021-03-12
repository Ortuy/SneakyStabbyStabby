using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class Tripwire : MonoBehaviourPunCallbacks
{
    public Rigidbody2D rb;
    public float stop;
    public float start;
    public float destroyTime;

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
                target.RPC("Stop", RpcTarget.AllBuffered, stop);
                StartCoroutine("DestroyByTime");
            }

            
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {

        PhotonView target = collision.gameObject.GetComponent<PhotonView>();
        if (target != null)
        {
            if (target.tag == "Player")
            {
                target.RPC("Stop", RpcTarget.AllBuffered, start);
               
            }


        }
    }
    IEnumerator DestroyByTime()
    {
        yield return new WaitForSeconds(destroyTime);
        this.GetComponent<PhotonView>().RPC("DestroyObject", RpcTarget.AllBuffered);

    }
}
