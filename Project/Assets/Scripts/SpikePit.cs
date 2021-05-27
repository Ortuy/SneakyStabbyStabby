using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpikePit : MonoBehaviourPunCallbacks
{

    //public Rigidbody2D rb;
    [SerializeField] private Animator animator;
    [SerializeField] private ParticleSystem activateFX;
    private bool usedUp;
    public float spikeDamage;

    [PunRPC]
    public void DestroyObject(float duration)
    {
        Destroy(this.gameObject, duration);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!usedUp)
        {
            PhotonView target = collision.gameObject.GetComponent<PhotonView>();
            if (target != null)
            {
                if (target.tag == "Player")
                {
                    target.RPC("ReduceHealth", RpcTarget.AllBuffered, spikeDamage);
                    animator.SetBool("Activated", true);
                    activateFX.Play();

                    this.GetComponent<PhotonView>().RPC("DestroyObject", RpcTarget.AllBuffered, 3f);
                    usedUp = true;
                }


            }
        }
        
    }
}
