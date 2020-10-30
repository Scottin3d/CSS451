using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;

[ExecuteInEditMode]
public class planeNormal : MonoBehaviour {
    /* References:
     * https://mathworld.wolfram.com/Point-LineDistance3-Dimensional.html
     * https://www.scratchapixel.com/lessons/3d-basic-rendering/minimal-ray-tracer-rendering-simple-shapes/ray-plane-and-ray-disk-intersection
     */
    public bool showAllObjects = false;

    public GameObject PtOnPlane;
    public GameObject plane2;
    public GameObject ShadowBlob;

    [Header("Point Objects")]
    public GameObject pointI;
    public GameObject pointM;
    public GameObject pointN;
    public GameObject pointO;
    public GameObject pointP;
    public GameObject pointQ;
    public GameObject pointHeightLock;
    public GameObject pointL;


    public float radius = 1f;
    public float length;
    public float a;
    public float b;
    public float x;
    public Vector3 intersect;
    public Vector3 vector;
    public Vector3 cylinderIntersect;
    public float Distance;

    [Header("Cylinder Values")]
    public GameObject cylinder;
    public float distanceFromCylinderPoint;
    public float cylinderHeight;
    public float cylinderHeightDifference;
    [Header("Cylinder Lengths")]
    public float lengthCylinder2Intersect;
    public float lengthLine2Intersect;
    public float lengthLine2Cylinder;
    public float cylinderRadius;
    [Header("Cylinder Angles")]
    public float angleA;
    public float angleB;
    public float angleC;
    [Header("Cylinder Angles Radians")]
    public float angleArad;
    public float angleBrad;
    public float angleCrad;
    [Header("Cylinder Point I Lengths")]
    public float lengthA;
    public float lengthB;
    public float lengthC;
    [Header("Cylinder Plane Angles")]
    public float angleIA;
    public float angleIB;
    public float angleIC;
    [Header("Cylinder Plane Lengths")]
    public float lengthIA;
    public float lengthIB;
    public float lengthIC;
    [Header("Cylinder Intersection Lengths")]
    public float lengthGreenA;
    public float lengthGreenB;
    public float lengthGreenC;
    [Header("Cylinder Intersection Angles")]
    public float angleGreenA;
    public float angleGreenB;
    public float angleGreenC;




    public float angleDeg;


    private void Awake() {
        ShadowBlob = Instantiate(Resources.Load("Prefabs/projection") as GameObject, this.transform);
    }


