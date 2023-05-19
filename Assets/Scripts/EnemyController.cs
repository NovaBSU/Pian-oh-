using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public Transform player;
    public GameObject MeleeStrike;
    public Light enemyLight;
    private CapsuleCollider enemyCollider;
    private float movementSpeed = 4f;
    private float cPos;
    private float lPos;

    void FixedUpdate()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        Vector3 movement = direction * movementSpeed * Time.fixedDeltaTime;
       // GetComponent<Rigidbody>().MovePosition(transform.position + direction * movementSpeed * Time.fixedDeltaTime);


        var lookPos = player.position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.fixedDeltaTime * movementSpeed);

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

        bool isGrounded = CheckGrounded();

        // By running this check *before* anything happens with the lights
        // we *should* be able to create a clean transition for enemies that
        // are no longer on the ground.

        // the isGrounded check doesn't work atm. Fix later. Ooga Booga.

        //if (isGrounded == true) <-- This is code and should be uncommented later
        //{ <-- This is code and should be uncommented later
            Debug.Log("Grounded");
            if (lPos - cPos != 0)
            {
                // Left these here for later.
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
        //} <-- This is code and should be uncommented later
        //else <-- This is code and should be uncommented later
        //{ <-- This is code and should be uncommented later
            // Debug Logs to see *when* it becomes ungrounded.
            Debug.Log("Ungrounded");

            CancelInvoke("enemyLightON");
            Invoke("cancelLightSmooth", 0.5f);
        //}
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
    private bool CheckGrounded()
    {
        // This does not work at all. I do not know where the evereloving &*$^ the ray is actually being cast to.

        Ray groundRay = new Ray(transform.position, transform.TransformDirection(Vector3.down));
        RaycastHit groundHit;

        bool isGrounded = Physics.Raycast(groundRay, out groundHit, 1f);
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * 10f, Color.red);

        return isGrounded;
    }

    private void OnTriggerEnter(Collider other)
    {
        // While this looks stupid, it gives the player 0.3s between coming into melee attack range and actually being hit.
        if (other.CompareTag("Player"))
        {
            Invoke("createAttack", 0.3f);
        }
    }

    private void createAttack()
    {
        Instantiate(MeleeStrike, transform.position, Quaternion.identity, transform);
    }

    void mAttack()
    {
        Physics.Raycast(transform.position, Vector3.forward, 10f);
        Debug.DrawRay(transform.position, Vector3.forward, Color.red);
    }
    void Start()
    {
        InvokeRepeating("LightBehaviour", 0f, 0.25f);
        enemyCollider = GetComponent<CapsuleCollider>();
        enemyLight.range = 0f;
    }
}