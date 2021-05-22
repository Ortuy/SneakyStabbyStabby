using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PlayerStatusDisplay : MonoBehaviourPunCallbacks
{
    [SerializeField] private Image playerColourImage;
    [SerializeField] private Text playerNameText;
    [SerializeField] private GameObject[] heartMarkers;

    private Health myPlayerHealth;
    private bool needsRefreshing;

    public void InitDisplay(Health playerToTrack)
    {
        myPlayerHealth = playerToTrack;
        playerColourImage.color = myPlayerHealth.GetComponent<Player>().recolorSprites[0].color;
        playerNameText.text = myPlayerHealth.playerName;
    }

    IEnumerator TrackPlayer()
    {
        while(gameObject.activeInHierarchy && needsRefreshing)
        {
            Refresh();
            yield return new WaitForSeconds(1f);
        }
    }

    public void StartTracking()
    {
        StartCoroutine(TrackPlayer());
    }

    public void Refresh()
    {
        switch (myPlayerHealth.healthAmount)
        {
            case 3:
                heartMarkers[0].SetActive(true);
                heartMarkers[1].SetActive(true);
                heartMarkers[2].SetActive(true);
                break;
            case 2:
                heartMarkers[0].SetActive(true);
                heartMarkers[1].SetActive(true);
                heartMarkers[2].SetActive(false);
                break;
            case 1:
                heartMarkers[0].SetActive(true);
                heartMarkers[1].SetActive(false);
                heartMarkers[2].SetActive(false);
                break;
            case 0:
                heartMarkers[0].SetActive(false);
                heartMarkers[1].SetActive(false);
                heartMarkers[2].SetActive(false);
                needsRefreshing = false;
                break;
        }
    }
}
