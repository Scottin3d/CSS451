using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

[ExecuteInEditMode]
public class MP4CameraControllerScript : MonoBehaviour
{
    [SerializeField]
    float cameraDistance;
    float cameraZoomSensitivity = 5f;
    Vector3 lookAtPosition;

    public GameObject cameraObj;

    private void Update() {
        lookAtPosition = transform.position;

        cameraDistance = Vector3.Distance(lookAtPosition, cameraObj.transform.position);

        if (Input.GetKey(KeyCode.LeftAlt)) {
            Debug.Log("Alt for zoom");
            cameraDistance -= Input.GetAxis("Mouse ScrollWheel") * cameraZoomSensitivity;
            Zoom();
        }
        
        LookAt();
    }

    void LookAt() {
        Vector3 lookAtDir = lookAtPosition - cameraObj.transform.position;
        cameraObj.transform.rotation = Quaternion.FromToRotation(Vector3.forward, lookAtDir);
        Debug.DrawLine(cameraObj.transform.position, lookAtPosition, Color.blue);
    }

    void Zoom() {
        Vector3 lookAtDir = lookAtPosition - cameraObj.transform.position;
        Vector3 cameraPos = -lookAtDir.normalized * cameraDistance + lookAtPosition;
        cameraObj.transform.position = cameraPos;
    }

    public void SetFOV(float fov) {
        Camera.main.fieldOfView = fov;
    }
}
