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
                Vector2 temp = Vector2.zero;

                Vector2 attackerPos = transform.position;

                Vector2 targetPos = collision.transform.position;

                temp = attackerPos - targetPos;

                var newAngle = Mathf.Rad2Deg * Mathf.Atan2(temp.y, temp.x) + 180;

                //float newAngle = Vector2.Angle(Vector2.left + pos2D, mousePos);
                target.GetComponent<Health>().hurtFX.transform.rotation = Quaternion.AngleAxis(newAngle, Vector3.forward);

                target.RPC("ReduceHealth", RpcTarget.AllBuffered, boltDamage);
            }

            this.GetComponent<PhotonView>().RPC("DestroyObject", RpcTarget.AllBuffered);
        }
    }
}

