using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Footstep : MonoBehaviourPunCallbacks
{
    [PunRPC]
    public void SetFootstep(bool flip, float duration)
    {
        GetComponent<SpriteRenderer>().flipX = flip;
        Destroy(gameObject, duration);
    }
}
