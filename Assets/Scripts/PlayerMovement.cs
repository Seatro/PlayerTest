using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float sprintIncrease = 3f;

    private Vector3 oldPosition;
    private float normalSpeed;

    private void Start()
    {
        normalSpeed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
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
}
