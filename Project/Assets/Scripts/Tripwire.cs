using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class Tripwire : MonoBehaviourPunCallbacks
{
    public Rigidbody2D rb;
    public float stop;
    public float start;
    public float destroyTime;
    public bool isTrapped;

    private Animator animator;
    public Transform lowerPoint, upperPoint;

    [SerializeField]
    private GameObject web;
    public Player player;

    [PunRPC]
    public void DestroyObject()
    {
        Destroy(this.gameObject);
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        

        PhotonView target = collision.gameObject.GetComponent<PhotonView>();
        if (target != null)
        {
            if (target.tag == "Player")
            {
                player = collision.GetComponent<Player>();
                //player.settingTrap = true;
                player.isTrapped = true;
                bool flip = false;
                if (Vector2.Distance(target.transform.position, lowerPoint.position) > Vector2.Distance(target.transform.position, upperPoint.position))
                {
                    flip = true;
                }

                animator.SetBool("Snap", true);
                web.transform.position = target.transform.position;
                GetComponent<PhotonView>().RPC("SetFlipDirection", RpcTarget.AllBuffered, flip);

                target.RPC("Stop", RpcTarget.AllBuffered, stop);
                StartCoroutine("DestroyByTime");
            }

            
        }
    }
    
    [PunRPC]
    private void SetFlipDirection(bool flip)
    {
        GetComponent<SpriteRenderer>().flipY = flip;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        PhotonView target = collision.gameObject.GetComponent<PhotonView>();
        if (target != null)
        {
            if (target.tag == "Player")
            {
                player = collision.GetComponent<Player>();
                //player.settingTrap = false;
                player.isTrapped = false;
                bool flip = false;
                if (Vector2.Distance(target.transform.position, lowerPoint.position) > Vector2.Distance(target.transform.position, upperPoint.position))
                {
                    flip = true;
                }

                animator.SetBool("Snap", true);
                web.transform.position = target.transform.position;
                GetComponent<PhotonView>().RPC("SetFlipDirection", RpcTarget.AllBuffered, flip);

                target.RPC("Stop", RpcTarget.AllBuffered, start);
               
            }


        }
    }

    IEnumerator DestroyByTime()
    {
        yield return new WaitForSeconds(destroyTime);
        this.GetComponent<PhotonView>().RPC("DestroyObject", RpcTarget.AllBuffered);

    }
    private void OnTriggerStay2D(Collider2D collision)
    {

        PhotonView target = collision.gameObject.GetComponent<PhotonView>();
        if (target != null)
        {
            if (target.tag == "Player")
            {
                player = collision.GetComponent<Player>();
                if (player.destroyWeb == true)
                {
                    DestroyWeb();
                }

            }


        }
    }
    public void DestroyWeb()
    {
        this.GetComponent<PhotonView>().RPC("DestroyObject", RpcTarget.AllBuffered);
    }
}
