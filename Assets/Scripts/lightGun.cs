using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightGun : MonoBehaviour
{
    public Light lightTest2;
    public Animation lightAnimation2;

    void Start()
    {
        lightTest2 = GetComponent<Light>();
        lightAnimation2 = GetComponent<Animation>();
    }
    public void LightRig2()
    {
        lightTest2.range = 100f;
        lightAnimation2.Play();
    }
}
