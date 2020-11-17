using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class primitiveMovement : MonoBehaviour
{
    public Vector3 pivot;
    public Vector3 eulerAngles;

    //[SerializeField]
    //public GameObject child;

    public bool xRotate = false;
    public float xRotateSpeed = 15f;

    public bool xBound = true;
    public float xMaxAngle = 90;
    float xAngle = 0f;

    public bool yRotate = false;
    public float yRotateSpeed = 15f;

    public bool yBound = true;
    public float yMaxAngle = 90;
    float yAngle = 0f;

    public bool zRotate = false;
    public float zRotateSpeed = 15f;

    public bool zBound = true;
    public float zMaxAngle = 90f;
    float zAngle = 0f;

    MeshFilter mf;
    Vector3[] origVerts;
    Vector3[] newVerts;

    void Start() {
        // Get the Mesh Filter component, save its original vertices
        // and make a new vertex array for processing.
        mf = GetComponent<MeshFilter>();
        origVerts = mf.mesh.vertices;
        newVerts = new Vector3[origVerts.Length];
    }

    // Update is called once per frame
    void Update()
    {
        //UpdatePivot();

        if (!xRotate) xAngle = 0f;
        if (!yRotate) yAngle = 0f;
        if (!zRotate) zAngle = 0f;

        // x
        if (xRotate) {
            if (xBound) {
                xAngle = xMaxAngle * Mathf.Sin(Time.time * xRotateSpeed / 4f);
            } else {
                xAngle = (xAngle <= 360f) ? xAngle + Time.deltaTime * xRotateSpeed: 0f;
            }
        }

        // y
        if (yRotate) {
            if (yBound) {
                yAngle = yMaxAngle * Mathf.Sin(Time.time * yRotateSpeed / 4f);
            } else {
                yAngle = (yAngle <= 360f) ? yAngle + Time.deltaTime * yRotateSpeed : 0f;
            }
        }

        // z
        if (zRotate) {
            if (zBound) {
                zAngle = zMaxAngle * Mathf.Sin(Time.time * zRotateSpeed / 4f);
            } else {
                zAngle = (zAngle <= 360f) ? zAngle + Time.deltaTime * zRotateSpeed : 0f;
            }
        }

        transform.rotation = Quaternion.Euler(xAngle, yAngle, zAngle);

        /*
        // Set a Quaternion from the specified Euler angles.
        Quaternion rotation = Quaternion.Euler(xAngle, yAngle, zAngle);

        // Set the translation, rotation and scale parameters.
        Matrix4x4 m = Matrix4x4.TRS(transform.localPosition, rotation, transform.localScale);

        // For each vertex...
        for (int i = 0; i < origVerts.Length; i++) {
            // Apply the matrix to the vertex.
            newVerts[i] = m.MultiplyPoint3x4(origVerts[i]);
        }

        // Copy the transformed vertices back to the mesh.
        mf.mesh.vertices = newVerts;
        */
    }

    void UpdatePivot() {
        Quaternion pRotation = Quaternion.Euler(0f, 0f, 0f);
        Vector3 pScale = Vector3.one;
        Matrix4x4 m = Matrix4x4.TRS(pivot, pRotation, pScale);
        // For each vertex...
        for (int i = 0; i < origVerts.Length; i++) {
            // Apply the matrix to the vertex.
            newVerts[i] = m.MultiplyPoint3x4(origVerts[i]);
        }

        // Copy the transformed vertices back to the mesh.
        mf.mesh.vertices = newVerts;
    }
}
