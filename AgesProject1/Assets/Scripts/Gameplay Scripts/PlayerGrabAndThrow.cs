using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrabAndThrow : MonoBehaviour {

    Transform playerTransform;
    Rigidbody playerRigidbody;

    public bool HasThrowableHead { get; set; }
    bool throwableGrabbedThisFrame;
    GameObject throwableObject;

    [SerializeField] float throwVelocityForward;
    [SerializeField] float throwVelocityY;

    public int playerNumber;
    string actionInputName;

	// Use this for initialization
	void Start () {
        playerNumber = gameObject.GetComponent<PlayerScript>().playerNumber;
        playerTransform = transform;
        playerRigidbody = gameObject.GetComponent<Rigidbody>();
        actionInputName = "Fire" + playerNumber;
	}
	
	// Update is called once per frame
	void Update () {
        if (HasThrowableHead)
        {
            //UpdateObjectPosition();
            HandleThrowInput();
        }

        if (throwableGrabbedThisFrame == true)
            throwableGrabbedThisFrame = false;
    }

    public void PickUpObject(GameObject objectToPickUp)
    {
        throwableObject = objectToPickUp;
        HasThrowableHead = true;
        ThrowableObject throwableScript = objectToPickUp.GetComponent<ThrowableObject>();
        throwableScript.BeingHeld = true;
        throwableScript.ObjectToFollow = gameObject;
        throwableGrabbedThisFrame = true;
    }

    void HandleThrowInput()
    {
        if (Input.GetButtonDown(actionInputName) && !throwableGrabbedThisFrame)
        {
            //Get throw vector based on 
            Vector3 throwVector = playerTransform.forward.normalized * throwVelocityForward;
            //Add vertical height to the throw
            Vector3 throwHeightVector = new Vector3(0, throwVelocityY, 0);
            throwVector += throwHeightVector;
            //Add player movement velocity
            Vector3 playerVelocityToAdd = new Vector3
                (playerRigidbody.velocity.x, 0, playerRigidbody.velocity.z) * 0.462f;
            throwVector += playerVelocityToAdd;

            //Tell throwable object to throw itself
            throwableObject.GetComponent<ThrowableObject>().ThrowSelf(throwVector);

            //Assign current object as nothing
            HasThrowableHead = false;
            throwableObject = null;
        }
    }
}
