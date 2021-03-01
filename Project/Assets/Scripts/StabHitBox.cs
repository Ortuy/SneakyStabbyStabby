using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StabHitBox : Photon.MonoBehaviour
{
    public float damage;
    public float lifetime;

    public bool valid;

    private void OnEnable()
    {
        StartCoroutine(WaitAndDeactivate());
    }

    IEnumerator WaitAndDeactivate()
    {
        yield return new WaitForSeconds(lifetime);
        photonView.RPC("Deactivate", PhotonTargets.AllBuffered);
    }

    [PunRPC]
    private void Deactivate()
    {
        this.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!photonView.isMine)
            return;
        if(collision.gameObject.transform.parent != null)
        {
            PhotonView target = collision.gameObject.transform.parent.GetComponent<PhotonView>();
            if (target != null && (!target.isMine || target.isSceneView))
            {
                if (target.tag == "Player" && valid)
                {
                    Debug.Log("Successful Stab!");
                    target.RPC("ReduceHealth", PhotonTargets.AllBuffered, damage);
                }
            }
        }        
    }
}
