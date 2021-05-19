using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class WaitRoomPortal35 : MonoBehaviourPunCallbacks
{
    public GameObject StartPortal8;
    public Player Player;
    public GameManager gameManager;
    public bool timeSpawnEnd8 = false;

    [SerializeField] private Animator portalAnimator;

    private void OnTriggerStay2D(Collider2D collision)
    {
        PhotonView target = collision.gameObject.GetComponent<PhotonView>();

        if (collision.tag == "Player")
        {
            Player = collision.GetComponent<Player>();
            gameManager.readyToStart8 = true;
            //target.RPC("TeleportToStart1", RpcTarget.AllBuffered);
            if (gameManager.readyToStart3 == true && gameManager.readyToStart4 == true && gameManager.readyToStart5 == true && gameManager.readyToStart6 == true && gameManager.readyToStart7 == true && gameManager.readyToStart8 == true)
            {
                EffectsManager.instance.FadeUnfade();
                StartCoroutine(Ready8());
            }
            if (timeSpawnEnd8 == true)
            {
                Player.transform.position = new Vector2(StartPortal8.transform.position.x, StartPortal8.transform.position.y);
                portalAnimator.SetBool("Open", true);
                StartCoroutine(StopPortal8());
            }
            if (Input.GetKeyDown(KeyCode.F))
            {
                StartCoroutine(Ready8());
            }

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            gameManager.readyToStart8 = false;
        }

    }
    IEnumerator Ready8()
    {

        yield return new WaitForSeconds(0.45f);
        AkSoundEngine.PostEvent("sfx_teleport", gameObject, gameObject);
        timeSpawnEnd8 = true;
        Player.stabLock = false;

    }

    IEnumerator StopPortal8()
    {
        yield return null;
        portalAnimator.SetBool("Open", false);
    }
}
