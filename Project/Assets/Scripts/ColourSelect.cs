using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class ColourSelect : InteractableObject
{
    public bool[] colourLocked;

    public PhotonView publicPhotonView;



    private void Awake()
    {
        publicPhotonView = photonView;
        FindLockedColours();
    }

    public void FindLockedColours()
    {
        var players = FindObjectsOfType<Player>();

        foreach(Player player in players)
        {
            if(!player.photonView.IsMine)
            {
                colourLocked[player.currentColourID] = true;
            }
        }
    }

    [PunRPC]
    public void LockColour(int colour)
    {
        colourLocked[colour] = true;        
    }

    [PunRPC]
    public void UnlockColour(int colour)
    {
        colourLocked[colour] = false;
    }

    protected override void StartInteraction()
    {
        base.StartInteraction();

        var playertemp = targetPV.GetComponent<Player>();

        playertemp.ColorSelect.SetActive(true);
        playertemp.RefreshColourDisplays();
        playertemp.ShowGuildDescription(playertemp.currentColourID);
        playertemp.ColorSelect.GetComponent<UIAnimator>().Show();
    }

    protected override void EndInteraction()
    {
        targetPV.GetComponent<Player>().ColorSelect.GetComponent<UIAnimator>().Hide();
        base.EndInteraction();
        
    }
}
