using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class OverlayObject : MonoBehaviourPunCallbacks
{
    public float fadeDuration;

    private SpriteRenderer spriteRenderer;

    private IEnumerator currentCoroutine;
    private List<IEnumerator> currentExtraCoroutines;

    [SerializeField] private float fadeOutLevel = 0;
    [SerializeField] private bool potionOnly;

    [SerializeField] private List<SpriteRenderer> extraSprites;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    IEnumerator FadeOut()
    {
        while(spriteRenderer.color.a > fadeOutLevel)
        {
            yield return null;
            spriteRenderer.color = new Color(1, 1, 1, spriteRenderer.color.a - (Time.deltaTime / fadeDuration));            
        }
        spriteRenderer.color = new Color(1, 1, 1, fadeOutLevel);
    }

    IEnumerator FadeOutExtra(SpriteRenderer extraRenderer)
    {
        while (extraRenderer.color.a > fadeOutLevel)
        {
            yield return null;
            extraRenderer.color = new Color(extraRenderer.color.r, extraRenderer.color.g, extraRenderer.color.b, extraRenderer.color.a - (Time.deltaTime / fadeDuration));
        }
        extraRenderer.color = new Color(extraRenderer.color.r, extraRenderer.color.g, extraRenderer.color.b, fadeOutLevel);
    }


    IEnumerator FadeIn()
    {
        while (spriteRenderer.color.a < 1)
        {
            yield return null;
            spriteRenderer.color = new Color(1, 1, 1, spriteRenderer.color.a + (Time.deltaTime / fadeDuration));
        }
    }

    IEnumerator FadeInExtra(SpriteRenderer extraRenderer)
    {
        while (extraRenderer.color.a < 1)
        {
            yield return null;
            extraRenderer.color = new Color(extraRenderer.color.r, extraRenderer.color.g, extraRenderer.color.b, extraRenderer.color.a + (Time.deltaTime / fadeDuration));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PhotonView target = collision.gameObject.GetComponent<PhotonView>();
        if (target != null && !potionOnly)
        {
            if (target.tag == "Player" && target.IsMine)
            {
                StopAllCoroutines();
                
                currentCoroutine = FadeOut();
                StartCoroutine(currentCoroutine);

                if(extraSprites != null)
                {
                    foreach(SpriteRenderer renderer in extraSprites)
                    {
                        StartCoroutine(FadeOutExtra(renderer));
                    }
                }
            }
            //this.GetComponent<PhotonView>().RPC("DestroyObject", RpcTarget.AllBuffered);
        }
        else if (potionOnly && collision.CompareTag("ViewCone"))
        {
            target = collision.transform.parent.parent.GetComponent<PhotonView>();
            if (target.tag == "Player" && target.IsMine)
            {
                StopAllCoroutines();

                currentCoroutine = FadeOut();
                StartCoroutine(currentCoroutine);

                if (extraSprites != null)
                {
                    foreach (SpriteRenderer renderer in extraSprites)
                    {
                        StartCoroutine(FadeOutExtra(renderer));
                    }
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        PhotonView target = collision.gameObject.GetComponent<PhotonView>();
        if (target != null && !potionOnly)
        {
            if (target.tag == "Player" && target.IsMine)
            {
                StopAllCoroutines();

                currentCoroutine = FadeIn();
                StartCoroutine(currentCoroutine);

                if (extraSprites != null)
                {
                    foreach (SpriteRenderer renderer in extraSprites)
                    {
                        StartCoroutine(FadeInExtra(renderer));
                    }
                }
            }
            //this.GetComponent<PhotonView>().RPC("DestroyObject", RpcTarget.AllBuffered);
        }
        else if (potionOnly && collision.CompareTag("ViewCone"))
        {
            target = collision.transform.parent.parent.GetComponent<PhotonView>();
            if (target.tag == "Player" && target.IsMine)
            {
                StopAllCoroutines();

                currentCoroutine = FadeIn();
                StartCoroutine(currentCoroutine);

                if (extraSprites != null)
                {
                    foreach (SpriteRenderer renderer in extraSprites)
                    {
                        StartCoroutine(FadeInExtra(renderer));
                    }
                }
            }
        }
    }
}
