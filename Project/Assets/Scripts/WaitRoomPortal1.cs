using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class WaitRoomPortal1 : MonoBehaviourPunCallbacks
{
    public GameObject StartPortal1;
    public Player Player;
    public GameManager gameManager;
    public bool timeSpawnEnd1 = false;

    [SerializeField] private Animator portalAnimator;

    private void OnTriggerStay2D(Collider2D collision)
    {

        Debug.Log("penis1");
        PhotonView target = collision.gameObject.GetComponent<PhotonView>();

        if (collision.tag == "Player")
        {
            Player = collision.GetComponent<Player>();
            gameManager.readyToStart1 = true;
            //target.RPC("TeleportToStart1", RpcTarget.AllBuffered);
            if (gameManager.readyToStart == true && gameManager.readyToStart1 == true)
            {
                StartCoroutine(Ready1());
                EffectsManager.instance.FadeUnfade();
            }
            if (timeSpawnEnd1 == true)
            {
                Player.transform.position = new Vector2(StartPortal1.transform.position.x, StartPortal1.transform.position.y);
                portalAnimator.SetBool("Open", true);
                StartCoroutine(StopPortal());
            }
            if (Input.GetKeyDown(KeyCode.F))
            {
                StartCoroutine(Ready1());
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            gameManager.readyToStart1 = false;
        }

    }
    IEnumerator Ready1()
    {
        yield return new WaitForSeconds(0.45f);
        timeSpawnEnd1 = true;
        Player.stabLock = false;
    }

    IEnumerator StopPortal()
    {
        yield return null;
        portalAnimator.SetBool("Open", false);
    }

}
