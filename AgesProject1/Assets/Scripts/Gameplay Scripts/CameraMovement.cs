using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class CameraMovement : MonoBehaviour {

    public Transform[] cameraTargets;
    Vector3 desiredPosition;
    [SerializeField] float cameraZOffset = -8f;

    //For camera movement
    Vector3 movementVelocity;
    float dampTime = 0.2f;

    //For camera zoom
    float screenPadding = 4f;
    float minimumDistance;


    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        MoveCamera();
        //ZoomCamera();
	}

    void MoveCamera()
    {
        desiredPosition = FindCameraMovePosition();
        transform.position = Vector3.SmoothDamp(
            transform.position, desiredPosition,
            ref movementVelocity, dampTime);
    }

    Vector3 FindCameraMovePosition()
    {
        Vector3 newCameraPosition;
        float sameYPosition = transform.position.y;

        //Average X position
        float averageXPosition = 0;
        foreach (Transform target in cameraTargets)
        {
            averageXPosition += target.position.x;
        }
        averageXPosition /= cameraTargets.Length;
        Debug.Log("Average X Pos = " + averageXPosition);

        //Average Z position
        float averageZPosition = 0;
        foreach (Transform target in cameraTargets)
        {
            averageZPosition += target.position.z;
        }
        averageZPosition /= cameraTargets.Length;
        averageZPosition += cameraZOffset;
        Debug.Log("Average Z Pos = " + averageZPosition);

        newCameraPosition = new Vector3 (averageXPosition, sameYPosition, averageZPosition);

        return newCameraPosition;
    }

    //void ZoomCamera()
    //{
    //    desiredPosition = FindCameraZoomPosition();
    //    transform.position = Vector3.SmoothDamp(
    //        transform.position, desiredPosition,
    //        ref movementVelocity, dampTime);
    //}

    //Vector3 FindCameraZoomPosition()
    //{
    //    //Get point the camera is looking at
    //    Vector3 pointOfFocus = new Vector3 (desiredPosition.x, 0, desiredPosition.z - cameraZOffset);
    //    //See which player is furthest away from the point of focus
    //    float furthestDistance = 0;
    //    foreach (Transform target in cameraTargets)
    //    {
    //        //We want to make sure there is no height differance between the two points
    //        Vector3 targetPosition = new Vector3(target.position.x, 0, target.position.z);
    //        float distanceBetweenPoints = Mathf.Abs(Vector3.Distance(pointOfFocus, targetPosition));
    //        if (distanceBetweenPoints > furthestDistance)
    //        {
    //            //Updates furthestDistance only if the distance between points is greater
    //            furthestDistance = distanceBetweenPoints;
    //        }
    //    }

    //    return desiredPosition;
    //}
}
