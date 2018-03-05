using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableObjectScript : MonoBehaviour {

    
    bool objectGrabbedThisFrame;

    Rigidbody objectRigidbody;
    [SerializeField] float throwVelocity;

    GameObject playerToFollow = null;
    public GameObject PlayerToFollow
    {
        set
        {
            playerToFollow = value;
        }
    }

    bool currentlyBeingHeld = false;
    public bool CurrentlyBeingHeld
    {
        get
        {
            return currentlyBeingHeld;
        }
        set
        {
            currentlyBeingHeld = value;
            //We don't want the object to collide with the player
            //So disable the box collider
            //But perhaps this collision can be disabled with layers in the editor
            if (currentlyBeingHeld == true)
            {
                gameObject.GetComponent<BoxCollider>().enabled = false;
            }
        }
    }

    bool needsToBeThrown;
    public bool NeedsToBeThrown
    {
        set
        {
            needsToBeThrown = value;
            if (needsToBeThrown)
            {
                HandleThrowInput();
            }
        }
    }

    // Use this for initialization
    void Start () {
        objectRigidbody = gameObject.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        if (currentlyBeingHeld)
        {
            FollowPlayerPosition();
        }

        if (objectGrabbedThisFrame == true) objectGrabbedThisFrame = false;
    }

    void FollowPlayerPosition()
    {
        float heightOffset = 1.05f;
        transform.position = playerToFollow.transform.position + new Vector3(0, heightOffset, 0);
        transform.rotation = playerToFollow.transform.rotation;
    }

    void HandleThrowInput()
    {
        if (Input.GetButtonDown("Fire1") && objectGrabbedThisFrame == false)
        {
            //Change holding variables
            currentlyBeingHeld = false;
            needsToBeThrown = false;
            playerToFollow = null;

            //Get player input
            float horizontalAxisInput = Input.GetAxis("Horizontal");
            float verticalAxisInput = Input.GetAxis("Vertical");

            //Change velocity based on input
            Vector3 throwVelocityToSet = new Vector3 (horizontalAxisInput, 0, verticalAxisInput) * throwVelocity;
            objectRigidbody.velocity = throwVelocityToSet;

            //Reenable the box collider
            gameObject.GetComponent<BoxCollider>().enabled = true;
        }
    }
}
