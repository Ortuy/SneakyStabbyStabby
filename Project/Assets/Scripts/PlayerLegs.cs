using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLegs : MonoBehaviour
{
    public void PlayFootstep()
    {
        transform.GetComponentInParent<Player>().PlayFootstep();
    }
}
