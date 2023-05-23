using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestructMeleeStrike : MonoBehaviour
{
    public void selfDestruct()
    {
        Destroy(gameObject);
    }
    void Start()
    {
        Invoke("selfDestruct", 1f);
    }

}
