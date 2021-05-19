using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Decorator : MonoBehaviourPunCallbacks
{
    public int decoratorTypeOffset;

    public bool isDecorPlaced;

    public GameObject[] decorPrefabs;
    public int[] decorWeights;
    public float[] decorScaleModifiers;

    public float maxScale, minScale;

    public int blankPercentChance;

    //private PhotonView pView;
    public int decorID;
    public float decorScale;
    public bool decorGenerated = true;
    public float decorRotation;
    
    public void ChooseDecor()
    {
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

            decorID = weightedDecorPool[Random.Range(0, weightedDecorPool.Count)];

            decorGenerated = true;

            decorScale = Random.Range(minScale, maxScale) * decorScaleModifiers[decorID];

            decorRotation = Random.Range(0, 361);
        }
        else
        {
            decorGenerated = false;

            decorScale = Random.Range(minScale, maxScale) * decorScaleModifiers[decorID];

            decorRotation = Random.Range(0, 361);
        }
    }
}
