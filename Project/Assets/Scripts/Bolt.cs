using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bolt : Photon.MonoBehaviour
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
        this.GetComponent<PhotonView>().RPC("DestroyObject", PhotonTargets.AllBuffered);
    }

    [PunRPC]
    public void DestroyObject()
    {
        Destroy(this.gameObject);
    }
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!photonView.isMine)
            return;
        PhotonView target = collision.gameObject.GetComponent<PhotonView>();
        if(target != null && (!target.isMine || target.isSceneView))
        {
            if(target.tag == "Player")
            {
                target.RPC("ReduceHealth", PhotonTargets.AllBuffered, boltDamage);
            }

            this.GetComponent<PhotonView>().RPC("DestroyObject", PhotonTargets.AllBuffered);
        }
    }
}

