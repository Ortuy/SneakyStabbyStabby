using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : Photon.MonoBehaviour
{
    public Player plMove;
    public float healthAmount;
    public Rigidbody2D rb;
    public CircleCollider2D cc;
    public SpriteRenderer sr;
    public GameObject playerCanvas;

    private void Awake()
    {
        if (photonView.isMine)
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
        if(photonView.isMine && healthAmount <= 0)
        {
            GameManager.instance.EnableRespawn();
            plMove.disableInput = true;
            this.GetComponent<PhotonView>().RPC("Dead", PhotonTargets.AllBuffered);
        }
    }

    public void EnableInput()
    {
        plMove.disableInput = false;
    }

    [PunRPC]
    private void Dead()
    {
        cc.enabled = false;
        sr.enabled = false;
        playerCanvas.SetActive(false);
    }
    [PunRPC]
    private void Respawn()
    {
        cc.enabled = true;
        sr.enabled = true;
        playerCanvas.SetActive(true);
    }
    private void ModifyHealth(float amount)
    {
        if (photonView.isMine)
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
