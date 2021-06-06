using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class InteractableObject : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject interactionKeyIndicator;
    protected PhotonView targetPV;
    protected Player targetPlayer;
    public bool interactionOn;
    protected bool playerInRange;

    protected virtual void StartInteraction()
    {

    }

    public virtual void EndInteraction()
    {

    }

    //Run this in the update method
    protected void CheckForInput()
    {
        if(playerInRange && targetPV.IsMine)
        {
            if (Input.GetKeyDown(KeyCode.E) && !interactionOn)
            {
                targetPlayer.isInteracting = true;
                interactionOn = true;
                StartInteraction();

            }

            if (Input.GetKeyDown(KeyCode.Escape) && interactionOn)
            {
                Debug.Log("InteractionCancel");
                //other.GetComponent<Player>().isInteracting = false;
                StartCoroutine(ClearPlayerInteractionStatus(targetPlayer));
                interactionOn = false;
                EndInteraction();

            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && other.GetComponent<PhotonView>().IsMine)
        {            
            interactionKeyIndicator.SetActive(true);
            targetPV = other.GetComponent<PhotonView>();
            targetPlayer = other.GetComponent<Player>();
            targetPlayer.nearbyInteractable = this;
            playerInRange = true;
        }
    }

    //private void OnTriggerStay2D(Collider2D other)
    //{       
    //    if (targetPV == null)
    //    {
    //        if (other.CompareTag("Player") && other.GetComponent<PhotonView>().IsMine)
    //        {
    //            targetPV = other.GetComponent<PhotonView>();
    //        }
    //    }
    //    else if (other.CompareTag("Player") && targetPV.IsMine)
    //    {
    //        Debug.Log("AAAAAAAAAAAAAAAA");
    //        if (Input.GetKeyDown(KeyCode.E) && !interactionOn)
    //        {
    //            other.GetComponent<Player>().isInteracting = true;
    //            interactionOn = true;
    //            StartInteraction();
                
    //        }

    //        if (Input.GetKeyDown(KeyCode.Escape) && interactionOn)
    //        {
    //            Debug.Log("InteractionCancel");
    //            //other.GetComponent<Player>().isInteracting = false;
    //            StartCoroutine(ClearPlayerInteractionStatus(other.GetComponent<Player>()));
    //            interactionOn = false;
    //            EndInteraction();
               
    //        }
    //    }
    //}

    IEnumerator ClearPlayerInteractionStatus(Player player)
    {
        yield return null;
        player.isInteracting = false;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.GetComponent<PhotonView>().IsMine)
        {
            other.GetComponent<Player>().isInteracting = false;
            interactionKeyIndicator.SetActive(false);
            if(targetPV == other.GetComponent<PhotonView>())
            {
                EndInteraction();
                targetPlayer.nearbyInteractable = null;
                targetPV = null;
                targetPlayer = null;
                interactionOn = false;
                playerInRange = false;
            }
        }
    }
}
