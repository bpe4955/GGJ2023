using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] KeyCode jumpKey, leftKey, rightKey;
    [SerializeField] float moveSpeed, jumpVelocity;
    Rigidbody2D rb;
    Collider2D playerCollider;
    Vector2 moveDir; //stores the direction the player wants to move in (horiztonally)
    bool wantsToJump; //if the player wants to jump, uses input from jumpKey
    [SerializeField] float groundCheckDist = 0.02f; //how far the raycast to check if the player is grounded goes
    [SerializeField] Transform groundCheckPoint; //gameobject to cats the check from
    [SerializeField] LayerMask groundCheckLayers; //layer that the ground check will hit, and count as ground


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        SetMoveDir();
    }

    private void FixedUpdate()
    {
        MoveHorizontally();

        TryToJump();
    }


    //get the player's input and store it in moveDir
    void SetMoveDir()
    {
        //left right movement
        moveDir.x = Input.GetAxisRaw("Horizontal");

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
		//if in air then dont move as much
		currentVeloc.x = moveDir.normalized.x * moveSpeed;
		rb.velocity = currentVeloc;
    }


    //try to jump if the player wants to jump and if they are grounded
    void TryToJump()
    {
        if(wantsToJump && IsGrounded())
        {
            //sets y velocity directly
            Vector2 currentVeloc = rb.velocity;
            currentVeloc.y = jumpVelocity;
            rb.velocity = currentVeloc;
        }
    }


    //returns wether or not the player is grounded
    public bool IsGrounded()
    {
        RaycastHit2D[] hits = new RaycastHit2D[1];
        return playerCollider.Cast(Vector2.down, hits, groundCheckDist) > 0;
    }
}
