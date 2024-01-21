using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Magazine magazine;
    public Transform fireingSpot;
    public GameObject bulletPrefab;
    public Transform aim;
    public Animator animator;
    public TMP_Text hints;
    // Start is called before the first frame update
    public void Start()
    {
        magazine = transform.GetComponentInChildren<Magazine>();
        animator = GameObject.Find("Player").GetComponent<Animator>();
        ParticleSystem[] systems = GetComponentsInChildren<ParticleSystem>();
        foreach (var item in systems)
        {
            item.Stop();
        }
    }
    public void Fire()
    {
        if (magazine != null)
        {
            Vystrel();
        }
        else
        {
            magazine = transform.GetComponentInChildren<Magazine>();
            if (magazine == null)
            {

            }
            else
            {
                Vystrel();
            }
        }
        
    }

    private void Vystrel()
    {
        if (magazine.ammo >= 1)
        {
            ParticleSystem[] systems = GetComponentsInChildren<ParticleSystem>();
            foreach (var item in systems)
            {
                item.Play();
            }
            animator.SetTrigger("Fire");
            magazine.ammo--;
            GameObject bullet = GameObject.Instantiate(bulletPrefab);
            bullet.transform.position = fireingSpot.position;

            // Use firing spot's forward vector as the bullet's initial direction
            bullet.transform.rotation = Quaternion.LookRotation(fireingSpot.forward);

            bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * 4000 * Time.deltaTime, ForceMode.Impulse);
            //Debug.Log("Fired");
        }
    }
}
