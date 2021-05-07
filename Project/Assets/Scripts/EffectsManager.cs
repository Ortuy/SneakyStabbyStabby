using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsManager : MonoBehaviour
{
    public static EffectsManager instance;
    [SerializeField] private Animator fadeScreen;

    // Start is called before the first frame update
    void Start()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public void Fade()
    {
        //StopAllCoroutines();
        fadeScreen.Play("ScreenFade");
    }

    public void Unfade()
    {
        //StopAllCoroutines();
        fadeScreen.Play("ScreenUnfade");
    }

    public void FadeUnfade()
    {
        //StopAllCoroutines();
        fadeScreen.Play("ScreenFadeUnfade");
        //StartCoroutine(FadeAndUnfade());
    }

    //IEnumerator FadeAndUnfade()
    //{
    //    fadeScreen.Play("ScreenFadeUnfade");
    //    //yield return new WaitForSeconds(0.3f);
    //    //fadeScreen.Play("ScreenUnfade");
    //}
}
