﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Decorator : MonoBehaviourPunCallbacks
{
    public bool isDecorPlaced;

    public GameObject[] decorPrefabs;
    public int[] decorWeights;
    public float[] decorScaleModifiers;

    public float maxScale, minScale;

    public int blankPercentChance;

    // Start is called before the first frame update
    void Start()
    {
        //photonView.RPC("PlaceDecor", RpcTarget.AllBuffered);

        Debug.Log(Physics2D.OverlapCircle(transform.position, 0.2f, LayerMask.GetMask("Decor")));

        if (photonView.IsMine && !Physics2D.OverlapCircle(transform.position, 0.2f, LayerMask.GetMask("Decor")))
        {
            photonView.RPC("PlaceDecor", RpcTarget.AllBuffered);
        }
        
    }

    [PunRPC]
    private void PlaceDecor()
    {
        //Debug.Log("AAAAAAA");
        if(Random.Range(0, 100) > blankPercentChance)
        {
            List<int> weightedDecorPool = new List<int>();

            for (int i = 0; i < decorPrefabs.Length; i++)
            {
                for (int j = 0; j < decorWeights[i]; j++)
                {
                    weightedDecorPool.Add(i);
                }
            }

            int decorID = weightedDecorPool[Random.Range(0, weightedDecorPool.Count)];

            var newObj = PhotonNetwork.Instantiate(decorPrefabs[decorID].name, transform.position, Quaternion.AngleAxis(Random.Range(0, 361), Vector3.forward));

            newObj.transform.parent = transform;

            var decorScale = Random.Range(minScale, maxScale) * decorScaleModifiers[decorID];
            newObj.transform.localScale = new Vector3(decorScale, decorScale, 1);
        }

        isDecorPlaced = true;
        //Destroy(gameObject);
    }
}
