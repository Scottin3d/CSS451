using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MP4xFormScript : MonoBehaviour {
    // Translate, rotate and scale a mesh. Try altering
    // the parameters in the inspector while running
    // to see the effect they have.

    public Vector3 translation;
    public Vector3 eulerAngles;
    public Vector3 scale = new Vector3(1, 1, 1);

    public Vector3 pivot = Vector3.zero;

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


    void Update() {
        //update pivot
        UpdatePivot();


        // Set a Quaternion from the specified Euler angles.
        Quaternion rotation = Quaternion.Euler(eulerAngles.x, eulerAngles.y, eulerAngles.z);

        // Set the translation, rotation and scale parameters.
        Matrix4x4 m = Matrix4x4.TRS(translation, rotation, scale);

        // For each vertex...
        for (int i = 0; i < origVerts.Length; i++) {
            // Apply the matrix to the vertex.
            newVerts[i] = m.MultiplyPoint3x4(origVerts[i]);
        }

        // Copy the transformed vertices back to the mesh.
        mf.mesh.vertices = newVerts;
    }

    void UpdatePivot() {
        Quaternion pRotation = Quaternion.Euler(0f,0f,0f);
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
