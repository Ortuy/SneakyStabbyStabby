using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject target;

    [Range(0f, 1f)]
    public float interest;

    private void FixedUpdate()
    {
        var temp = Vector3.MoveTowards(transform.position, target.transform.position, interest);
        transform.position = new Vector3(temp.x, temp.y, transform.position.z);
    }
}
