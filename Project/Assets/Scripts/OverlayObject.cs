using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class OverlayObject : MonoBehaviourPunCallbacks
{
    public float fadeDuration;

    private SpriteRenderer spriteRenderer;

    private IEnumerator currentCoroutine;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    IEnumerator FadeOut()
    {
        while(spriteRenderer.color.a > 0)
        {
            yield return null;
            spriteRenderer.color = new Color(1, 1, 1, spriteRenderer.color.a - (Time.deltaTime / fadeDuration));
        }
    }

    IEnumerator FadeIn()
    {
        while (spriteRenderer.color.a < 1)
        {
            yield return null;
            spriteRenderer.color = new Color(1, 1, 1, spriteRenderer.color.a + (Time.deltaTime / fadeDuration));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PhotonView target = collision.gameObject.GetComponent<PhotonView>();
        if (target != null)
        {
            if (target.tag == "Player" && target.IsMine)
            {
                if(currentCoroutine != null)
                {
                    StopCoroutine(currentCoroutine);
                }
                
                currentCoroutine = FadeOut();
                StartCoroutine(currentCoroutine);
            }
            //this.GetComponent<PhotonView>().RPC("DestroyObject", RpcTarget.AllBuffered);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        PhotonView target = collision.gameObject.GetComponent<PhotonView>();
        if (target != null)
        {
            if (target.tag == "Player" && target.IsMine)
            {
                if (currentCoroutine != null)
                {
                    StopCoroutine(currentCoroutine);
                }

                currentCoroutine = FadeIn();
                StartCoroutine(currentCoroutine);
            }
            //this.GetComponent<PhotonView>().RPC("DestroyObject", RpcTarget.AllBuffered);
        }
    }
}
