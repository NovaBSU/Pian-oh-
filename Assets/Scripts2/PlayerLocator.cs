using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocator : MonoBehaviour
{
    public Transform player;
    public Vector3 playerPst;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerPst = player.position;
    }
}
