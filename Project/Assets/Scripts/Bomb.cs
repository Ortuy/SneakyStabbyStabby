using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class Bomb : MonoBehaviourPunCallbacks
{
    public Rigidbody2D rb;
    public GameObject rotatingItem;
    public Transform gasExitPos;
    public GameObject gasObject;
    public float time = 2;
    [PunRPC]
    public void DestroyObject()
    {
        Destroy(this.gameObject);
    }

    private void Awake()
    {
        StartCoroutine(DestroyByTime());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        PhotonView target = collision.gameObject.GetComponent<PhotonView>();
        if (target != null)
        {
            if (target.tag == "Player")
            {
                
                StartCoroutine("DestroyByTime");
            }

            
        }
    }

    IEnumerator DestroyByTime()
    {
        yield return new WaitForSeconds(time);

        var allNearbyPlayers = Physics2D.OverlapCircleAll(transform.position, 5, LayerMask.GetMask("MeleeHurtable"));
        
        foreach (Collider2D collider in allNearbyPlayers)
        {
            if(collider.transform.parent.CompareTag("Player"))
            {
                collider.GetComponentInParent<Health>().cFollow.ShakeCamera(3);
            }
        }

        this.GetComponent<PhotonView>().RPC("DestroyObject", RpcTarget.AllBuffered);
        GameObject obj = PhotonNetwork.Instantiate(gasObject.name, new Vector2(gasExitPos.transform.position.x, gasExitPos.transform.position.y), rotatingItem.transform.rotation, 0);
    }
}
