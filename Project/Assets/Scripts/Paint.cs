using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class Paint : MonoBehaviourPunCallbacks
{
    public Rigidbody2D rb;
    public float paintDestroyTime;

    [PunRPC]
    public void DestroyObject()
    {
        Destroy(this.gameObject);
    }
    private void Awake()
    {
        
        StartCoroutine("DestroyByTime");

    }
    IEnumerator DestroyByTime()
    {
        yield return new WaitForSeconds(paintDestroyTime);
        photonView.RPC("DestroyObject", RpcTarget.AllBuffered);

    }

}
