using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController {

    private Rigidbody2D playerRigidBody;
    private Transform playerTransform;

    //private Vector2 facingDirection;

    public PlayerController(ref Rigidbody2D playerRigidBody, ref Transform playerTransform, ref Vector2 facingDirection) {

        this.playerRigidBody = playerRigidBody;
        this.playerTransform = playerTransform;
        //this.facingDirection = facingDirection;

        if(facingDirection != Vector2.right) { flip(ref facingDirection); }

    }

    public void horizontalMovement(float movementSpeed, ref Vector2 facingDirection) {

        float moveDirection = Input.GetAxis(Global.controlsLeftRight);

        playerRigidBody.velocity = new Vector2(movementSpeed * Input.GetAxis(Global.controlsLeftRight), playerRigidBody.velocity.y);
        
        if (Math.Sign(moveDirection) + facingDirection.x == 0) {
            flip(ref facingDirection);
        }

    }

    public void Jump(ref bool isJumping, float jumpForce) {

        if (Input.GetButtonDown(Global.controlsJump) && !isJumping) {

            isJumping = true;

            playerRigidBody.AddForce(new Vector2(playerRigidBody.velocity.x, jumpForce));

            //switch to jumping animation

        }

    }

    public void crouch(ref bool isCrouching) {

        if (Input.GetButtonDown(Global.controlsCrouch)) {

            switch(isCrouching) {

                case true:
                    //change animation
                    break;

                case false:
                    //change animation
                    break;

            }

            isCrouching = !isCrouching;

        }

    }

    public void flip(ref Vector2 facingDirection) {

        facingDirection = -facingDirection;

        playerRigidBody.transform.localScale = new Vector3(Mathf.Abs(playerTransform.transform.localScale.x) * facingDirection.x,
                                                playerTransform.transform.localScale.y,
                                                playerTransform.transform.localScale.z);

    }

}
