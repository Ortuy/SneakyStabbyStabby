using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class UIAnimator : MonoBehaviour
{
    public Animator Anim { get; private set; }

    private void Awake()
    {
        Anim = GetComponent<Animator>();
    }

    public void Show()
    {
        Anim.Play("PanelShow");
    }

    

    public void Hide()
    {
        Anim.Play("PanelHide");
    }

    public void DisableObject()
    {
        gameObject.SetActive(false);
    }
}
