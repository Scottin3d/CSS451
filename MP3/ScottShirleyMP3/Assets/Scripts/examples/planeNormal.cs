using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class planeNormal : MonoBehaviour
{
    public GameObject PtOnPlane;

    public float length;
    public float a;
    public float b;
    public float x;
    public Vector3 intersect;
    public Vector3 vector;


    private void Start() {
        PtOnPlane.GetComponent<Renderer>().material.color = Color.black;
    }


    private void Update() {
        DrawNormal(this.gameObject);

        Debug.DrawLine(this.transform.localPosition, PtOnPlane.transform.localPosition, Color.white);
        Debug.DrawLine(this.transform.localPosition, PtOnPlane.transform.position, Color.green);
        //in this case local is the same

        Vector3  intersection = Vector3.zero;

        //calculate the distance between the linePoint and the line-plane intersection point
        a = Vector3.Dot((transform.position - PtOnPlane.transform.position), transform.up);
        b = Vector3.Dot(PtOnPlane.transform.up, transform.up);

        x = a / b;
        intersect = x * PtOnPlane.transform.up + transform.position;

        Debug.DrawLine(this.transform.localPosition, PtOnPlane.transform.localPosition, Color.white);
        Debug.DrawLine(this.transform.localPosition, PtOnPlane.transform.position, Color.green);
        Debug.DrawLine(PtOnPlane.transform.position, intersect, Color.blue);
    }

    void DrawNormal(GameObject obj) {
        Vector3 pt = obj.transform.localPosition;
        pt.y += 2f;
        Debug.DrawLine(obj.transform.localPosition, pt, Color.red);
    }


}
