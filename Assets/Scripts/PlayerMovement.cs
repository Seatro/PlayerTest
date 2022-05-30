using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float maxSpeed = 5f;
    [SerializeField] private Rigidbody playerRigid;
    [SerializeField] private bool useNormalized;

    private Vector3 oldPosition;
     
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(useNormalized == false)
        {
            MovePlayer();
        }
        if(useNormalized == true)
        {
            MovePlayerNormalized();
        }
    }

    private void MovePlayer()
    {
        float horizontalMove = Input.GetAxis("Horizontal");
        float verticalMove = Input.GetAxis("Vertical");

        if (horizontalMove > 0.01 || horizontalMove < -0.01)
        {
            transform.Translate(transform.right * Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime);
        }
        if (verticalMove > 0.01 || verticalMove < -0.01)
        {
            transform.Translate(transform.forward * Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime);
        }

        var speed = Vector3.Distance(oldPosition, transform.position) / Time.deltaTime;
        oldPosition = transform.position;

        Debug.Log(speed);
    }

    private void MovePlayerNormalized()
    {
        float horizontalMove = Input.GetAxis("Horizontal");
        float verticalMove = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(horizontalMove, 0, verticalMove);
        move.Normalize();

        transform.position += move * moveSpeed * Time.deltaTime;

        var speed = Vector3.Distance(oldPosition, transform.position) / Time.deltaTime;
        oldPosition = transform.position;

        Debug.Log(speed);
    }
}