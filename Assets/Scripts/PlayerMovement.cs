using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float sprintIncrease = 3f;
    [SerializeField] private float mouseXSensitivity = 0.5f;
    [SerializeField] private float mouseYSensitivity = 0.5f;
    [SerializeField] private Transform cameraTransform;

    private Vector3 oldPosition;
    private float normalSpeed;
    private void Start()
    {
        normalSpeed = moveSpeed;

        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();

        MoveCamera();
    }

    private void MovePlayer()
    {
        float horizontalMove = Input.GetAxis("Horizontal");
        float verticalMove = Input.GetAxis("Vertical");

        if(Input.GetButtonDown("Sprint"))
        {
            moveSpeed += sprintIncrease;
        }
        else if(Input.GetButtonUp("Sprint"))
        {
            moveSpeed = normalSpeed;
        }

        Vector3 move = new Vector3(horizontalMove, 0, verticalMove);
        move.Normalize();

        transform.position += move * moveSpeed * Time.deltaTime;

        // Unused, but may be useful later.
        var speed = Vector3.Distance(oldPosition, transform.position) / Time.deltaTime;
        oldPosition = transform.position;
    }

    private void MoveCamera()
    {
        float vertical = Input.GetAxis("Mouse X");
        float horizontal = Input.GetAxis("Mouse Y");

        float newPlayerRotation = cameraTransform.localRotation.eulerAngles.x - mouseXSensitivity * horizontal;
        float newCameraRotation = cameraTransform.localRotation.eulerAngles.y + mouseYSensitivity * vertical;

        cameraTransform.localRotation = Quaternion.Euler(newPlayerRotation, newCameraRotation, 0);
    }
}
