using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Gas : MonoBehaviourPunCallbacks
{
    public Rigidbody2D rb;
    //public GameObject gas;
    private Vector3 scaleChange;
    public bool timerInhaleRunning = false;
    public float timeInhaleRemaining, timeInhaleBase;
    public float toxinDamage = 1;
    public bool ded = false;

    private Health playerInGas;


    [PunRPC]
    public void DestroyObject()
    {
        Destroy(this.gameObject);
    }

    private void Awake()
    {
        scaleChange = new Vector3(0.01f, 0.01f,0f);
        StartCoroutine("DestroyByTime");

    }
    void Update()
    {
        /*gas.*/transform.localScale += scaleChange;
        

        
        if (/*gas.*/transform.localScale.y > 1.0f)
        {
            scaleChange = new Vector3(0f, 0f, 0f);
            

        }
        
        if (timerInhaleRunning == true)
        {
            if (timeInhaleRemaining > 0)
            {               
                timeInhaleRemaining -= Time.deltaTime;

                if (playerInGas != null)
                {
                    playerInGas.breathMeter.SetValue(timeInhaleRemaining / timeInhaleBase);
                }

                ded = false;
            }
            else
            {
                playerInGas.breathMeter.gameObject.SetActive(false);
                timeInhaleRemaining = timeInhaleBase;
                timerInhaleRunning = false;
                ded = true;
                
            }
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PhotonView target = collision.gameObject.GetComponent<PhotonView>();
        if (target != null)
        {
            if (target.tag == "Player")
            {
                playerInGas = target.GetComponent<Health>();
                if(target.IsMine)
                {
                    playerInGas.breathMeter.gameObject.SetActive(true);
                }
                
                timerInhaleRunning = true;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        PhotonView target = collision.gameObject.GetComponent<PhotonView>();
        if (target != null)
        {
            if (target.tag == "Player")
            {
                
                //timerInhaleRunning = true;
                if(ded == true)
                {
                    AkSoundEngine.PostEvent("char_cough_gas", gameObject, gameObject);
                    target.RPC("ReduceHealth", RpcTarget.AllBuffered, toxinDamage);
                    timerInhaleRunning = false;
                    timeInhaleRemaining = timeInhaleBase;
                    ded = false;
                    StartCoroutine("IsGhost");
                }

                
            }

            
        }
        
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        PhotonView target = collision.gameObject.GetComponent<PhotonView>();
        if (target != null)
        {
            if (target.tag == "Player")
            {
                playerInGas.breathMeter.gameObject.SetActive(false);
                timerInhaleRunning = false;
                ded = false;
                timeInhaleRemaining = timeInhaleBase;
                StopCoroutine("IsGhost");


            }


        }


    }
    IEnumerator DestroyByTime()
    {
        yield return new WaitForSeconds(10f);
        photonView.RPC("DestroyObject", RpcTarget.AllBuffered);

    }
    IEnumerator IsGhost()
    {

        yield return new WaitForSeconds(3f);
        timerInhaleRunning = true;
        playerInGas.breathMeter.gameObject.SetActive(true);
    }

}
