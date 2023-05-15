using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestructLightBurst : MonoBehaviour
{
    void selfDestruct()
    {
        Destroy(gameObject);
    }
    void Start()
    {
        Invoke("selfDestruct", 0.5f);
    }

}
