using UnityEditor;
using UnityEngine;
public class objectNormal  : MonoBehaviour{
    GameObject normal;
    public float size = 4f;
    void Start() {
        normal = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        normal.transform.parent = transform;
        normal.GetComponent<MeshRenderer>().material.color = Color.red;
        Vector3 pos = transform.position;
        pos.y = size;
        normal.transform.localPosition = pos;
        normal.transform.localScale = new Vector3(0.2f, size, 0.2f);
        normal.transform.rotation = Quaternion.FromToRotation(Vector3.up, transform.up);
    }
}
