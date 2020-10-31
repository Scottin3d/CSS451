using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class cylinderIntersection : MonoBehaviour
{
    public bool showAllObjects = false;

    [Header("Line")]
    public GameObject lineObject;
    public Vector3 linePoint;
    public Vector3 lineDirection;
    public Vector3 lineNormal;
    public Vector3 interectPoint;

    [Header("Point Objects")]
    public GameObject pointI;
    public GameObject pointM;
    public GameObject pointN;
    public GameObject pointO;
    public GameObject pointP;
    public GameObject pointQ;
    public GameObject pointHeightLock;
    public GameObject pointL;

    [Header("Cylinder")]
    public GameObject cylinderObject;
    public GameObject pointCylinderTop;
    public Vector3 cylinderTopPoint;
    public GameObject pointCylinderCenter;
    public Vector3 cylinderCenterPoint;
    public GameObject pointCylinderBottom;
    public Vector3 cylinderBottomPoint;
    public Vector3 cylinderDirection;
    public Vector3 cylinderNormal;
    public float cylinderRadius;

    [Header("Cylinder Values")]
    public float distanceFromCylinderPoint;
    public float cylinderHeight;
    public float cylinderHeightDifference;
    [Header("Cylinder Lengths")]
    public float lengthCylinder2Intersect;
    public float lengthLine2Intersect;
    public float lengthLine2Cylinder;
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

    private void Update() {
        // line setup
        lineDirection = lineObject.transform.up;
        linePoint = lineObject.transform.localPosition;

        // cylinder setup
        cylinderCenterPoint = cylinderObject.transform.localPosition;
        cylinderTopPoint  = cylinderNormal.normalized * cylinderObject.transform.localScale.x + cylinderCenterPoint;
        cylinderBottomPoint = -cylinderNormal.normalized * cylinderObject.transform.localScale.x + cylinderCenterPoint;
        cylinderNormal = cylinderObject.transform.up;
        cylinderRadius = cylinderObject.transform.localScale.x / 2f;

        pointCylinderTop.transform.localPosition = cylinderTopPoint;
        pointCylinderTop.transform.rotation = Quaternion.FromToRotation(Vector3.up, cylinderNormal);
        pointCylinderTop.GetComponent<MeshRenderer>().enabled = true;

        pointCylinderCenter.transform.position = cylinderCenterPoint;
        pointCylinderCenter.transform.rotation = Quaternion.FromToRotation(Vector3.up, cylinderNormal);
        pointCylinderCenter.GetComponent<MeshRenderer>().enabled = true;

        pointCylinderBottom.transform.localPosition = cylinderBottomPoint;
        pointCylinderBottom.transform.rotation = Quaternion.FromToRotation(Vector3.up, cylinderNormal);
        pointCylinderBottom.GetComponent<MeshRenderer>().enabled = true;
        // ==================================

        Debug.DrawLine(cylinderTopPoint, linePoint, Color.white);
        Debug.DrawLine(cylinderCenterPoint, linePoint, Color.white);
        Debug.DrawLine(cylinderBottomPoint, linePoint, Color.white);

        if (CylinderIntersect(out interectPoint, linePoint, lineNormal,cylinderNormal, cylinderTopPoint, cylinderCenterPoint , cylinderBottomPoint, cylinderRadius)) { 
        
        }


        if (CylinderCast(out interectPoint, linePoint, lineDirection, cylinderNormal, cylinderCenterPoint, 0.5f)) {
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

    bool CylinderIntersect(out Vector3 intersection, Vector3 linePoint, Vector3 lineNormal,
                                 Vector3 cylinderNormal, Vector3 cylinderTop, Vector3 cylinderCenterPoint, Vector3 cylinderBottom, float radius) {
        intersection = Vector3.zero;

        return true;
  


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
        angleGreenA = Vector3.Angle((linePoint - linePlaneOrthogonal), (lineCylinderNormalPoint - linePlaneOrthogonal));
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


        /*
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
        */


        cylinderHeight = cylinderObject.transform.localScale.y * 2f;
        cylinderHeightDifference = qPoint.y - cylinderPoint.y;
        if (cylinderHeightDifference > cylinderHeight / 2f) {
            Debug.Log("Outside of cylidner");
            return false;
        }
        intersection = qPoint;


        return true;
    }
}
