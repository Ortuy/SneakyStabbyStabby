using System.Collections;
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

    private PhotonView pView;
    private int decorID;
    private float decorScale;
    private bool decorGenerated = true;

    private GameObject newObj;

    // Start is called before the first frame update
    void Start()
    {
        pView = GetComponent<PhotonView>();
        //photonView.RPC("PlaceDecor", RpcTarget.AllBuffered);

        //Debug.Log(Physics2D.OverlapCircle(transform.position, 0.2f, LayerMask.GetMask("Decor")));
        /*
        if (pView.IsMine)
        {
            pView.RPC("PlaceDecor", RpcTarget.AllBuffered);

            var newObj = PhotonNetwork.Instantiate(decorPrefabs[decorID].name, transform.position, Quaternion.AngleAxis(Random.Range(0, 361), Vector3.forward));

            newObj.transform.parent = transform;
            newObj.transform.localScale = new Vector3(decorScale, decorScale, 1);
        }*/
        if(true)
        {
            pView.RPC("PlaceDecor", RpcTarget.AllBuffered);

            if (decorGenerated)
            {
                newObj = PhotonNetwork.InstantiateRoomObject(decorPrefabs[decorID].name, transform.position, Quaternion.AngleAxis(Random.Range(0, 361), Vector3.forward));

                if(newObj != null)
                {
                    //newObj.transform.parent = transform;
                    newObj.transform.localScale = new Vector3(decorScale, decorScale, 1);
                }
               
            }
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

            decorID = weightedDecorPool[Random.Range(0, weightedDecorPool.Count)];

            decorGenerated = true;

            decorScale = Random.Range(minScale, maxScale) * decorScaleModifiers[decorID];

           
        }
        else
        {
            decorGenerated = false;
        }

        //isDecorPlaced = true;
        //Destroy(gameObject);
    }
}
