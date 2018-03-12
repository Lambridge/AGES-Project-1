using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    enum PlayerState { onGround, inMidair, takingDamage }
    [SerializeField] PlayerState currentPlayerState;

    #region Movement Variables
    Rigidbody playerRigidbody;
    [SerializeField] float movementVelocity;
    [SerializeField] float movementVelocityWithHeadObject;
    [SerializeField] float midairAccelerationAmount;

    //Variables for player management
    public int playerNumber;
    string xMovementAxisName;
    string zMovementAxisName;
    string jumpButtonName;
    #endregion

    #region Jump Variables
    [SerializeField] Transform groundDetectPoint;
    [SerializeField] float groundDetectRadius;
    [SerializeField] LayerMask whatIsGround;
    [SerializeField] float jumpVelocity = 20f;
    bool isOnGround;
    bool canJump;
    #endregion

    #region Grabbing/Throwing Variables
    bool hasHeadObject = false;
    #endregion

    #region Properties
    private bool IsOnGround
    {
        get
        {
            return UpdateIsOnGround();
        }
    }

    public bool HasHeadObject
    {
        set
        {
            hasHeadObject = value;
        }
    }
    #endregion

    // Use this for initialization
    void Start () {
        playerRigidbody  = GetComponent<Rigidbody>();
        xMovementAxisName = "Horizontal" + playerNumber;
        zMovementAxisName = "Vertical" + playerNumber;
        jumpButtonName = "Jump" + playerNumber;
    }

    private void Update()
    {
        HandleMovementInput();
        HandleJumpInput();
        //HandleGrabOrThrowInput();
        UpdateIsOnGround();
    }

    private void HandleMovementInput()
    {
        float xAxisMovementInput = Input.GetAxis(xMovementAxisName);
        float zAxisMovementInput = Input.GetAxis(zMovementAxisName);

        Vector3 velocityToSet;

        //How to move while on the ground
        if(currentPlayerState == PlayerState.onGround)
        {
            if (!hasHeadObject)
            {
                //Take inputs and find what our new movement velocity will be
                velocityToSet =
                    new Vector3(xAxisMovementInput, 0, zAxisMovementInput) * movementVelocity;
                //Keep the same midair velocity
                velocityToSet += new Vector3(0, playerRigidbody.velocity.y, 0);
            }
            else
            {
                //Take inputs and find what our new movement velocity will be
                velocityToSet =
                    new Vector3(xAxisMovementInput, 0, zAxisMovementInput)* movementVelocityWithHeadObject;
                //Keep the same midair velocity
                velocityToSet += new Vector3(0, playerRigidbody.velocity.y, 0);
            }

            //Set velocity
            playerRigidbody.velocity = velocityToSet;

        }

        else if (currentPlayerState == PlayerState.inMidair)
        {
            //Add velocity to the current velocity
            Vector3 currentVelocity = playerRigidbody.velocity;
            //Take inputs and find what our acceleration velocity will be
            Vector3 acceleration =
                new Vector3(xAxisMovementInput, 0, zAxisMovementInput) * midairAccelerationAmount * Time.deltaTime;
            velocityToSet = currentVelocity + acceleration;

            if(velocityToSet.magnitude > movementVelocity)
            {
                velocityToSet =
                    new Vector3(xAxisMovementInput, 0, zAxisMovementInput) * movementVelocity
                    + new Vector3 (0, currentVelocity.y, 0);
            }

            //Set velocity
            playerRigidbody.velocity = velocityToSet;
        }

        //Handle player rotation only if the player is moving
        if(playerRigidbody.velocity != Vector3.zero)
        {
            //Quaternion.LookRotation() gives out error when horizontal and vertical axis input is 0
            //So let's avoid that altogether
            if (xAxisMovementInput == 0 && zAxisMovementInput == 0)
            {
                //Don't do anything
            }
            else
                HandlePlayerRotatiion(xAxisMovementInput, zAxisMovementInput);
        }
        
    }

    void HandleJumpInput()
    {
        //If we're on the ground, the player can always jump
        if(IsOnGround)
            canJump = true;
        
        //If player presses jump button and is on ground
        if (Input.GetButtonDown(jumpButtonName))
        {
            if (IsOnGround)
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
                currentPlayerState = PlayerState.inMidair;
                Debug.Log("State set to inMidair");
            }
            else if(canJump)
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
                currentPlayerState = PlayerState.inMidair;
                Debug.Log("State set to inMidair");

                //They can only jump in midair once
                canJump = false;
            }
        }
    }

    private void HandlePlayerRotatiion(float xInput, float zInput)
    {
        //Take the player input and make the player look in that direction
        Vector3 movementVector = new Vector3(xInput, 0f, zInput);
        transform.rotation = Quaternion.LookRotation(movementVector);
    }

    //void HandleGrabOrThrowInput()
    //{
    //    //If we press the button to grab an object
    //    if (Input.GetButtonDown("Fire1"))
    //    {
    //        //And are holding an object
    //        //But didn't just pick him up this frame
    //        if (isHoldingObject && !objectGrabbedThisFrame)
    //        {
    //            //Throw the object!
    //            ThrowObject();

    //        }
    //        //Otherwise if we aren't holding an object
    //        else if(!isHoldingObject)
    //        {
    //            //Try to pick something up
    //            PickUpObject();
    //        }
    //    }
    //}

    //void PickUpObject()
    //{
    //    //Find an object we can pick up
    //    ThrowableObjectScript objectToGrab = GetObjectToGrab();
    //    //If we found an object we can grab
    //    if (objectToGrab)
    //    {
    //        //Store object to be thrown later
    //        gameObjectBeingHeld = objectToGrab;
    //        isHoldingObject = true;
    //        objectGrabbedThisFrame = true;
    //        //Tell throwable object they are being held
    //        objectToGrab.CurrentlyBeingHeld = true;
    //        //Tell throwable object about this object
    //        objectToGrab.PlayerToFollow = gameObject;
    //        Debug.Log("Found an object and picked it up");
    //    }
    //    else
    //    {
    //        Debug.Log("Found no object to pick up");
    //    }
    //}

    //private ThrowableObjectScript GetObjectToGrab()
    //{
    //    Collider[] objectsInRangeOfGrab = Physics.OverlapSphere(objectDetectPoint.position, objectDetectRadius);
    //    Debug.Log("objectsInRangeOfGrab length = " + objectsInRangeOfGrab.Length);
    //    for (int i = 0; i < objectsInRangeOfGrab.Length; i++)
    //    {
    //        Debug.Log("Collider number " + i);
    //        ThrowableObjectScript nullCheck = objectsInRangeOfGrab[i].gameObject.GetComponent<ThrowableObjectScript>();
    //        //Look for an object which has the ThrowableObjectScript attached
    //        if (nullCheck == null)
    //        {
    //            //If one is found, return that object
    //            return objectsInRangeOfGrab[i].gameObject.GetComponent<ThrowableObjectScript>();
    //        }
    //    }
    //    //Otherwise return no object
    //    return null;
    //}

    //void ThrowObject()
    //{
    //    //Make sure we don't instantly throw objects
    //    if (isHoldingObject && !objectGrabbedThisFrame)
    //    {
    //        if(Input.GetButton("Fire1"))
    //        {
    //            //Tell throwable object they need to throw themselves
    //            gameObjectBeingHeld.NeedsToBeThrown = true;
    //            gameObjectBeingHeld.CurrentlyBeingHeld = false;
    //            //Make sure there is no object grabbed this frame
    //            objectGrabbedThisFrame = false;
    //        }
    //    }
    //}

    private bool UpdateIsOnGround()
    {
        Collider[] groundColliders = Physics.OverlapSphere(groundDetectPoint.position, groundDetectRadius, whatIsGround);
        ///What is this ????? you can do = and > ????
        bool onGround = groundColliders.Length > 0;
        currentPlayerState = UpdatePlayerState(onGround);
        return onGround;
    }

    private PlayerState UpdatePlayerState(bool onGround)
    {
        PlayerState newPlayerState = PlayerState.inMidair;

        if (onGround)
            newPlayerState = PlayerState.onGround;
        else if(currentPlayerState != PlayerState.takingDamage)
            newPlayerState = PlayerState.inMidair;

        return newPlayerState;
    }

    public void HandleKnockback(Vector3 collidedObjectVelocity)
    {
        float knockbackMultiplier = 1.65f;
        Vector3 knockback = collidedObjectVelocity * knockbackMultiplier;
        
        playerRigidbody.velocity = playerRigidbody.velocity + knockback;
    }

}