    private void Update() {
        DrawNormal(this.gameObject);
        pointP.transform.position = PtOnPlane.transform.position;
        //Debug.DrawLine(this.transform.localPosition, PtOnPlane.transform.localPosition, Color.white);
        //Debug.DrawLine(this.transform.localPosition, PtOnPlane.transform.position, Color.green);

        Vector3 n = -transform.up;
        Vector3 lineDir = PtOnPlane.transform.up;
        Vector3 linePt = PtOnPlane.transform.position;
        Vector3 cylinderPoint = cylinder.transform.localPosition;
        //cylinderPoint.Normalize();
        cylinderRadius = cylinder.transform.localScale.x / 2f;
        Vector3 cylinderTop = cylinderPoint;
        Vector3 cylinderBottom = cylinderPoint;
        cylinderTop.y += cylinder.transform.localScale.y;
        cylinderBottom.y -= cylinder.transform.localScale.y;
        Vector3 cylinderNormal = -cylinder.transform.up;
        Vector3 plane2Pt = plane2.transform.position;
        Vector3 plane2Normal = -plane2.transform.forward;

        if (CylinderCast(out cylinderIntersect, linePt, lineDir, cylinderNormal, cylinderPoint, 0.5f)) {
            pointQ.GetComponent<MeshRenderer>().enabled = true;
        } else { 
            pointQ.GetComponent<MeshRenderer>().enabled = false;
        }

        pointI.GetComponent<MeshRenderer>().enabled = showAllObjects;
        pointM.GetComponent<MeshRenderer>().enabled = showAllObjects;
        pointN.GetComponent<MeshRenderer>().enabled = showAllObjects;
        pointO.GetComponent<MeshRenderer>().enabled = showAllObjects;
        pointP.GetComponent<MeshRenderer>().enabled = showAllObjects;
        pointL.GetComponent<MeshRenderer>().enabled = showAllObjects;
        
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
                                 Vector3 cylinderNormal, Vector3 cylinderPoint, float radius) {
        intersection = Vector3.zero;
        
        // Orthogonal
        Vector3 linePlaneOrthogonal;
        if (ScottCast(out linePlaneOrthogonal, linePoint, lineNormal, cylinderNormal, cylinderPoint)) {
            pointI.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
            pointI.transform.position = linePlaneOrthogonal;
            pointI.transform.rotation = Quaternion.FromToRotation(Vector3.up, lineNormal);
        }

        distanceFromCylinderPoint = Vector3.Distance(linePlaneOrthogonal, cylinderPoint);
        

        // line2cylinderPlane
        Vector3 lineCylinderNormalPoint;
        if (ScottCast(out lineCylinderNormalPoint, linePoint, -cylinderNormal, cylinderNormal, cylinderPoint)) {
        }

        // plane triangle lengths (blue)
        lengthCylinder2Intersect = Vector3.Distance(linePlaneOrthogonal, cylinderPoint);
        lengthLine2Cylinder = Vector3.Distance(linePoint, cylinderPoint);
        lengthLine2Intersect = Vector3.Distance(linePoint, linePlaneOrthogonal);

        // line to cylinder trianlge (red)
        // ==================================
        // red angles
        angleA = Vector3.Angle((lineCylinderNormalPoint - cylinderPoint), (linePoint - cylinderPoint));
        angleArad = angleA * Mathf.Deg2Rad;
        angleB = 90f;
        angleBrad = angleB * Mathf.Deg2Rad;
        angleC = 180f - angleB - angleA;
        angleCrad = angleC * Mathf.Deg2Rad;

        // red lengths
        // a = c·sin(A)/sin(C) 
        lengthA = radius * Mathf.Sin(angleArad) / Mathf.Sin(angleCrad);
        // b = c·sin(B)/sin(C)
        lengthB = radius * Mathf.Sin(angleBrad) / Mathf.Sin(angleCrad);
        lengthC = radius;
        // ==================================

        // line to intersect triangle (magenta)
        // ==================================
        // magenta lengths
        // a = radius
        lengthIA = radius;
        // b = a·sin(B)/sin(A) = 0.37067
        lengthIB = Vector3.Distance(cylinderPoint, linePlaneOrthogonal);


        // magenta angles
        angleIA = Vector3.Angle((cylinderPoint - linePlaneOrthogonal), (lineCylinderNormalPoint - linePlaneOrthogonal));
        float angleIArad = angleIA * Mathf.Deg2Rad;
        // B = arcsin( (b * sin(S)) / a)
        float t = Mathf.Sin(angleIArad) * lengthIB;
        t /= lengthIA;
        float angleIBrad = Mathf.Asin(t);

        angleIB = angleIBrad / Mathf.Deg2Rad;
        // C = 180° - A - B
        angleIC = 180f - angleIA - angleIB;
        float angleICrad = angleIC * Mathf.Deg2Rad;

        // c = a·sin(C) / sin(A) = 0.21589
        lengthIC = lengthIA * Mathf.Sin(angleICrad) / Mathf.Sin(angleIArad);
        // ==================================

        // pointL
        // line intersection with cylinder
        // targetDirection = target = position
        Vector3 lDir = lineCylinderNormalPoint - linePlaneOrthogonal;
        Vector3 lPoint = lDir.normalized * lengthIC + linePlaneOrthogonal;
        pointL.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
        pointL.transform.position = lPoint;
        pointL.transform.rotation = Quaternion.FromToRotation(Vector3.up, lDir);

        // pointM
        // cylinder2intersect
        Vector3 mDir = linePlaneOrthogonal - cylinderPoint;
        Vector3 mPoint = mDir.normalized * radius + cylinderPoint;
        pointM.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
        pointM.transform.position = mPoint;
        pointM.transform.rotation = Quaternion.FromToRotation(Vector3.up, mDir);

        /* pointN
        // line2cylinder
        Vector3 nDir = -cylinderNormal;
        Vector3 nPoint = nDir.normalized * lengthA + oPoint;
        pointN.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
        pointN.transform.position = nPoint;
        pointN.transform.rotation = Quaternion.FromToRotation(Vector3.up, nDir);
        Debug.DrawLine(oPoint, nPoint, Color.red);
        */

        // pointO
        // line2cylinder
        Vector3 oDir = lineCylinderNormalPoint - cylinderPoint;
        Vector3 oPoint = oDir.normalized * radius + cylinderPoint;
        pointO.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
        pointO.transform.position = oPoint;
        pointO.transform.rotation = Quaternion.FromToRotation(Vector3.up, oDir);

        // ==================================
        // green angles
        angleGreenA = Vector3.Angle((linePoint - linePlaneOrthogonal),(lineCylinderNormalPoint - linePlaneOrthogonal));
        float angleGreedArad = angleGreenA * Mathf.Deg2Rad;
        angleGreenB = 90f;
        float angleGreedBrad = angleGreenB * Mathf.Deg2Rad;
        angleGreenC = 180f - angleGreenA - angleGreenB;
        float angleGreedCrad = angleGreenC * Mathf.Deg2Rad;

        // green lengths
        // distance between orthogonal and plane intersect
        lengthGreenC = Vector3.Distance(linePlaneOrthogonal, lPoint);
        // a = c·sin(A)/sin(C)
        lengthGreenA = lengthGreenC * Mathf.Sin(angleGreedArad) / Mathf.Sin(angleGreedCrad);
        // b = c·sin(B)/sin(C)
        lengthGreenB = lengthGreenC * Mathf.Sin(angleGreedBrad) / Mathf.Sin(angleGreedCrad); ;
        // ==================================

        // pointQ
        // line cylinder intersection

        Vector3 qDir = -cylinderNormal;
        Vector3 qPoint = qDir.normalized * lengthGreenA + lPoint;
        pointQ.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
        pointQ.transform.position = qPoint;
        pointQ.transform.rotation = Quaternion.FromToRotation(Vector3.up, qDir);

        // pointHeightLock
        // only stays withing the bounds of the cylinder
        

        // magenta lines
        Debug.DrawLine(cylinderPoint, lPoint, Color.magenta);
        Debug.DrawLine(cylinderPoint, mPoint, Color.magenta);
        Debug.DrawLine(cylinderPoint, oPoint, Color.magenta);
        Debug.DrawLine(cylinderPoint, qPoint, Color.magenta);

        // line2intersect triangle
        Debug.DrawLine(linePoint, linePlaneOrthogonal, Color.green);
        Debug.DrawLine(linePlaneOrthogonal, cylinderPoint, Color.green);
        Debug.DrawLine(lineCylinderNormalPoint, linePlaneOrthogonal, Color.green);
        Debug.DrawLine(lPoint, qPoint, Color.green);

        // line2cylinder triangle
        Debug.DrawLine(linePoint, cylinderPoint, Color.red);
        Debug.DrawLine(linePoint, lineCylinderNormalPoint, Color.red);
        Debug.DrawLine(cylinderPoint, lineCylinderNormalPoint, Color.red);


       
        cylinderHeight = cylinder.transform.localScale.y * 2f;
        cylinderHeightDifference = qPoint.y - cylinderPoint.y;
        if (cylinderHeightDifference > cylinderHeight / 2f) {
            Debug.Log("Outside of cylidner");
            return false;
        }
        intersection = qPoint;
        
        
        return true;
    }
}
