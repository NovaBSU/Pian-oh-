using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    public GameObject ATK;
    public GameObject player;
    void Start()
    {
        Invoke("createNext", 0.1f);

        
    }

    void createNext()
    {
        Vector3 newPlace = transform.position;
        newPlace.y = newPlace.y - 0.15f;
        newPlace.z = newPlace.z - 0.15f;
        newPlace.x = newPlace.x + 0.09f;
        Instantiate(ATK, newPlace, Quaternion.identity, transform);
        Debug.Log("CreatedNext");
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SelfDestructMeleeStrike vantaBlast = GetComponentInParent<SelfDestructMeleeStrike>();
            vantaBlast.selfDestruct();

            Debug.Log("PLAYER HIT");
            CancelInvoke("createNext");
            player.GetComponent<playerController>().damage();
        }
    }

}
