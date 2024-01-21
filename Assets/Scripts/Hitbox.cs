using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{

    public Animator animator;
    private ZombieController zombieController;

    private void Start()
    {
        animator = GetComponentInParent<Animator>();
        zombieController = GetComponentInParent<ZombieController>();    
    }
    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Kolize");
        if (collision.gameObject.tag=="Bullet" && zombieController.isDead == false)
        {
            Destroy(collision.gameObject);
            zombieController.isDead = true;
            ParticleSystem[] systems = GetComponentsInChildren<ParticleSystem>();
            foreach (var item in systems)
            {
                item.Play();
            }
            animator.SetTrigger("Death");

        }
    }
}
