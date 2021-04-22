using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Detector : MonoBehaviourPunCallbacks
{
    private Player lastMarkedPlayer, lastMarkedPlayerExit;
    [SerializeField] private Animator animator;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            animator.SetBool("Alert", true);
            lastMarkedPlayer = collision.GetComponent<Player>();
            photonView.RPC("MarkPlayer", RpcTarget.AllBuffered, true);
        }
    }

    [PunRPC]
    public void DestroyDetector()
    {
        Destroy(gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(!Physics2D.OverlapCircle(transform.position, 2f, LayerMask.GetMask("MeleeHurtable")))
            {
                animator.SetBool("Alert", false);
            }
            lastMarkedPlayerExit = collision.GetComponent<Player>();
            photonView.RPC("MarkPlayer", RpcTarget.AllBuffered, false);
        }
    }

    [PunRPC]
    private void MarkPlayer(bool state)
    {
        if(state)
        {
            if (!lastMarkedPlayer.photonView.IsMine)
            {
                lastMarkedPlayer.mapIcon.SetActive(state);
            }
            lastMarkedPlayer.isByDetector = state;
        }
        else
        {
            Debug.Log(lastMarkedPlayerExit);
            if (!lastMarkedPlayerExit.photonView.IsMine)
            {
                lastMarkedPlayerExit.mapIcon.SetActive(state);
            }
            lastMarkedPlayerExit.isByDetector = state;
        }
        
    }
}
