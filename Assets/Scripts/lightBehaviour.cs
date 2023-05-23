using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightBehaviour : MonoBehaviour
{
    public delegate void LightType(int num);
    public LightType lightParameter;
    public Animation lightAnimation;

    public Light lightTest;

    void Start()
    {
        lightParameter += LightRig;

        lightTest = GetComponent<Light>();
        lightAnimation = GetComponent<Animation>();
    }

    void LightRig(int num)
    {
        switch(num)
        {
            case 0:
                lightTest.range = 7.5f;
                break;
            case 1:
                lightTest.range = 15f;
                break;
            case 2:
                lightTest.range = 30f;
                break;
            case 3:
                lightTest.range = 100f;
                break;
        }
        lightAnimation.Play();
    }
}
