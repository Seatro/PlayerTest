using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float sprintIncrease = 3f;
    [SerializeField] private float jumpAmount = 5f;
    [SerializeField] private float superJumpAmount = 30f;
    [SerializeField] private float mouseXSensitivity = 0.5f;
    [SerializeField] private float mouseYSensitivity = 0.5f;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Transform groundDetector;
    [SerializeField] private Rigidbody playerRigid;

    private bool superJump;
    private float originalJumpAmount;

    private Vector3 oldPosition;
    private float normalSpeed;
    private void Start()
    {
        normalSpeed = moveSpeed;

        originalJumpAmount = jumpAmount;

        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();

        MoveCamera();

        Jump();
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

    private void Jump()
    {
        if (!Input.GetButtonDown("Jump")) return;

        if (Input.GetButton("Fire1"))
        {
            superJump = true;
            jumpAmount = 30f;
        }
        
        RaycastHit hit;
        Physics.Raycast(groundDetector.position, -transform.up, out hit, Mathf.Infinity);

        if (hit.distance > 0.4f) return;

        playerRigid.AddForce(transform.up * jumpAmount, ForceMode.Impulse);

        jumpAmount = originalJumpAmount;

        var distanceToGround = Vector3.Distance(transform.position, hit.point);
        Debug.Log(hit.distance);
    }
}
