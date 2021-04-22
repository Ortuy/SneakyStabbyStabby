using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class InteractableObject : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject interactionKeyIndicator;
    protected PhotonView targetPV;
    protected bool interactionOn;

    protected virtual void StartInteraction()
    {

    }

    protected virtual void EndInteraction()
    {

    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && other.GetComponent<PhotonView>().IsMine)
        {
            
            interactionKeyIndicator.SetActive(true);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        
        if (targetPV == null)
        {
            if (other.CompareTag("Player") && other.GetComponent<PhotonView>().IsMine)
            {
                targetPV = other.GetComponent<PhotonView>();
            }
        }
        else if (other.CompareTag("Player") && targetPV.IsMine)
        {
            
            if (Input.GetKeyDown(KeyCode.E) && !interactionOn)
            {
                interactionOn = true;
                StartInteraction();
                
            }

            if (Input.GetKeyDown(KeyCode.Escape) && interactionOn)
            {
                interactionOn = false;
                EndInteraction();
               
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.GetComponent<PhotonView>().IsMine)
        {
            interactionKeyIndicator.SetActive(false);
            if(targetPV == other.GetComponent<PhotonView>())
            {
                targetPV = null;
                EndInteraction();
                interactionOn = false;
            }
        }
    }
}
