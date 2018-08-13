using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{

    [SerializeField] float dampTime = 0.2f;
    [SerializeField] float screenEdgePadding = 4f;
    [SerializeField] float minimumSize = 6.5f;
    public Transform[] cameraTargets;

    private Camera cameraObject;
    private float zoomSpeed;
    private Vector3 movementVelocity;
    private Vector3 desiredPosition;

    // Use this for initialization  
    void Awake()
    {
        cameraObject = gameObject.GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MoveCamera();
        //ZoomCamera();
    }

    void MoveCamera()
    {
        FindAveragePosition();
        transform.position = Vector3.SmoothDamp(
            transform.position, desiredPosition,
            ref movementVelocity, dampTime);
    }

    private void FindAveragePosition()
    {
        Vector3 averagePosition = new Vector3();
        int numberOfTargets = 0;

        for (int i = 0; i < cameraTargets.Length; i++)
        {
            if (!cameraTargets[i].gameObject.activeSelf)
                continue;

            averagePosition += cameraTargets[i].position;
            numberOfTargets++;
        }

        if (numberOfTargets > 0)
            averagePosition /= numberOfTargets;

        averagePosition.y = transform.position.y;
        desiredPosition = averagePosition;
    }

    private void ZoomCamera()
    {
        float requiredSize = FindRequiredSize();
        cameraObject.fieldOfView =
            Mathf.SmoothDamp(cameraObject.fieldOfView,
            requiredSize, ref zoomSpeed, dampTime);
    }

    private float FindRequiredSize()
    {
        Vector3 desiredLocalPosition = transform.InverseTransformPoint(desiredPosition);
        float size = 0f;

        for (int i = 0; i < cameraTargets.Length; i++)
        {
            if (!cameraTargets[i].gameObject.activeSelf)
                continue;

            Vector3 targetLocalPosition = transform.InverseTransformPoint(cameraTargets[i].position);
            Vector3 desiredPosToTarget = targetLocalPosition - desiredLocalPosition;

            size = Mathf.Max(size, Mathf.Abs(desiredPosToTarget.y));
            size = Mathf.Max(size, Mathf.Abs(desiredPosToTarget.x) / cameraObject.aspect);
        }

        size += screenEdgePadding;
        size = Mathf.Max(size, minimumSize);

        return size;
    }

}
