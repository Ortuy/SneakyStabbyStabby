using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Gas : MonoBehaviourPunCallbacks
{
    public Rigidbody2D rb;
    public GameObject gas;
    private Vector3 scaleChange;

    private void Awake()
    {
        scaleChange = new Vector3(-0.01f, -0.01f,0f);
    }
    void Update()
    {
        gas.transform.localScale += scaleChange;
        

        // Move upwards when the sphere hits the floor or downwards
        // when the sphere scale extends 1.0f.
        if (gas.transform.localScale.y < 0.1f || gas.transform.localScale.y > 1.0f)
        {
            scaleChange = -scaleChange;
            
        }
    }
}
