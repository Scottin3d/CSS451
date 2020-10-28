using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class MP2CameraControllerScript : MonoBehaviour
{
    [SerializeField]
    GameObject cameraControlObject;
    Camera mainCamera;

    [SerializeField]
    Slider cameraRotstion;
    float defaultRot = 0;

    [SerializeField]
    Slider cameraFOV;
    float defaultFOV = 60;

    [SerializeField]
    Slider cameraViewAngle;
    float defaultAng = 0;


    private void Start() {
        if (!cameraControlObject) {
            cameraControlObject = GameObject.Find("CameraControlObject");
        }
        if (!mainCamera) {
            mainCamera = Camera.main;
        }
        Debug.Assert(cameraRotstion != null);
        Debug.Assert(cameraFOV != null);
        Debug.Assert(cameraViewAngle != null);
    }

    public void ResetTransform() {
        cameraControlObject.transform.position = Vector3.zero;
        cameraRotstion.value = defaultRot;
        cameraControlObject.transform.eulerAngles = Vector3.zero;
        cameraFOV.value = defaultFOV;
        mainCamera.fieldOfView = defaultFOV;
        cameraViewAngle.value = defaultAng;
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
