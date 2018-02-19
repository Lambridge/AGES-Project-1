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

	// Use this for initialization
	void Start () {
		
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
            Debug.Log("Found grabbable object");
            if (Input.GetButtonDown("Fire1"))
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
    }

    void UpdateObjectPosition()
    {
        objectBeingHeld.transform.position = playerTransform.position + new Vector3(0, 1.05f, 0);
        objectBeingHeld.transform.rotation = playerTransform.rotation;
    }

    void HandleThrowInput()
    {
        if (Input.GetButtonDown("Fire1") && objectGrabbedThisFrame == false)
        {
            //Throw the object!
            //Rigidbody objectRigidbody = objectBeingHeld.GetComponent<Rigidbody>();
            Vector3 throwVector = playerTransform.forward.normalized;
            objectBeingHeld.GetComponent<Rigidbody>().velocity = throwVector * throwVelocity;
            objectBeingHeld.GetComponent<BoxCollider>().enabled = true;
            Debug.DrawRay(objectBeingHeld.transform.position, throwVector * throwVelocity, Color.red);
            //Set variables so nothing is held
            currentlyHoldingAnObject = false;
            objectBeingHeld = null;
        }
    }

}
