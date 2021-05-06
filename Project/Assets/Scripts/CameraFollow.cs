using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MilkShake;

public class CameraFollow : MonoBehaviour
{
    public GameObject target;

    [Range(0f, 1f)]
    public float interest;

    [SerializeField] private Shaker shaker;
    [SerializeField] private ShakePreset[] shakePresets;

    private void FixedUpdate()
    {
        var temp = Vector3.MoveTowards(transform.position, target.transform.position, interest);
        transform.position = new Vector3(temp.x, temp.y, transform.position.z);

        if(Input.GetKeyDown(KeyCode.L))
        {
            ShakeCamera(0);
        }
    }

    public void ShakeCamera(int presetID)
    {
        shaker.Shake(shakePresets[presetID]);
    }
}
