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

    private bool usedUp;

    private Animator animator;
    public Transform lowerPoint, upperPoint;

    [SerializeField]
    private GameObject web;
    public Player player;

    [SerializeField]
    private ParticleSystem webBreakFX;

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
        if (target != null && !usedUp)
        {
            if (target.tag == "Player")
            {
                AkSoundEngine.PostEvent("sfx_obj_tripwire_activate", gameObject, gameObject);
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
                AkSoundEngine.PostEvent("sfx_obj_tripwire", gameObject, gameObject);

                target.RPC("Stop", RpcTarget.AllBuffered, stop);
                target.GetComponent<Health>().cFollow.ShakeCamera(1);
                StartCoroutine("DestroyByTime");
            }
            else if (target.CompareTag("Bolt"))
            {
                AkSoundEngine.PostEvent("sfx_obj_tripwire_activate", gameObject, gameObject);
                AkSoundEngine.PostEvent("sfx_obj_tripwire", gameObject, gameObject);

                bool flip = false;
                if (Vector2.Distance(target.transform.position, lowerPoint.position) > Vector2.Distance(target.transform.position, upperPoint.position))
                {
                    flip = true;
                }

                animator.SetBool("Snap", true);
                web.transform.position = target.transform.position;
                GetComponent<PhotonView>().RPC("SetFlipDirection", RpcTarget.AllBuffered, flip);
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
        usedUp = true;
        //GetComponent<BoxCollider2D>().enabled = false;
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
                    target.GetComponent<Health>().cFollow.ShakeCamera(1);
                    DestroyWeb();
                }

            }


        }
    }
    public void DestroyWeb()
    {
        
        Instantiate(webBreakFX, web.transform.position, Quaternion.identity);
        this.GetComponent<PhotonView>().RPC("DestroyObject", RpcTarget.AllBuffered);
    }
}
