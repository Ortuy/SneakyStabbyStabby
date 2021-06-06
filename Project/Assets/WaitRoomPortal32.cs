using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class WaitRoomPortal32 : MonoBehaviourPunCallbacks
{
    public GameObject StartPortal5;
    public Player Player;
    public GameManager gameManager;
    public bool timeSpawnEnd5 = false;

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
            gameManager.readyToStart5 = true;
            //target.RPC("TeleportToStart1", RpcTarget.AllBuffered);
            if (gameManager.readyToStart3 == true && gameManager.readyToStart4 == true && gameManager.readyToStart5 == true && gameManager.readyToStart6 == true && gameManager.readyToStart7 == true && gameManager.readyToStart8 == true)
            {
                EffectsManager.instance.FadeUnfade();
                StartCoroutine(Ready5());
            }
            if (timeSpawnEnd5 == true)
            {
                Player.transform.position = new Vector2(StartPortal5.transform.position.x, StartPortal5.transform.position.y);
                portalAnimator.SetBool("Open", true);
                StartCoroutine(StopPortal5());
            }
            //if (Input.GetKeyDown(KeyCode.F))
            //{
            //    StartCoroutine(Ready5());
            //}

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            gameManager.readyToStart5 = false;
        }

    }
    IEnumerator Ready5()
    {

        yield return new WaitForSeconds(0.45f);
        AkSoundEngine.PostEvent("sfx_teleport", gameObject, gameObject);
        timeSpawnEnd5 = true;
        Player.stabLock = false;
        Player.inWaitRoom = false;
    }

    IEnumerator StopPortal5()
    {
        yield return null;
        portalAnimator.SetBool("Open", false);
    }
}
