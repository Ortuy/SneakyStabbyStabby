using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Gold : MonoBehaviourPunCallbacks
{
    public Player player;
    public int goldAmount;

    public GameObject particleEffect;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        PhotonView target = collision.gameObject.GetComponent<PhotonView>();


       if (collision.tag == "Player")
       {
            
          AkSoundEngine.PostEvent("sfx_pickup_gold", gameObject, gameObject);
          player = collision.GetComponent<Player>();
          player.gold += goldAmount;
          Debug.Log("yee");
          var particle = Instantiate(particleEffect, transform.position, Quaternion.identity);
          Destroy(particle, 5f);
          Destroy(gameObject);
        }

       
    }
}
