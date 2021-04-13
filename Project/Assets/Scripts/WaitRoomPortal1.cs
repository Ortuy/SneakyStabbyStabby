using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class WaitRoomPortal1 : MonoBehaviourPunCallbacks
{
    public GameObject StartPortal1;
    public GameObject Player;
    public GameManager gameManager;
    public bool timeSpawnEnd1 = false;


    private void OnTriggerStay2D(Collider2D Player)
    {

        Debug.Log("penis1");
        //PhotonView target = collision.gameObject.GetComponent<PhotonView>();

        if (Player.tag == "Player")
        {
            gameManager.readyToStart1 = true;
            //target.RPC("TeleportToStart1", RpcTarget.AllBuffered);
            if (gameManager.readyToStart == true && gameManager.readyToStart1 == true)
            {
                StartCoroutine(Ready1());
            }
            if (timeSpawnEnd1 == true)
            {
                Player.transform.position = new Vector2(StartPortal1.transform.position.x, StartPortal1.transform.position.y);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(Player.tag == "Player")
        {
            gameManager.readyToStart1 = false;
        }

    }
    IEnumerator Ready1()
    {
        yield return new WaitForSeconds(0.2f);
        timeSpawnEnd1 = true;
    }

}
