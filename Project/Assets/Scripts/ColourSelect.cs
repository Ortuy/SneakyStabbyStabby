using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class ColourSelect : InteractableObject
{
    //public Player player;

    //public void OnTriggerEnter2D(Collider2D collision)
    //{
    //    PhotonView target = collision.gameObject.GetComponent<PhotonView>();
    //    if (collision.tag == "Player")
    //    {
    //        player = collision.GetComponent<Player>();
    //        player.ColorSelect.SetActive(true);
    //    }

    //}
    //public void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.tag == "Player")
    //    {
    //        player = collision.GetComponent<Player>();
    //        player.ColorSelect.SetActive(false);
    //    }

    //}

    protected override void StartInteraction()
    {
        base.StartInteraction();
        targetPV.GetComponent<Player>().ColorSelect.SetActive(true);
        targetPV.GetComponent<Player>().ColorSelect.GetComponent<UIAnimator>().Show();
    }

    protected override void EndInteraction()
    {
        targetPV.GetComponent<Player>().ColorSelect.GetComponent<UIAnimator>().Hide();
        base.EndInteraction();
        
    }
}
