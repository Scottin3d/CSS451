using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class primitiveMovement : MonoBehaviour
{
    public bool xRotate = false;
    public float xRotateSpeed = 10f;

    public bool xBound = true;
    public float xMaxAngle = 90;
    float xAngle = 0f;

    public bool yRotate = false;
    public float yRotateSpeed = 10f;

    public bool yBound = true;
    public float yMaxAngle = 90;
    float yAngle = 0f;

    public bool zRotate = false;
    public float zRotateSpeed = 10f;

    public bool zBound = true;
    public float zMaxAngle = 90f;
    float zAngle = 0f;




    // Update is called once per frame
    void Update()
    {
        if (!xRotate) xAngle = 0f;
        if (!yRotate) yAngle = 0f;
        if (!zRotate) zAngle = 0f;

        // x
        if (xRotate) {
            if (xBound) {
                xAngle = xMaxAngle * Mathf.Sin(Time.time * xRotateSpeed);
            } else {
                xAngle = (xAngle <= 360f) ? xAngle + Time.deltaTime * xRotateSpeed : 0f;
            }
        }

        // y
        if (yRotate) {
            if (yBound) {
                yAngle = yMaxAngle * Mathf.Sin(Time.time * yRotateSpeed);
            } else {
                yAngle = (yAngle <= 360f) ? yAngle + Time.deltaTime * yRotateSpeed : 0f;
            }
        }

        // z
        if (zRotate) {
            if (zBound) {
                zAngle = zMaxAngle * Mathf.Sin(Time.time * zRotateSpeed);
            } else {
                zAngle = (zAngle <= 360f) ? zAngle + Time.deltaTime * zRotateSpeed : 0f;
            }
        }

        transform.rotation = Quaternion.Euler(xAngle, yAngle, zAngle);

    }
}
