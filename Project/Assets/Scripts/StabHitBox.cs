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
        Debug.LogWarning("DUPA");
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

                    Vector2 temp = Vector2.zero;

                    Vector2 attackerPos = transform.parent.parent.position;

                    Vector2 targetPos = collision.transform.position;

                    temp = attackerPos - targetPos;

                    var newAngle = Mathf.Rad2Deg * Mathf.Atan2(temp.y, temp.x) + 180;

                    //float newAngle = Vector2.Angle(Vector2.left + pos2D, mousePos);
                    target.GetComponent<Health>().hurtFX.transform.rotation = Quaternion.AngleAxis(newAngle, Vector3.forward);

                    target.RPC("ReduceHealth", RpcTarget.AllBuffered, damage);
                }
                
                
            }
            if(target != null && LayerMask.LayerToName(target.gameObject.layer) == "Detector")
            {
                target.RPC("DestroyDetector", RpcTarget.AllBuffered);
            }
            
        }
        
        
            
            var target1 = collision.GetComponent<Chest>();
            if (target1 != null)
            {
            Debug.LogWarning(target1); 
                if (target1.CompareTag("Chest"))
                {
                    target1.RandomItem();

                }
            }
        
    }
    //private void OnTriggerStay2D(Collider2D Chest)
    //{
    //    canOpenChest = true;
    //}
}
