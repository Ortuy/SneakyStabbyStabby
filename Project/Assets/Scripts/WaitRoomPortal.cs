using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class WaitRoomPortal : MonoBehaviourPunCallbacks
{
    public GameObject StartPortal;
    public Player Player;
    public GameManager gameManager;
    public bool timeSpawnEnd = false;

    [SerializeField] private Animator portalAnimator;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            gameManager.map1 = true;
        }

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        PhotonView target = collision.gameObject.GetComponent<PhotonView>();

        if (collision.tag == "Player")
        {
            Player = collision.GetComponent<Player>();
            gameManager.readyToStart = true;
            //target.RPC("TeleportToStart1", RpcTarget.AllBuffered);
            if (gameManager.readyToStart == true && gameManager.readyToStart1 == true)
            {
                EffectsManager.instance.FadeUnfade();
                StartCoroutine(Ready());
            }
            if (timeSpawnEnd == true)
            {
                Player.transform.position = new Vector2(StartPortal.transform.position.x, StartPortal.transform.position.y);
                portalAnimator.SetBool("Open", true);
                StartCoroutine(StopPortal());
            }
            if (Input.GetKeyDown(KeyCode.F))
            {
                StartCoroutine(Ready());
            }

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            gameManager.readyToStart = false;
        }

    }
    IEnumerator Ready()
    {

        yield return new WaitForSeconds(0.45f);
        AkSoundEngine.PostEvent("sfx_teleport", gameObject, gameObject);
        timeSpawnEnd = true;
        Player.stabLock = false;

    }

    IEnumerator StopPortal()
    {
        yield return null;
        portalAnimator.SetBool("Open", false);
    }
}
