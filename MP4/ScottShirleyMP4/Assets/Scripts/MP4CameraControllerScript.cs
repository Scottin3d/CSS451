using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;


public class MP4CameraControllerScript : MonoBehaviour {
    [SerializeField]
    float cameraDistance;
    public float cameraZoomSensitivity = 10f;

    public GameObject lookAtObj;
    Vector3 lookAtPosition;

    Vector3 cameraPosition;

    public Vector3 mouseOrigin;
    public float mouseDistance;
    float panSpeed = 0.5f;
    float turnSpeed = 20f;  
    public bool isAlt = false;
    public bool isPanning;
    public bool isRotating;
    public bool isZooming = false;

    void FixedUpdate() {
        cameraPosition = transform.position;
        lookAtPosition = lookAtObj.transform.position;

        cameraDistance = Vector3.Distance(cameraPosition, lookAtPosition);
        // check alt key pressed
        if (Input.GetKey(KeyCode.LeftAlt)) {
            // rotate
            if (Input.GetMouseButtonDown(0)) {
                mouseOrigin = Input.mousePosition;
                isRotating = true;
            }

            // pan
            if (Input.GetMouseButton(1)) {
                isPanning = true;
            }

            // zoom
            cameraDistance -= Input.GetAxis("Mouse ScrollWheel") * cameraZoomSensitivity;
            t_Zoom();
        }
        if (Input.GetMouseButton(0)) isRotating = true;
        if (Input.GetMouseButton(1) && !isPanning) {
            mouseOrigin = Input.mousePosition;
            isPanning = true; 
        }

        // Disable movements on button release
        if (!Input.GetKeyUp(KeyCode.LeftAlt)) isAlt = false;
        if (!Input.GetMouseButton(0)) isRotating = false;
        if (!Input.GetMouseButton(1)) isPanning = false;

        // Move the camera on it's XY plane
        if (isPanning) {
            t_Pan();
            /*
            Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - mouseOrigin);
            Vector3 move = new Vector3(pos.x * panSpeed, pos.y * panSpeed, 0);
            transform.Translate(move, Space.Self);
            lookAtObj.transform.Translate(move, Space.Self);
            */
        }

        // Rotate camera along X and Y axis
        if (isRotating) {
        }
        LookAt();
    }

    void LookAt() {
        Vector3 lookAtDir = lookAtPosition - cameraPosition;
        transform.rotation = Quaternion.FromToRotation(Vector3.forward, lookAtDir);
        Debug.DrawLine(lookAtPosition, cameraPosition, Color.blue);
    }

    void t_Zoom() {
        Vector3 camPos = transform.position;
        float distance = Vector3.Distance(lookAtPosition, cameraPosition);
        camPos += transform.forward * cameraZoomSensitivity * Input.mouseScrollDelta.y * Time.deltaTime;
        transform.position = camPos;
    }

    public void SetFOV(float fov) {
        Camera.main.fieldOfView = fov;
    }

    void t_Pan() {
        Vector3 direction = Input.mousePosition - mouseOrigin;
        mouseDistance = Vector3.Distance(Input.mousePosition, mouseOrigin);
        Vector3 posMove = -direction.normalized * mouseDistance * 0.05f;
        //transform.position = posMove;
        lookAtObj.transform.position = posMove;
    }

    void RotateAround() {
        Vector3 viewDirection = lookAtPosition - cameraPosition;
        Vector3 xRef = Vector3.Cross(transform.up, viewDirection);
        Vector3.Normalize(xRef);
        Vector3 yRef = Vector3.up;
    }
}

//unity pivoty transform
