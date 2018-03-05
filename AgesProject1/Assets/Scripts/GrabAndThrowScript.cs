using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabAndThrowScript : MonoBehaviour {

    bool currentlyHoldingAnObject = false;
    GameObject objectBeingHeld;
    bool objectGrabbedThisFrame;
    [SerializeField] Transform playerTransform;
    [SerializeField] float throwVelocity;
    [SerializeField] float throwHeight;

    [HideInInspector] public int playerNumber;
    string actionButtonName;

    private void Start()
    {
        actionButtonName = "Fire" + playerNumber;
    }

    // Update is called once per frame
    void Update () {
        if (currentlyHoldingAnObject)
        {
            UpdateObjectPosition();
            HandleThrowInput();
        }

        if (objectGrabbedThisFrame == true) objectGrabbedThisFrame = false;
	}

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Throwable" && currentlyHoldingAnObject == false)
        {
 //           Debug.Log("Found grabbable object");
            if (Input.GetButtonDown(actionButtonName))
            {
                PickUpObject(other.gameObject);
                Debug.Log("Object picked up");
            }
        }

    }

    private void PickUpObject(GameObject gameObject)
    {
        objectBeingHeld = gameObject;
        currentlyHoldingAnObject = true;
        objectBeingHeld.transform.position = playerTransform.position + new Vector3(0, 1.05f, 0);
        objectGrabbedThisFrame = true;
        objectBeingHeld.GetComponent<BoxCollider>().enabled = false;
        //Tell player they now have a head
        playerTransform.gameObject.GetComponent<PlayerScript>().HasHeadObject = true;
    }

    void UpdateObjectPosition()
    {
        objectBeingHeld.transform.position = playerTransform.position + new Vector3(0, 1.05f, 0);
        objectBeingHeld.transform.rotation = playerTransform.rotation;
    }

    void HandleThrowInput()
    {
        if (Input.GetButtonDown(actionButtonName) && objectGrabbedThisFrame == false)
        {
            //Throw the object!
            //Rigidbody objectRigidbody = objectBeingHeld.GetComponent<Rigidbody>();
            Vector3 throwHeightVector = new Vector3 (0, throwHeight, 0);
            Vector3 throwVector = playerTransform.forward.normalized + throwHeightVector;
            //Get the player's velocity minus upward/downward movement
            Vector3 playerVelocityToAdd = new Vector3 
                (
                playerTransform.gameObject.GetComponent<Rigidbody>().velocity.x,
                0,
                playerTransform.gameObject.GetComponent<Rigidbody>().velocity.z
                ) * 0.462f;
            //Change object velocity
            objectBeingHeld.GetComponent<Rigidbody>().velocity =
                (throwVector * throwVelocity) + playerVelocityToAdd;
            objectBeingHeld.GetComponent<BoxCollider>().enabled = true;
            //Tell the Head that it has been thrown
            objectBeingHeld.GetComponent<ThrowableObject>().HasBeenThrown = true;
            //Set variables so nothing is held
            currentlyHoldingAnObject = false;
            objectBeingHeld = null;
            //Tell player they no longer have a head
            playerTransform.gameObject.GetComponent<PlayerScript>().HasHeadObject = false;
            
        }
    }

}
