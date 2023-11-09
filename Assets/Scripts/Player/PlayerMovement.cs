using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 5.0f;
    public float mouseSensitivity = 2.0f;

    private CharacterController player;
    private Camera playerCamera;
    private float verticalRotation = 0f;

    void Start()
    {
        player = GetComponent<CharacterController>();
        playerCamera = GetComponentInChildren<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float forwardMovement = Input.GetAxis("Vertical") * movementSpeed;
        float strafeMovement = Input.GetAxis("Horizontal") * movementSpeed;

        Vector3 speed = new Vector3(strafeMovement, 0, forwardMovement);
        speed = transform.rotation * speed;
        player.SimpleMove(speed);

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);

        playerCamera.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
        transform.Rotate(Vector3.up * mouseX);
    }
}
