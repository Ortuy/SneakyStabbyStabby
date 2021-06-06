using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RotationFollower : MonoBehaviour
{
    private Transform followTarget;

    private bool following;

    [SerializeField]
    private float range, speed, checkFrequency, offset;

    [SerializeField]
    private bool canFollow;

    [SerializeField]
    private bool isShop;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CheckForTarget());
    }

    private void Update()
    {
        if(following)
        {
            Vector2 axis = followTarget.position - transform.position;
            var angle = Mathf.Atan2(axis.y, axis.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.AngleAxis(angle + offset, Vector3.forward);
        }
        else
        {
            transform.rotation = Quaternion.AngleAxis(0, Vector3.forward);
        }
    }

    IEnumerator CheckForTarget()
    {
        while(true)
        {
            yield return new WaitForSeconds(checkFrequency);
            if(canFollow)
            {
                var temp = Physics2D.OverlapCircle(transform.position, range, LayerMask.GetMask("MeleeHurtable"));
                if (temp != null && temp.CompareTag("Player"))
                {
                    if(isShop)
                    {
                        PlayShopkeeperSounds();
                    }

                    following = true;
                    followTarget = temp.transform;
                }
                else
                {
                    following = false;
                }
            }
        }
    }

    private void PlayShopkeeperSounds()
    {
        //Sounds here!
    }
}
