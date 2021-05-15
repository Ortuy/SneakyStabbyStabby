using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum VegetationType { NONE, PLANT_BIG, PLANT_SMALL, SHROOMS, PUDDLE, BIRDS }

[RequireComponent(typeof(Animator))]
public class Vegetation : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private ParticleSystem particles;
    [SerializeField] private bool moveParticlesToCollision;
    [SerializeField] private VegetationType vegetationType;

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
            PlayVegetationSound(particles);
            particles.Play();
            animationPlaying = true;
        }
        
    }

    public void MarkAnimationEnd()
    {
        animationPlaying = false;
    }

    public void PlayVegetationSound(ParticleSystem particles)
    {
        //if (particles.ToString().Contains("Spores"))
        //    AkSoundEngine.PostEvent("amb_schroom_enter", gameObject, gameObject);
        //else if (particles.ToString().Contains("PickupParticle"))
        //    AkSoundEngine.PostEvent("amb_bush_rustle_enter", gameObject, gameObject);
        switch(vegetationType)
        {
            case VegetationType.PLANT_BIG:
                AkSoundEngine.PostEvent("amb_bush_rustle_enter", gameObject, gameObject);
                break;
            case VegetationType.PLANT_SMALL:
                //Put code to play the small plant rustle sound here!
                break;
            case VegetationType.SHROOMS:
                AkSoundEngine.PostEvent("amb_schroom_enter", gameObject, gameObject);
                break;
            case VegetationType.PUDDLE:
                //Put code to play the puddle splash sound here!
                break;
            case VegetationType.BIRDS:
                //Put code to play the flying birds sound here!
                break;
        }
    }
}
