using UnityEditor;
using UnityEngine;
public class objectNormal  : MonoBehaviour{
    GameObject normal;
    public float size = 4f;
    void Start() {
        normal = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        normal.transform.parent = transform;
        normal.GetComponent<MeshRenderer>().material.color = Color.red;
        Vector3 p1 = transform.parent.transform.localPosition;
        Vector3 p2 = p1;
        p2.y += 2f;


        Vector3 V = p2 - p1;
        float length = V.magnitude;

        transform.localPosition = p1 + 0.5f * V;
        transform.localRotation = Quaternion.FromToRotation(Vector3.up, V);
    }
}
