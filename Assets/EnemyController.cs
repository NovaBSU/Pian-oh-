using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform player;
    private float movementSpeed = 4f;
    // Update is called once per frame
    void Update()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        Vector3 movement = direction * movementSpeed * Time.deltaTime;
        transform.Translate(movement);

        transform.LookAt(player);
    }
}
