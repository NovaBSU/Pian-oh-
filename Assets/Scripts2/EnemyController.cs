using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public GameObject player;
    public GameObject MeleeStrike;
    public Light enemyLight;
    private CapsuleCollider enemyCollider;
    private float movementSpeed = 4f;
    private float cPos;
    private float lPos;
    private bool grounded = false;
    public int repeat = 0;
    void FixedUpdate()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;
        Vector3 movement = direction * movementSpeed * Time.fixedDeltaTime;
        GetComponent<Rigidbody>().MovePosition(transform.position + direction * movementSpeed * Time.fixedDeltaTime);


        var lookPos = player.transform.position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.fixedDeltaTime * movementSpeed);

        Vector3 MeleeStrikeRelocate;
        MeleeStrikeRelocate = transform.position;
        MeleeStrikeRelocate.y = MeleeStrikeRelocate.y + 2.4f;
        MeleeStrikeRelocate.z = MeleeStrikeRelocate.z + 0.8f;
        MeleeStrikeRelocate.x = MeleeStrikeRelocate.x + 0.6f;
        MeleeStrike.transform.position = MeleeStrikeRelocate;
    }
    public void rpt()
    {
        repeat = repeat + 1;
    }
    void LightBehaviour()
    {
        lPos = cPos;
        cPos = transform.position.x + transform.position.y + transform.position.z;

        // This is a stupid fix to make C# do math.
        // It utterly refuses to use overloads on Round
        // The "Easy" solution was supposed to be
        // cPos = Mathf.Round(cPos, 2);
        // But C# absolutely hated that, so we ended up with this awfulness.
        // I am so sorry Gorg.

        cPos = cPos * 100;
        cPos = Mathf.Round(cPos);
        cPos = cPos / 100;

        // By running this check *before* anything happens with the lights
        // we *should* be able to create a clean transition for enemies that
        // are no longer on the ground.

        // the isGrounded check doesn't work atm. Fix later. Ooga Booga.

        if (grounded == true)
        {
            Debug.Log("Grounded");
            if (lPos - cPos != 0)
            {
                // Left these here for later. Dont want to clutter logs.
                //Debug.Log("lPos: " + lPos);
                //Debug.Log("cPos: " + cPos);

                // I feel like there is a waaaaay better way of doing this. But meh, smeh, eh. It works.
                InvokeRepeating("enemyLightON", 0f, 0.25f);
                InvokeRepeating("enemyLightOFF", 0.125f, 0.25f);
            }
            else
            {
                CancelInvoke("enemyLightON");
                Invoke("cancelLightSmooth", 0.5f);
                enemyLight.range = 0f;
            }
        }
        else
        {
            // Debug Logs to see *when* it becomes ungrounded.
            Debug.Log("Ungrounded");

            CancelInvoke("enemyLightON");
            Invoke("cancelLightSmooth", 0.5f);
        }
    }
    private void enemyLightHALF()
    {
        enemyLight.range = 6.25f;
    }
    private void enemyLightON()
    {
        enemyLight.range = 12.5f;
        Invoke("enemyLightHALF", 0.1f);
    }
    private void enemyLightOFF()
    {
        enemyLight.range = 0f;
    }
    private void cancelLightSmooth()
    {
        CancelInvoke("enemyLightOff");
    }

    // Function to make it attack players.
    private void OnTriggerEnter(Collider other)
    {
        // While this looks stupid, it gives the player 0.3s between coming into melee attack range and actually being hit.
        if (other.CompareTag("Player"))
        {
            Invoke("createAttack", 0.3f);
        }

        // This feels horrifically inefficient, but, I'm beyond caring.
        if (other.CompareTag("Floor"))
        {
            grounded = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Floor"))
        {
            grounded = false;
        }
    }

    private void createAttack()
    {
        Vector3 newPlace = transform.position;
        newPlace.y = newPlace.y + 2.4f;
        newPlace.z = newPlace.z + 0.8f;
        newPlace.x = newPlace.x + 0.6f;
        Instantiate(MeleeStrike, newPlace, Quaternion.identity);
        MeleeStrike.transform.SetParent(transform);
    }
    void Start()
    {
        InvokeRepeating("LightBehaviour", 0f, 0.25f);
        enemyCollider = GetComponent<CapsuleCollider>();
        enemyLight.range = 0f;
        player = GameObject.FindWithTag("Player");
    }
}