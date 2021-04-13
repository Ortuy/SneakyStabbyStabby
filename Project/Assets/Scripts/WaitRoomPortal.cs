using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class WaitRoomPortal : MonoBehaviourPunCallbacks
{
    public GameObject StartPortal;
    public GameObject Player;
    public GameManager gameManager;
    public bool timeSpawnEnd = false;



    private void OnTriggerStay2D(Collider2D Player)
    {

        Debug.Log("penis");
        //PhotonView target = collision.gameObject.GetComponent<PhotonView>();

        if (Player.tag == "Player")
        {
            gameManager.readyToStart = true;
            //target.RPC("TeleportToStart1", RpcTarget.AllBuffered);
            if(gameManager.readyToStart == true && gameManager.readyToStart1 == true)
            {
                
                StartCoroutine(Ready());
            }
            if (timeSpawnEnd == true)
            {
                Player.transform.position = new Vector2(StartPortal.transform.position.x, StartPortal.transform.position.y);
            }
           
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (Player.tag == "Player")
        {
            gameManager.readyToStart = false;
        }

    }
    IEnumerator Ready()
    {
        yield return new WaitForSeconds(0.2f);
        timeSpawnEnd = true;
    }
}
