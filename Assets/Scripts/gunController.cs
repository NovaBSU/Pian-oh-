using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButton("Fire1"))
        {
            ShootGun();
        }
    }

    void ShootGun()
    {
        RaycastHit Hit;
        if(Physics.Raycast(transform.position, transform.forward, out Hit, Mathf.Infinity))
        {
            Debug.Log("Hit");
        }
        else
        {
            Debug.Log("Dont Hit");
        }
    }
}
