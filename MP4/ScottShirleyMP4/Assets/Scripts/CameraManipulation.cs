using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManipulation : MonoBehaviour {


    public enum LookAtCompute {
        QuatLookRotation = 0,
        TransformLookAt = 1
    };

    public bool altKey = false;

    public float panSpeed = 2f;
    float MIN_CAMERA_DIST = 1.5f;
    //debugging
    [Header("Debug Values")]
    public float distnace;
    public float mouseDistance;
    public float mouseScrollDelta;
    

    public Transform LookAtPosition = null;
   // public LineSegment LineOfSight = null;
    public LookAtCompute ComputeMode = LookAtCompute.QuatLookRotation;


    Vector3 delta = Vector3.zero;
    Vector3 mouseDownPos = Vector3.zero;


	// Update is called once per frame
	void Update () {
        altKey = false;


        switch (ComputeMode)
        {
            case LookAtCompute.QuatLookRotation:
                // Viewing vector is from transform.localPosition to the lookat position
                Vector3 V = LookAtPosition.localPosition - transform.localPosition;
                Vector3 W = Vector3.Cross(-V, transform.up);
                Vector3 U = Vector3.Cross(W, -V);
                transform.localRotation = Quaternion.LookRotation(V, U);
                break;

            case LookAtCompute.TransformLookAt:
                transform.LookAt(LookAtPosition);
                break;
        }

        if (Input.GetKey(KeyCode.LeftAlt)) {
            altKey = true;
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)) {
                mouseDownPos = Input.mousePosition;
                delta = Vector3.zero;
            } 
            // tumble
            if (Input.GetMouseButton(0)) {
                delta = mouseDownPos - Input.mousePosition;
                mouseDownPos = Input.mousePosition;
                t_Tumble(delta.x, transform.up);
                t_Tumble(delta.y, transform.right);

            }
            // track
            if (Input.GetMouseButton(1)) {
                delta = mouseDownPos - Input.mousePosition;
                mouseDownPos = Input.mousePosition;
                t_Pan(delta);
            }
            // zoom
            if (Input.mouseScrollDelta.y != 0) {
                t_Zoom(Input.mouseScrollDelta.y);
            }
        }
	}

    public void t_Zoom(float delta)
    {
        Vector3 v = LookAtPosition.localPosition - transform.localPosition;
        distnace = v.magnitude;
        // this will set a mininum zoom in distance
        // -delta otherwise scroll wheel will be inverted
        distnace = (distnace >= MIN_CAMERA_DIST) ? distnace += -delta : MIN_CAMERA_DIST; 
        transform.localPosition = LookAtPosition.localPosition - distnace * v.normalized;
    }

    void t_Pan(Vector3 delta) {
        Vector3 posLA = LookAtPosition.transform.localPosition;
        posLA.z += delta.y / panSpeed;
        posLA.x += delta.x / panSpeed;
        LookAtPosition.transform.localPosition = posLA;

        Vector3 posC = transform.localPosition;
        posC.z += delta.y / panSpeed;
        posC.x += delta.x / panSpeed;
        transform.localPosition = posC;
    }

    float Direction = 1f;
    void t_Tumble(float delta, Vector3 direction) {

        // orbit with respect to the transform.right axis

        // 1. Rotation of the viewing direction by right axis
        Quaternion q = Quaternion.AngleAxis(delta, direction);
        // 2. we need to rotate the camera position
        Matrix4x4 r = Matrix4x4.TRS(Vector3.zero, q, Vector3.one);
        Matrix4x4 invP = Matrix4x4.TRS(-LookAtPosition.localPosition, Quaternion.identity, Vector3.one);
        r = invP.inverse * r * invP;
        Vector3 newCameraPos = r.MultiplyPoint(transform.localPosition);
        transform.localPosition = newCameraPos;
        transform.LookAt(LookAtPosition);

        if (Mathf.Abs(Vector3.Dot(newCameraPos.normalized, Vector3.up)) > 0.7071f) // this is about 45-degrees
        {
            Direction *= -1f;
        }
    }

    public void SetFOV(float fov) {
        Camera.main.fieldOfView = fov;
    }
}
