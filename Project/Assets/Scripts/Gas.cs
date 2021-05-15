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
    public float timeInhelRemaining;
    public float toxinDamage = 1;
    public bool ded = false;
    

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
            if (timeInhelRemaining > 0)
            {
                timeInhelRemaining -= Time.deltaTime;
                ded = false;
            }
            else
            {

                timeInhelRemaining = 5;
                timerInhaleRunning = false;
                ded = true;
                
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
                timerInhaleRunning = true;
                if(ded == true)
                {
                    AkSoundEngine.PostEvent("char_cough_gas", gameObject, gameObject);
                    target.RPC("ReduceHealth", RpcTarget.AllBuffered, toxinDamage);
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
                timerInhaleRunning = false;
                ded = false;
                timeInhelRemaining = 5;



            }


        }


    }
    IEnumerator DestroyByTime()
    {
        yield return new WaitForSeconds(10f);
        photonView.RPC("DestroyObject", RpcTarget.AllBuffered);

    }

}
