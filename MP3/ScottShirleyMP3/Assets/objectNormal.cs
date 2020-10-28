using UnityEditor;
using UnityEngine;
public class objectNormal  : MonoBehaviour{

    

    GameObject normal; 
    void Start() {
        normal = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        normal.transform.parent = transform;
        normal.GetComponent<MeshRenderer>().material.color = Color.red;
        normal.transform.position = transform.position;
        normal.transform.localScale = new Vector3(0.1f, 1f, 0.1f);
        normal.transform.rotation = Quaternion.FromToRotation(Vector3.up, transform.up);
    }

    void Update() { 
    
    }

    
}
