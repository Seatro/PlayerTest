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

        //NEEDS A DIFFERENT SOLUTION.
        //transform.localPosition += move * moveSpeed * Time.deltaTime;

        transform.Translate(move * moveSpeed * Time.deltaTime, Space.Self);

        // Unused, but may be useful later.
        var speed = Vector3.Distance(oldPosition, transform.position) / Time.deltaTime;
        oldPosition = transform.position;
    }

    private void MoveCamera()
    {
        float horizontal = Input.GetAxis("Mouse X");
        float vertical = Input.GetAxis("Mouse Y");

        float newPlayerXRotation = transform.localRotation.eulerAngles.y + mouseXSensitivity * horizontal;
        float newCameraYRotation = cameraTransform.localRotation.eulerAngles.x - mouseYSensitivity * vertical;

        transform.rotation = Quaternion.Euler(transform.rotation.x, newPlayerXRotation, transform.rotation.z);
        cameraTransform.localRotation = Quaternion.Euler(newCameraYRotation, cameraTransform.localRotation.y, cameraTransform.localRotation.z);
    }
}
