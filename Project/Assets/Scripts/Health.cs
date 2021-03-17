using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Health : MonoBehaviourPunCallbacks
{
    public Player plMove;
    public float healthAmount;
    public Rigidbody2D rb;
    public CircleCollider2D cc;
    public SpriteRenderer sr;
   
    //public GameObject playerCanvas;

    public ParticleSystem deathFX;

    private void Awake()
    {
        if (photonView.IsMine)
        {
            GameManager.instance.localPlayer = this.gameObject;
        }
    }
    

    [PunRPC] public void ReduceHealth(float amount)
    {
        ModifyHealth(amount);
    }

    private void CheckHealth()
    {
        if(photonView.IsMine && healthAmount <= 0)
        {
            GameManager.instance.EnableRespawn();
            plMove.disableInput = true;
            plMove.moveSpeed = 0;
            this.GetComponent<PhotonView>().RPC("Dead", RpcTarget.AllBuffered);
        }
    }

    public void EnableInput()
    {
        plMove.disableInput = false;
        
    }

    [PunRPC]
    private void Dead()
    {
        GameManager.instance.victoryText.gameObject.SetActive(true);
        GameManager.instance.victoryText.text = "Player " + GetComponent<Player>().playerID + " vanquished!";
        deathFX.Play();
        //cc.enabled = false;
        //sr.enabled = false;
        //playerCanvas.SetActive(false);
    }

    [PunRPC]
    private void Respawn()
    {
        GameManager.instance.victoryText.gameObject.SetActive(false);
        GameManager.instance.ReloadScene();
        //cc.enabled = true;
        //sr.enabled = true;
        //playerCanvas.SetActive(true);
    }

    private void ModifyHealth(float amount)
    {
        if (photonView.IsMine)
        {
            healthAmount -= amount;
        }
        else
        {
            healthAmount -= amount;
        }
        CheckHealth();
    }
    
    
}
