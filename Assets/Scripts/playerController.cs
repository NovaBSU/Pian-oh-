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
    Vector3 lastPostion;

    // Gravity
    Vector3 velocitySpeed;
    [SerializeField] float gravitySpeed = -47.05f;
    [SerializeField] float jumpHeight = 1.25f;

    // Mouse
    [SerializeField] float mouseSpeed = 2f;

    //DEBUG
    [SerializeField] float currentSpeed;

    void Start()
    {
        playerMovement = GetComponent<CharacterController>();
        playerCamera = this.gameObject.transform.GetChild(0);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        playerControls();
        mouseControls();
    }

    void playerControls()
    {
        if (transform.position != lastPostion && playerMovement.isGrounded)
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

        lastPostion = transform.position;

        // Movement.
        float xMovement = Input.GetAxis("Horizontal");
        float zMovement = Input.GetAxis("Vertical");

        Vector3 vectorMovement = (transform.forward * zMovement) + (transform.right * xMovement);
        vectorMovement *= playerSpeed;
        if(Input.GetButton("Sprint"))
        {
            playerSpeed = 50f;
        }
        if (Input.GetButtonUp("Sprint"))
        {
            playerSpeed = 3.75f;
        }
        if(Input.GetButton("Warp"))
        {
            playerSpeed = 2500f;
            playerSpeed = 3.75f;
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