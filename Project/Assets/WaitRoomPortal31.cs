using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class WaitRoomPortal31 : MonoBehaviourPunCallbacks
{
    public GameObject StartPortal4;
    public Player Player;
    public GameManager gameManager;
    public bool timeSpawnEnd4 = false;

    [SerializeField] private Animator portalAnimator;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            gameManager.map2 = true;
        }

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        PhotonView target = collision.gameObject.GetComponent<PhotonView>();

        if (collision.tag == "Player")
        {
            Player = collision.GetComponent<Player>();
            gameManager.readyToStart4 = true;
            //target.RPC("TeleportToStart1", RpcTarget.AllBuffered);
            if (gameManager.readyToStart3 == true && gameManager.readyToStart4 == true && gameManager.readyToStart5 == true && gameManager.readyToStart6 == true && gameManager.readyToStart7 == true && gameManager.readyToStart8 == true)
            {
                EffectsManager.instance.FadeUnfade();
                StartCoroutine(Ready4());
            }
            if (timeSpawnEnd4 == true)
            {
                Player.transform.position = new Vector2(StartPortal4.transform.position.x, StartPortal4.transform.position.y);
                portalAnimator.SetBool("Open", true);
                StartCoroutine(StopPortal4());
            }
            if (Input.GetKeyDown(KeyCode.F))
            {
                StartCoroutine(Ready4());
            }

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            gameManager.readyToStart4 = false;
        }

    }
    IEnumerator Ready4()
    {

        yield return new WaitForSeconds(0.45f);
        AkSoundEngine.PostEvent("sfx_teleport", gameObject, gameObject);
        timeSpawnEnd4 = true;
        Player.stabLock = false;

    }

    IEnumerator StopPortal4()
    {
        yield return null;
        portalAnimator.SetBool("Open", false);
    }
}
