using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class DecorHolder : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject[] decorPrefabs;

    private void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            var decorators = GetComponentsInChildren<Decorator>();

            foreach (Decorator decorator in decorators)
            {
                decorator.ChooseDecor();
                photonView.RPC("SyncAndPlaceDecor", RpcTarget.AllBuffered, decorator.decorID, decorator.decorGenerated, decorator.decorScale,
                    decorator.decorRotation, decorator.transform.position.x, decorator.transform.position.y, decorator.decoratorTypeOffset);

            }
        }
    }

    [PunRPC]
    private void SyncAndPlaceDecor(int decorID, bool decorGenerated, float decorScale, float decorRotation, float posX, float posY, int prefabOffset)
    {
        //var decorID = dID;
        //var decorGenerated = isPlaced;
        //var decorScale = dScale;
        //var decorRotation = dRot;
        if (decorGenerated)
        {
            //newObj = PhotonNetwork.InstantiateRoomObject(decorPrefabs[decorID].name, transform.position, Quaternion.AngleAxis(Random.Range(0, 361), Vector3.forward));
            var newObj = Instantiate(decorPrefabs[decorID + prefabOffset], new Vector3(posX, posY, 0), Quaternion.AngleAxis(decorRotation, Vector3.forward));

            if (newObj != null)
            {
                //newObj.transform.parent = transform;
                Debug.Log("Generated: " + newObj.name + " at x(" + posX + "), y(" + posY + ")");
                newObj.transform.localScale = new Vector3(decorScale, decorScale, 1);
            }

        }
    }
}
