using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class planeNormal : MonoBehaviour
{
    /* References:
     * https://mathworld.wolfram.com/Point-LineDistance3-Dimensional.html
     * https://www.scratchapixel.com/lessons/3d-basic-rendering/minimal-ray-tracer-rendering-simple-shapes/ray-plane-and-ray-disk-intersection
     */
    public GameObject PtOnPlane;
    public GameObject ShadowBlob;

    public float radius = 2f;
    public float length;
    public float a;
    public float b;
    public float x;
    public Vector3 intersect;
    public Vector3 vector;

    public float Distance;

    private void Start() {
        ShadowBlob = Instantiate(Resources.Load("Prefabs/projection") as GameObject, this.transform);
    }


    private void Update() {
        DrawNormal(this.gameObject);
       
        Debug.DrawLine(this.transform.localPosition, PtOnPlane.transform.localPosition, Color.white);
        Debug.DrawLine(this.transform.localPosition, PtOnPlane.transform.position, Color.green);

        Vector3 n = -transform.up;
        Vector3 lineDir = PtOnPlane.transform.up;
        Vector3 linePt = PtOnPlane.transform.position;
        float length = 0;

        if (ScottCast(out intersect, linePt, -n, n, transform.position, out length)) { 
            Debug.DrawLine(PtOnPlane.transform.position, intersect, Color.black);

            //Distance = Vector3.Distance(intersect, transform.position);
            Distance = (intersect - transform.position).magnitude;
            if (Distance >= radius) {
                ShadowBlob.GetComponent<MeshRenderer>().enabled = false;
            } else {
                ShadowBlob.GetComponent<MeshRenderer>().enabled = true;
            }

            ShadowBlob.transform.rotation = Quaternion.FromToRotation(Vector3.up, n);
            Vector3 pos = intersect;
            pos.y += 0.1f;
            ShadowBlob.transform.position = pos;
        }

        if (ScottCast(out intersect, linePt, lineDir, n, transform.position, out length)) {
            Debug.DrawLine(PtOnPlane.transform.position, intersect, Color.blue);
        }
    }

    void DrawNormal(GameObject obj) {
        Vector3 pt = obj.transform.localPosition;
        pt.y += 2f;
        Debug.DrawLine(obj.transform.localPosition, pt, Color.red);
    }

    public bool ScottCast(out Vector3 intersection, Vector3 linePoint, Vector3 lineVec, Vector3 planeNormal, Vector3 planePoint, out float length) {
        
        length = 0;
        float dotNumerator;
        float dotDenominator;
        Vector3 vector;
        intersection = Vector3.zero;

        //calculate the distance between the linePoint and the line-plane intersection point
        dotNumerator = Vector3.Dot((planePoint - linePoint), planeNormal);
        dotDenominator = Vector3.Dot(lineVec, planeNormal);

        if (dotDenominator != 0.0f) {
            length = dotNumerator / dotDenominator;

            vector = lineVec.normalized * length;

            //vector = SetVectorLength(lineVec, length);

            intersection = linePoint + vector;
            return true;
        } else
            return false;
    }
}
