using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform player;

    private float movementSpeed = 3f;

    void FixedUpdate()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        GetComponent<Rigidbody>().MovePosition(transform.position + direction * movementSpeed * Time.fixedDeltaTime);

        var lookPos = player.position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * movementSpeed);
    }
}
