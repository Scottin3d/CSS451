using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class planeNormal : MonoBehaviour {
    /* References:
     * https://mathworld.wolfram.com/Point-LineDistance3-Dimensional.html
     * https://www.scratchapixel.com/lessons/3d-basic-rendering/minimal-ray-tracer-rendering-simple-shapes/ray-plane-and-ray-disk-intersection
     */
    public GameObject PtOnPlane;
    public GameObject plane2;
    public GameObject cylinder;
    public GameObject ShadowBlob;

    public float radius = 2f;
    public float length;
    public float a;
    public float b;
    public float x;
    public Vector3 intersect;
    public Vector3 vector;
    public Vector3 cylinderIntersect;

    public float angleRad;
    public float angleDeg;

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
        Vector3 cylinderPoint = cylinder.transform.localPosition;
        //cylinderPoint.Normalize();
        Vector3 cylinderTop = cylinderPoint;
        Vector3 cylinderBottom = cylinderPoint;
        cylinderTop.y += cylinder.transform.localScale.y;
        cylinderBottom.y -= cylinder.transform.localScale.y;

        Vector3 cylinderNormal = -cylinder.transform.up;
        //cylinderNormal.Normalize();


        Vector3 plane2Pt = plane2.transform.position;
        Vector3 plane2Normal = -plane2.transform.forward;

        float length = 0;

        /*
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
        */

        if (ScottCast(out intersect, linePt, lineDir, n, transform.position, length)) {
            //Debug.DrawLine(PtOnPlane.transform.position, intersect, Color.blue);
        }

        Vector3[] points = new Vector3[2];


        if (CylinderCast(out cylinderIntersect, linePt,lineDir,cylinderNormal,cylinderPoint,cylinderBottom,cylinderTop, cylinder.transform.localScale.y /2f)) {
            Debug.DrawLine(cylinderIntersect, linePt, Color.blue);
        }
        
        if (ScottCast(out cylinderIntersect, linePt, lineDir, cylinderNormal, cylinderPoint, length)) {
            //Debug.DrawLine(cylinderIntersect, linePt, Color.red);
           //Debug.DrawLine(cylinderPoint, linePt, Color.red);
            //Debug.DrawLine(cylinderPoint, cylinderIntersect, Color.blue);

            //Vector3 targetDir = linePt - cylinderPoint;
            //angleDeg = Vector3.Angle(targetDir, cylinder.transform.forward);
        }
    }

    void Lines() { 
    
    }

    void DrawNormal(GameObject obj) {
        Vector3 pt = obj.transform.localPosition;
        pt.y += 2f;
        Debug.DrawLine(obj.transform.localPosition, pt, Color.red);
    }

    public bool ScottCast(out Vector3 intersection, Vector3 linePoint, Vector3 lineNormal, Vector3 planeNormal, Vector3 planePoint, float length = 1f) {

        length = 0;
        float dotNumerator;
        float dotDenominator;
        Vector3 vector;
        intersection = Vector3.zero;

        //calculate the distance between the linePoint and the line-plane intersection point
        dotNumerator = Vector3.Dot((planePoint - linePoint), planeNormal);
        dotDenominator = Vector3.Dot(lineNormal, planeNormal);

        if (dotDenominator != 0.0f) {
            length = dotNumerator / dotDenominator;

            vector = lineNormal.normalized * length;

            //vector = SetVectorLength(lineVec, length);

            intersection = linePoint + vector;
            return true;
        } else
            return false;
    }

    public bool CylinderCast(out Vector3 intersection, Vector3 linePoint, Vector3 lineNormal, 
                                 Vector3 cylinderNormal, Vector3 cylinderPoint, Vector3 cylinderBottom, Vector3 cylinderTop, float radius) {
        Vector3 CD;
        Vector3 AD;
        if (ScottCast(out CD, linePoint, lineNormal, cylinderNormal, cylinderPoint)) { }
        if (ScottCast(out AD, linePoint, lineNormal, cylinderNormal, cylinderBottom)) { }

        float num = Vector3.Dot(CD, AD);
        float dem = Vector3.Dot(AD, CD);
        intersection = (num / dem) * AD;
        intersection = CD - intersection;
        Debug.DrawLine(intersection, linePoint, Color.blue);

        return false;
    }

    public bool IntersectionPoint(out Vector3[] points, Vector3 p1, Vector3 p2, Vector3 center, float radius) {
        Vector3 dp = new Vector3();
        Vector3[] sect;
        float a, b, c;
        float bb4ac;
        float mu1;
        float mu2;

        //  get the distance between X and Z on the segment
        dp.x = p2.x - p1.x;
        dp.z = p2.z - p1.z;
        //   I don't get the math here
        a = dp.x * dp.x + dp.z * dp.z;
        b = 2 * (dp.x * (p1.x - center.x) + dp.z * (p1.z - center.z));
        c = center.x * center.x + center.z * center.z;
        c += p1.x * p1.x + p1.z * p1.z;
        c -= 2 * (center.x * p1.x + center.z * p1.z);
        c -= radius * radius;
        bb4ac = b * b - 4 * a * c;
        if (Mathf.Abs(a) < float.Epsilon || bb4ac < 0) {
            //  line does not intersect
            points = new Vector3[] { Vector3.zero, Vector3.zero };
            return false;
        }
        mu1 = (-b + Mathf.Sqrt(bb4ac)) / (2 * a);
        mu2 = (-b - Mathf.Sqrt(bb4ac)) / (2 * a);
        sect = new Vector3[2];
        sect[0] = new Vector3(p1.x + mu1 * (p2.x - p1.x), 0, p1.z + mu1 * (p2.z - p1.z));
        sect[1] = new Vector3(p1.x + mu2 * (p2.x - p1.x), 0, p1.z + mu2 * (p2.z - p1.z));

        points = sect;
        return true;
    }

    

}
