using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class MP2CameraControllerScript : MonoBehaviour
{
    [SerializeField]
    GameObject cameraControlObject;
    private Camera mainCamera;

    [SerializeField]
    Slider cameraRotation;
    private const float DEFAULT_ROT = 0;

    [SerializeField]
    Slider cameraFOV;
    private const float DEFUALT_FOV = 45;

    [SerializeField]
    Slider cameraViewAngle;
    private const float DEFAULT_VA = 22;


    private void Start() {
        InitializeComponents();
        Debug.Assert(cameraRotation != null);
        Debug.Assert(cameraFOV != null);
        Debug.Assert(cameraViewAngle != null);
    }

    void InitializeComponents() {
        if (!cameraControlObject) {
            cameraControlObject = GameObject.Find("CameraControlObject");
        }
        if (!mainCamera) {
            mainCamera = Camera.main;
        }
        if (!cameraRotation) {
            cameraRotation = GameObject.Find("CameraRotation").GetComponentInChildren<Slider>();
        }
        if (!cameraFOV) {
            cameraFOV = GameObject.Find("CameraFOV").GetComponentInChildren<Slider>();
        }
        if (!cameraViewAngle) {
            cameraViewAngle = GameObject.Find("CameraViewAngle").GetComponentInChildren<Slider>();
        }
    }

    public void ResetTransform() {
        cameraControlObject.transform.position = Vector3.zero;
        cameraRotation.value = DEFAULT_ROT;
        cameraControlObject.transform.eulerAngles = Vector3.zero;
        cameraFOV.value = DEFUALT_FOV;
        mainCamera.fieldOfView = DEFUALT_FOV;
        cameraViewAngle.value = DEFAULT_VA;
    }

    public void SetRotation(float rot) { 
        cameraControlObject.transform.eulerAngles = new Vector3(cameraControlObject.transform.eulerAngles.x, 
                                                                rot,
                                                                cameraControlObject.transform.eulerAngles.z);
    }
    public void SetFOV(float fov) {
        mainCamera.fieldOfView = fov;
    }

    public void SetViewAngle(float ang) {
        cameraControlObject.transform.eulerAngles = new Vector3(ang, 
                                                    cameraControlObject.transform.eulerAngles.y, 
                                                    cameraControlObject.transform.eulerAngles.z);
    }
}
