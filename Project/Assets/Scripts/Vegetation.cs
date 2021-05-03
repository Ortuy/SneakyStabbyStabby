using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Vegetation : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private ParticleSystem particles;
    [SerializeField] private bool moveParticlesToCollision;

    private bool animationPlaying;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!animationPlaying && !collision.CompareTag("ViewCone"))
        {
            animator.SetTrigger("Rustle");
            if(moveParticlesToCollision)
            {
                particles.transform.position = collision.transform.position;
            }
            particles.Play();
            animationPlaying = true;
        }
        
    }

    public void MarkAnimationEnd()
    {
        animationPlaying = false;
    }
}
