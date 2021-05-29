using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class Radar : InteractableObject
{
    [SerializeField] private GameObject cooldownDisplay;
    [SerializeField] private Text cooldownText;

    private Player currentPlayer;
    private bool isAvaliable = true;

    [SerializeField] private int refreshTime;

    protected override void StartInteraction()
    {
        
        AkSoundEngine.PostEvent("sfx_obj_radar_use", gameObject, gameObject);
        if (isAvaliable)
        {
            base.StartInteraction();
            currentPlayer = targetPV.GetComponent<Player>();

            if (!GameManager.localInstance.mapOut)
            {
                GameManager.localInstance.ToggleMap();
            }

            var playersTemp = FindObjectsOfType<Player>();

            foreach (Player player in playersTemp)
            {
                player.mapIconRadar.SetActive(true);
            }

            photonView.RPC("StartCooldown", RpcTarget.AllBuffered);
        }
        else
        {
            interactionOn = false;
        }        
    }

    protected override void EndInteraction()
    {
        base.EndInteraction();
        if (GameManager.localInstance.mapOut)
        {
            GameManager.localInstance.ToggleMap();
        }

        var playersTemp = FindObjectsOfType<Player>();

        foreach (Player player in playersTemp)
        {
            if(player != currentPlayer && !player.isByDetector)
            {
                player.mapIconRadar.SetActive(false);
            }
            
        }

        
    }

    [PunRPC]
    private void StartCooldown()
    {
        Debug.Log("EndRadar");
        StartCoroutine(DoCooldown());
    }
    
    IEnumerator DoCooldown()
    {
        cooldownDisplay.SetActive(true);
        isAvaliable = false;

        int timer = refreshTime;
        while(timer > 0)
        {
            cooldownText.text = timer.ToString();
            yield return new WaitForSeconds(1f);
            timer--;
        }

        cooldownDisplay.SetActive(false);
        isAvaliable = true;
    }
}
