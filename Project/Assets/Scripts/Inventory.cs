using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Inventory : MonoBehaviourPunCallbacks
{
    public bool[] isFull;
    public GameObject[] slots;
}
