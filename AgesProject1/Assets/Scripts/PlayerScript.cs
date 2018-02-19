using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    #region Movement Variables
    Rigidbody playerRigidbody;
    [SerializeField] float movementVelocity;
    #endregion

    #region Jump Variables
    [SerializeField] Transform groundDetectPoint;
    [SerializeField] float groundDetectRadius;
    [SerializeField] LayerMask whatIsGround;
    [SerializeField] float jumpVelocity = 20f;
    bool isOnGround;
    #endregion

    #region Grabbing/Throwing Variables
    enum IsHoldingObject { notHoldingObject, holdingObject }
    #endregion

    #region Properties
    private bool IsOnGround
    {
        get
        {
            return UpdateIsOnGround();
        }
    }
    #endregion

    // Use this for initialization
    void Start () {
        playerRigidbody  = GetComponent<Rigidbody>();
	}

    private void Update()
    {
        {
            HandleMovementInput();
            HandleJumpInput();
            UpdateIsOnGround();
            
        }
    }

    private bool UpdateIsOnGround()
    {
        Collider[] groundColliders = Physics.OverlapSphere(groundDetectPoint.position, groundDetectRadius, whatIsGround);
        ///What is this ????? you can do = and > ????
        bool isOnGound = groundColliders.Length > 0;
        return isOnGound;
    }

    private void HandleMovementInput()
    {
        float movementSpeedMultiplier = 30; //Makes it so speed doesn't need to be set outragiously high
        float horizontalMovementInput = Input.GetAxis("Horizontal");
        float xVelocity = horizontalMovementInput * movementVelocity;

        float verticalMovementInput = Input.GetAxis("Vertical");
        float zVelocity = verticalMovementInput * movementVelocity;

        float yVelocity = playerRigidbody.velocity.y;

        Vector3 velocityToSet = new Vector3(xVelocity, yVelocity, zVelocity);
        playerRigidbody.velocity = velocityToSet;

        if(horizontalMovementInput == 0 && verticalMovementInput == 0)
        {
            //I don't know I just didn't want them to rotate back to zero rotation
        }
        else
        {
            HandlePlayerRotatiion(horizontalMovementInput, verticalMovementInput);
        }
        
    }

    void HandleJumpInput()
    {
        //If player presses jump button and is on ground
        if (Input.GetButtonDown("Jump") && IsOnGround)
        {
            //Non-vertical speed needs to stay the same
            float xVelocity = playerRigidbody.velocity.x;
            float zVelocity = playerRigidbody.velocity.z;
            //Make new upward velocity
            float yVelocity = jumpVelocity;
            //Make the overall new velocity
            Vector3 velocityToSet = new Vector3(xVelocity, yVelocity, zVelocity);
            //Set the new velocity
            playerRigidbody.velocity = velocityToSet;
        }
    }

    private void HandlePlayerRotatiion(float xInput, float zInput)
    {
        //Take the player input and make the player look in that direction
        Vector3 movementVector = new Vector3(xInput, 0f, zInput);
        transform.rotation = Quaternion.LookRotation(movementVector);
    }

}
