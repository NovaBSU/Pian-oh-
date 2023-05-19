using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    public GameObject ATK;
    void Start()
    {
        Invoke("createNext", 0.1f);
    }

    void createNext()
    {
        Vector3 newPlace = transform.position;
        newPlace.y = newPlace.y - 0.15f;
        newPlace.x = newPlace.x + 0.15f;
        Instantiate(ATK, newPlace, Quaternion.identity, transform);
        Debug.Log("CreatedNext");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Player Damage Please.
        }
    }

}
