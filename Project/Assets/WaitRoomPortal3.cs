using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class WaitRoomPortal3 : MonoBehaviourPunCallbacks
{
    public GameObject StartPortal3;
    public Player Player;
    public GameManager gameManager;
    public bool timeSpawnEnd3 = false;

    [SerializeField] private Animator portalAnimator;

    private void OnTriggerStay2D(Collider2D collision)
    {
        PhotonView target = collision.gameObject.GetComponent<PhotonView>();

        if (collision.tag == "Player")
        {
            Player = collision.GetComponent<Player>();
            gameManager.readyToStart3 = true;
            //target.RPC("TeleportToStart1", RpcTarget.AllBuffered);
            if (gameManager.readyToStart3 == true && gameManager.readyToStart4 == true && gameManager.readyToStart5 == true && gameManager.readyToStart6 == true && gameManager.readyToStart7 == true && gameManager.readyToStart8 == true)
            {
                EffectsManager.instance.FadeUnfade();
                StartCoroutine(Ready3());
            }
            if (timeSpawnEnd3 == true)
            {
                Player.transform.position = new Vector2(StartPortal3.transform.position.x, StartPortal3.transform.position.y);
                portalAnimator.SetBool("Open", true);
                StartCoroutine(StopPortal3());
            }
            if (Input.GetKeyDown(KeyCode.F))
            {
                StartCoroutine(Ready3());
            }

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            gameManager.readyToStart3 = false;
        }

    }
    IEnumerator Ready3()
    {

        yield return new WaitForSeconds(0.45f);
        AkSoundEngine.PostEvent("sfx_teleport", gameObject, gameObject);
        timeSpawnEnd3 = true;
        Player.stabLock = false;

    }

    IEnumerator StopPortal3()
    {
        yield return null;
        portalAnimator.SetBool("Open", false);
    }
}
