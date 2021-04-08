using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class StabHitBox : MonoBehaviourPunCallbacks
{
    public float damage;
    public float lifetime;

    public bool valid;

    public PhotonView playerPV;
    public bool canOpenChest = false;

    /**
    private void Start()
    {
        playerPV = transform.parent.GetComponentInParent<PhotonView>();
    }


    public override void OnEnable()
    {
        StartCoroutine(WaitAndDeactivate());
    }

    IEnumerator WaitAndDeactivate()
    {
        yield return new WaitForSeconds(lifetime);
        playerPV.RPC("DeactivateStab", RpcTarget.AllBuffered);
    }
    **/
    

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (!playerPV.IsMine)
            return;
        if(collision.gameObject.transform.parent != null)
        {
            PhotonView target = collision.gameObject.transform.parent.GetComponent<PhotonView>();
            if (target != null && (!target.IsMine || target.IsRoomView))
            {
                if (target.tag == "Player" && valid)
                {
                    Debug.Log("Successful Stab!");
                    target.RPC("ReduceHealth", RpcTarget.AllBuffered, damage);
                }
                
                
            }
            
        }
        
        
            Debug.LogWarning("DUPA");
            PhotonView target1 = collision.GetComponent<PhotonView>();
            if (target1 != null)
            {
            Debug.LogWarning(target1); 
                if (target1.CompareTag("Chest"))
                {
                    target1.RPC("RandomItem", RpcTarget.AllBuffered);

                }
            }
        
    }
    private void OnTriggerStay2D(Collider2D Chest)
    {
        canOpenChest = true;
    }
}
