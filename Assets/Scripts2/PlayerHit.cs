using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    public GameObject ATK;
    public GameObject player;
    private PlayerHit parHit;
    private EnemyController parCtrl;
    public Vector3 newPlace;
    public int repeat = 0;
    void Start()
    {
        Invoke("createNext", 0.1f);
        player = GameObject.FindWithTag("Player");
        parCtrl = transform.parent.GetComponent<EnemyController>();
        if(parHit != null)
        {
            repeat = parCtrl.repeat;
            parCtrl.rpt();
        }
    }

    void createNext()
    {
        Vector3 newPlace = transform.position;
        newPlace.y = newPlace.y - 0.15f;
        newPlace.z = newPlace.z - 0.15f;
        newPlace.x = newPlace.x + 0.09f;
        Instantiate(ATK, newPlace, Quaternion.identity);
        ATK.transform.SetParent(transform.parent);
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
