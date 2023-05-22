using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunController : MonoBehaviour
{
    public lightBehaviour lightBehaviourComponent;
    public lightGun lightGun;
    public GameObject lightBurstGun;

    float FireRate = 0.5f;
    private float NextFire;

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
            lightGun.LightRig2();
        }
    }
}
