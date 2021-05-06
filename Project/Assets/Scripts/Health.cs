using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Health : MonoBehaviourPunCallbacks
{
    public Player plMove;
    public Player player;
    public float healthAmount;
    public Rigidbody2D rb;
    public CircleCollider2D cc;
    public SpriteRenderer sr;
    public bool isGhost = false;
    public float ghostTime = 3;

    public CameraFollow cFollow;

    //public GameObject playerCanvas;

    public GameObject[] lifeMarkers;
    public ParticleSystem deathFX, hurtFX;

    private void Awake()
    {
        if (photonView.IsMine)
        {
            GameManager.localInstance.localPlayer = this.gameObject;
        }
    }
    

    [PunRPC] public void ReduceHealth(float amount)
    {
        if(isGhost == false)
        {
            hurtFX.Play();
            ModifyHealth(amount);
            StartCoroutine(StabHitStop(true, 0.5f));
            //isGhost = true;
            //StartCoroutine("GhostEnum");
            //if (isGhost == true && healthAmount >0)
            //{
            //    Color playerColor = new Color(player.recolorSprites[0].color.r, player.recolorSprites[0].color.g, player.recolorSprites[0].color.b, 0);

            //    //player.ghost.SetActive(true);
            //    player.ghostSprites[0].color = new Color(1, 1, 1, 1);
            //    player.ghostSprites[1].color = new Color(1, 1, 1, 1);
            //    player.nonRecolorSprites[0].color = new Color(1, 1, 1, 0);
            //    player.nonRecolorSprites[1].color = new Color(1, 1, 1, 0);
            //    player.recolorSprites[0].color = playerColor;
            //    player.recolorSprites[1].color = playerColor;

            //    player.moveSpeed = 10;
            //    player.stabReady = false;
            //}

            
        }
        
    }

    [PunRPC] public void HitStop()
    {
        StartCoroutine(StabHitStop(false, 0.5f));
    }

    IEnumerator StabHitStop(bool hitTaken, float duration)
    {
        cFollow.ShakeCamera(0);
        player.torsoAnimator.speed = 0;
        player.legsAnimator.speed = 0;
        player.isTrapped = true;

        yield return new WaitForSeconds(duration);

        player.torsoAnimator.speed = 1;
        player.legsAnimator.speed = 1;
        player.isTrapped = false;

        if (hitTaken)
        {
            isGhost = true;
            StartCoroutine("GhostEnum");
            if (isGhost == true && healthAmount > 0)
            {
                Color playerColor = new Color(player.recolorSprites[0].color.r, player.recolorSprites[0].color.g, player.recolorSprites[0].color.b, 0);

                //player.ghost.SetActive(true);
                player.ghostSprites[0].color = new Color(1, 1, 1, 1);
                player.ghostSprites[1].color = new Color(1, 1, 1, 1);
                player.nonRecolorSprites[0].color = new Color(1, 1, 1, 0);
                player.nonRecolorSprites[1].color = new Color(1, 1, 1, 0);
                player.recolorSprites[0].color = playerColor;
                player.recolorSprites[1].color = playerColor;

                player.moveSpeed = 10;
                player.stabReady = false;
            }
        }
    }

    IEnumerator GhostEnum()
    {
        yield return new WaitForSeconds(ghostTime);
        isGhost = false;

        Color playerColor = new Color(player.recolorSprites[0].color.r, player.recolorSprites[0].color.g, player.recolorSprites[0].color.b, 1);

        player.ghostSprites[0].color = new Color(1, 1, 1, 0);
        player.ghostSprites[1].color = new Color(1, 1, 1, 0);
        player.nonRecolorSprites[0].color = new Color(1, 1, 1, 1);
        player.nonRecolorSprites[1].color = new Color(1, 1, 1, 1);
        player.recolorSprites[0].color = playerColor;
        player.recolorSprites[1].color = playerColor;
        player.stabReady = true;
        if (healthAmount > 0)
        {
            player.moveSpeed = 5;
        }
        if (healthAmount <= 0)
        {
            plMove.disableInput = true;
            player.moveSpeed = 0;
            
        }

    }

    private void CheckHealth()
    {
        if(photonView.IsMine && healthAmount <= 0)
        {
            GameManager.localInstance.EnableRespawn();
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
        GameManager.localInstance.victoryText.gameObject.SetActive(true);
        GameManager.localInstance.victoryText.text = "Player " + GetComponent<Player>().playerID + " vanquished!";
        deathFX.Play();
        //cc.enabled = false;
        //sr.enabled = false;
        //playerCanvas.SetActive(false);
    }

    [PunRPC]
    private void Respawn()
    {
        GameManager.localInstance.victoryText.gameObject.SetActive(false);
        GameManager.localInstance.ReloadScene();
        //cc.enabled = true;
        //sr.enabled = true;
        //playerCanvas.SetActive(true);
    }

    private void ModifyHealth(float amount)
    {
        if (photonView.IsMine)
        {
            healthAmount -= amount;
            lifeMarkers[Mathf.FloorToInt(healthAmount)].SetActive(false);
        }
        else
        {
            healthAmount -= amount;
        }
        CheckHealth();
    }
    
    
}
