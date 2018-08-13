using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableDetectionCollider : MonoBehaviour {

    int playerNumber;
    string actionButtonName;

    GameObject objectToPickUp;
    GameObject playerObject;
    PlayerScript playerScript;
    PlayerGrabAndThrow grabAndThrowScript;

	// Use this for initialization
	void Start () {
        playerScript = gameObject.GetComponentInParent<PlayerScript>();
        playerObject = playerScript.gameObject;
        playerNumber = playerScript.playerNumber;
        grabAndThrowScript = playerObject.GetComponent<PlayerGrabAndThrow>();
        actionButtonName = "Fire" + playerNumber;
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Throwable")
        {
            bool currentlyHasThrowable = playerObject.GetComponent<PlayerGrabAndThrow>().HasThrowableHead;

            if (Input.GetButtonDown(actionButtonName) && currentlyHasThrowable == false)
            {
                grabAndThrowScript.PickUpObject(other.gameObject);
            }
        }
    }


}
