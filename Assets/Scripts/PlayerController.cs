using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] KeyCode jumpKey, leftKey, rightKey;
    [SerializeField] float moveSpeed, jumpVelocity;
    bool isGrounded; //wether or not the player is touching the ground
    Rigidbody2D rb;
    Vector2 moveDir; //stores the direction the player wants to move in (horiztonally)
    bool wantsToJump; //if the player wants to jump, uses input from jumpKey
    [SerializeField] float groundCheckDist = 0.02f; //how far the raycast to check if the player is grounded goes
    [SerializeField] Transform groundCheckPoint; //gameobject to cats the check from
    [SerializeField] LayerMask groundCheckLayers; //layer that the ground check will hit, and count as ground


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        SetMoveDir();
    }

    private void FixedUpdate()
    {
        MoveHorizontally();
    }


    //get the player's input and store it in moveDir
    void SetMoveDir()
    {
        //left right movement
        if (Input.GetKey(leftKey))
        {
            moveDir.x = -1;
        }
        else if(Input.GetKey(rightKey))
        {
            moveDir.x = 1;
        }
        else
        {
            moveDir.x = 0;
        }

        //jumping
        if(Input.GetKey(jumpKey))
        {
            wantsToJump = true;
        }
        else
        {
            wantsToJump = false;
        }
    }

    
    //apply the player's horizontal movement
    void MoveHorizontally()
    {
        //sets x velocity directly
        Vector2 currentVeloc = rb.velocity;
        currentVeloc.x = moveDir.normalized.x * moveSpeed;
        rb.velocity = currentVeloc;
    }
}
