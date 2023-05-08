using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class playerController : MonoBehaviour
{
    // Objects.
    CharacterController playerMovement;
    Transform playerCamera;

    // Movement.
    [SerializeField] float playerSpeed = 3.75f;

    // Gravity
    Vector3 velocitySpeed;
    [SerializeField] float gravitySpeed = -47.05f;
    [SerializeField] float jumpHeight = 1.25f;

    // Mouse
    [SerializeField] float mouseSpeed = 2f;
    float maxMouseAngle = 360f;
    float minMouseAngle = -90f;

    void Start()
    {
        playerMovement = GetComponent<CharacterController>();
        playerCamera = this.gameObject.transform.GetChild(0);
    }

    void Update()
    {
        playerControls();
        mouseControls();
    }

    void playerControls()
    {
        // Movement.
        float xMovement = Input.GetAxis("Horizontal");
        float zMovement = Input.GetAxis("Vertical");

        Vector3 vectorMovement = new Vector3(xMovement, 0, zMovement);

        // Gravity / jumping.
        if (Input.GetKey("space") && playerMovement.isGrounded)
        {
            velocitySpeed.y += Mathf.Sqrt(jumpHeight * -3.0f * gravitySpeed);
        }
        if (!playerMovement.isGrounded)
        {
            // Limit fall speed (Terminal velocity).
            velocitySpeed.y += Mathf.Clamp(gravitySpeed * Time.deltaTime, -45f, 0);
        }

        playerMovement.Move((vectorMovement * playerSpeed * Time.deltaTime) + (velocitySpeed * Time.deltaTime));
    }

    void mouseControls()
    {
        float yMouse = Input.GetAxis("Mouse X") * mouseSpeed;
        float xMouse = Input.GetAxis("Mouse Y") * mouseSpeed;

        // Limit x rotaion axis (looking up and down).
        xMouse = Mathf.Clamp(xMouse, minMouseAngle, maxMouseAngle);

        playerCamera.transform.eulerAngles += new Vector3(-xMouse, transform.eulerAngles.y + yMouse, 0);
    }
}