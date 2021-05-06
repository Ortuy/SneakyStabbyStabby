using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class Blindingtrap : MonoBehaviourPunCallbacks
{
    public Rigidbody2D rb;

    [SerializeField]
    private Animator animator;

    public bool see = true;
    
    [PunRPC]
    public void DestroyObject()
    {
        Destroy(this.gameObject, 1.4f);
    }

    

    private void OnTriggerEnter2D(Collider2D collision)
    {

        PhotonView target = collision.gameObject.GetComponent<PhotonView>();
        if (target != null)
        {
            if (target.tag == "Player")
            {
                //target.RPC("Blinded", RpcTarget.AllBuffered, !see);
                StartCoroutine(BlindPlayer(target));

            }

            animator.SetBool("Activated", true);

            this.GetComponent<PhotonView>().RPC("DestroyObject", RpcTarget.AllBuffered);
        }
    }
    
    IEnumerator BlindPlayer(PhotonView player)
    {
        yield return new WaitForSeconds(0.35f);
        player.RPC("Blinded", RpcTarget.AllBuffered, !see);
    }

}
