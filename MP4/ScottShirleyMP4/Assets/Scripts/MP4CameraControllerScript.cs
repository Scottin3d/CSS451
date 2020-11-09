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
    const float DEFAULT_CAMERADISTANCE = 12f;
    float cameraZoomSensitivity = 5f;
    float zoomAmount = 0f;

    Vector3 DEFAULT_LOOKATPOS = new Vector3(0f, 2f, 3f);
    Vector3 lookAtPosition;

    public GameObject cameraObj;

    Slider cameraFOV;
    private const float DEFUALT_FOV = 45;

    Vector3 DEFAULT_CAMERAPOS = new Vector3();

    private void Awake() {
        //mainCamera = Camera.main;
    }

    private void Update() {
        lookAtPosition = transform.position;

        cameraDistance = Vector3.Distance(lookAtPosition, cameraObj.transform.position);

        if (Input.GetKey(KeyCode.LeftAlt)) {
            Debug.Log("Alt for zoom");
            cameraDistance -= Input.GetAxis("Mouse ScrollWheel") * cameraZoomSensitivity;
            Zoom();
        }

        if (Input.GetKeyUp(KeyCode.LeftAlt)) {
            zoomAmount = 0;
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

#region 
    public void ResetTransform() {
        transform.position = DEFAULT_LOOKATPOS;
        transform.rotation = Quaternion.FromToRotation(transform.rotation.eulerAngles, Vector3.up);
        cameraFOV.value = DEFUALT_FOV;
        cameraDistance = DEFAULT_CAMERADISTANCE;
    }
    public void SetFOV(float fov) {
        Camera.main.fieldOfView = fov;
    }
 #endregion
}
