using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Health : MonoBehaviourPunCallbacks
{
    public string playerName;

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

    public UIAnimator[] lifeMarkers;
    public ParticleSystem deathFX, hurtFX;

    public RadialProgressBar breathMeter;

    private void Awake()
    {
        if (photonView.IsMine)
        {
            GameManager.localInstance.localPlayer = this.gameObject;
            //playerName = PlayerPrefs.GetString("playername");
            photonView.RPC("SyncName", RpcTarget.AllBuffered, PlayerPrefs.GetString("playername"));
        }
    }

    [PunRPC]
    private void SyncName(string nameToSync)
    {
        playerName = nameToSync;
    }

    [PunRPC]
    public void ReduceHealth(float amount)
    {
        
        if (isGhost == false)
        {
            AkSoundEngine.PostEvent("char_taking_damage", gameObject, gameObject);
            hurtFX.Play();
            //StopAllCoroutines();
            StartCoroutine(StabHitStop(true, 0.5f));
            ModifyHealth(amount);
            
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

    [PunRPC]
    public void HitStop(bool damageTaken)
    {
        
        StartCoroutine(StabHitStop(damageTaken, 0.5f));
    }

    IEnumerator StabHitStop(bool hitTaken, float duration)
    {
        if(!hitTaken && !photonView.IsMine)
        {
            yield return new WaitForSeconds(0.1f);
        }
        
        if(hitTaken)
        {
            isGhost = true;
        }

        Debug.LogFormat(hitTaken + " hit stop");
        cFollow.ShakeCamera(0);
        player.torsoAnimator.speed = 0;
        player.legsAnimator.speed = 0;
        player.isTrapped = true;

        yield return new WaitForSeconds(duration);

        player.torsoAnimator.speed = 1;
        player.legsAnimator.speed = 1;
        player.isTrapped = false;

        if (hitTaken & healthAmount > 0)
        {
            isGhost = true;
            player.ChangeTagGhost();
            player.isAliveGhostPlayer = true;
            StartCoroutine("GhostEnum");
            if (isGhost == true & healthAmount > 0)
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
        player.ChangeTagPlayer();
        player.isAliveGhostPlayer = false;
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
        if (photonView.IsMine & healthAmount <= 0)
        {
            //GameManager.localInstance.EnableRespawn();
            //plMove.disableInput = true;
            //plMove.moveSpeed = 0;
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
        player.ChangeTagGhost();
        player.isGhostPlayer = true;
        GameManager.localInstance.victoryText.gameObject.SetActive(true);
        GameManager.localInstance.victoryText.text = playerName + " is dead!";
        GameManager.localInstance.secondaryText.text = "";
        GameManager.localInstance.DisappearText(3f);
        GameManager.localInstance.numerOfPlayers++;
        GameManager.localInstance.MapWin();
        
        AkSoundEngine.PostEvent("char_voice_death", gameObject, gameObject);
        deathFX.Play();
        isGhost = true;
        Color playerColor = new Color(player.recolorSprites[0].color.r, player.recolorSprites[0].color.g, player.recolorSprites[0].color.b, 0);

        //player.ghost.SetActive(true);
        player.ghostSprites[0].color = new Color(1, 0, 0, 1);
        player.ghostSprites[1].color = new Color(1, 0, 0, 1);
        player.nonRecolorSprites[0].color = new Color(1, 1, 1, 0);
        player.nonRecolorSprites[1].color = new Color(1, 1, 1, 0);
        player.recolorSprites[0].color = playerColor;
        player.recolorSprites[1].color = playerColor;

        player.moveSpeed = 10;
        player.stabReady = false;
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
            if(healthAmount >= 0)
            {
                lifeMarkers[Mathf.FloorToInt(healthAmount)].Hide();
            }
            
        }
        else
        {
            healthAmount -= amount;
        }
        CheckHealth();
    }


}
