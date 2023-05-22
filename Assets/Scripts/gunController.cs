using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunController : MonoBehaviour
{
    public lightBehaviour lightBehaviourComponent;
    public lightGun lightGun;
    public GameObject lightBurstGun;
    public AudioSource gunSound;

    float FireRate = 1.125f;
    private float NextFire;

    void Start()
    {
        gunSound = GetComponent<AudioSource>();
    }

    void Update()
    {
        if(Input.GetButton("Fire1") && Time.time > NextFire)
        {
            ShootGun();
            NextFire = Time.time + FireRate;
        }
    }

    void ShootGun()
    {
        RaycastHit Hit;
        if (Physics.Raycast(transform.position, transform.forward, out Hit, Mathf.Infinity))
        {
            if (Hit.transform.tag == "Enemy")
            {
                Destroy(Hit.transform.gameObject);
            }
            lightGun.LightRig2();
            gunSound.Play();
        }
    }
}
