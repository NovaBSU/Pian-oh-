using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    // Objects.
    public lightBehaviour lightBehaviourComponent;
    CharacterController playerMovement;
    Transform playerCamera;

    // Movement.
    [SerializeField] float playerSpeed = 3.75f;
    Vector3 lastPosition;

    // Gravity
    Vector3 velocitySpeed;
    [SerializeField] float gravitySpeed = -47.05f;
    [SerializeField] float jumpHeight = 1.25f;

    // Mouse
    [SerializeField] float mouseSpeed = 2f;

    //DEBUG
    [SerializeField] float currentSpeed;

    // Calls the light burst
    public GameObject LightBurster;

    // just starting the variable
    private int warpReset = 0;

    // Health
    private int pHealth = 100;
    private bool pInvuln = false;

    void Start()
    {
        playerMovement = GetComponent<CharacterController>();
        playerCamera = this.gameObject.transform.GetChild(0);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

    }

    void ResetSpeed()
    {
        playerSpeed = 3.75f;
    }
    void ResetWarp()
    {
        warpReset = 0;
    }

    void Update()
    {
        playerControls();
        mouseControls();

        if (pHealth < 1)
        {
            // DIE
        }
    }

    // This function will be called from within the script that the player is hurt by.
    // This function also handles the player's regenerative health, calling the autoHeal function.
    // autoHeal increases the player's health by 8 (done every .25s, so 32hp/s) until the player's
    // HP is 100 or greater, then if it is above 100 it is set to 100.
    // This script also now gives players a half second of invulnerability, as iframes make games
    // a whole hell of a lot more playable. (and it was buggy as all disbelief otherwise)
    public void damage()
    {
        if (pInvuln == false)
        {
            CancelInvoke("autoHeal");
            pHealth = pHealth - 45;
            InvokeRepeating("autoHeal", 2.5f, 0.25f);
            Debug.Log("Player has taken damage. Current HP: " + pHealth);
        }
        pInvuln = true;
        Invoke("endIFrames", 0.5f);
    }
    void endIFrames()
    {
        pInvuln = false;
    }
    void autoHeal()
    {
        if (pHealth < 100)
        {
            pHealth = pHealth + 8;
        }
        if (pHealth > 100)
        {
            pHealth = 100;
        }
        Debug.Log("Player Healed. Current HP: " + pHealth);
    }

    void playerControls()
    {
        if (transform.position != lastPosition && playerMovement.isGrounded)
        {
            lightBehaviourComponent.lightParameter(0);
        }

        else
        {
            lightBehaviourComponent.lightTest.intensity = 0;
        }

        if (playerMovement.isGrounded && velocitySpeed.y < -2f)
        {
            lightBehaviourComponent.lightParameter(1);
        }

        if (playerMovement.isGrounded && velocitySpeed.y < 0)
        {
            velocitySpeed.y = 0f;
        }

        lastPosition = transform.position;

        // Movement.
        float xMovement = Input.GetAxis("Horizontal");
        float zMovement = Input.GetAxis("Vertical");

        Vector3 vectorMovement = (transform.forward * zMovement) + (transform.right * xMovement);
        vectorMovement *= playerSpeed;
        if(Input.GetButton("Sprint"))
        {
            playerSpeed = 6f;
        }
        if (Input.GetButtonUp("Sprint"))
        {
            playerSpeed = 3.75f;
        }
        if (Input.GetButtonDown("Warp"))
        {
            if (warpReset == 0)
            {
                Instantiate(LightBurster, lastPosition, transform.rotation);
                playerSpeed = 80f;
                warpReset = 1;
                Invoke("ResetSpeed", 0.15f);
                Invoke("ResetWarp", 2f);
            }
        }

        if(Input.GetButton("Jump") && playerMovement.isGrounded)
        {
            velocitySpeed.y += Mathf.Sqrt(jumpHeight * -3.0f * gravitySpeed);
        }

        if (!playerMovement.isGrounded)
        {
            velocitySpeed.y += Mathf.Clamp(gravitySpeed * Time.deltaTime, -45.0f, 0.0f);
        }

        playerMovement.Move((vectorMovement * Time.deltaTime) + (velocitySpeed * Time.deltaTime));
    }

    void mouseControls()
    {
        float yMouse = Input.GetAxis("Mouse X") * mouseSpeed;
        float xMouse = Input.GetAxis("Mouse Y") * mouseSpeed;

        // Limit x rotaion axis (looking up and down).
        xMouse = Mathf.Clamp(xMouse ,-89 ,89);

        playerCamera.transform.eulerAngles += new Vector3(-xMouse ,0 ,0);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + yMouse, 0);
    }
}