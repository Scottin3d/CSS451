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
    [Header("Show Planes")]
    bool showXPlane;
    public GameObject cylinderXplane;
    MeshRenderer cylinderXplaneRenderer;
    bool showYPlane;
    public GameObject cylinderYplane;
    MeshRenderer cylinderYplaneRenderer;
    bool showZPlane;
    public GameObject cylinderZplane;
    MeshRenderer cylinderZplaneRenderer;

    [Header("Cylinder Points")]
    public GameObject lineCylinderHeightPoint;
    public GameObject pointCylinderTop;
    public Vector3 cylinderTopPoint;
    public GameObject pointCylinderCenter;
    public Vector3 cylinderCenterPoint;
    public GameObject pointCylinderBottom;
    public Vector3 cylinderBottomPoint;
    public Vector3 cylinderDirection;
    public Vector3 cylinderNormal;
    public float cylinderRadius;

    [Header("Intersections")]
    public GameObject cylinderTrefPoint;
    public GameObject lineTrefPoint;
    public GameObject lineIntersectionPoint;
    public GameObject cylinderXIntersect;
    public GameObject cylinderYIntersect;
    public GameObject cylinderZIntersect;

    [Header("Intersect Triangulation")]
    public float tAngleA = 0;
    public float tAngleB = 0;
    public float tAngleC = 0;

    public float tLengthA = 0;
    public float tLengthB = 0;
    public float tLengthC = 0;


    [Header("Cylinder Values")]
    public float distanceFromCylinderPoint;
    public float cylinderHeight;
    public float cylinderHeightDifference;

    [Header("Cylinder Lengths")]
    float lengthCylinder2Intersect;
    float lengthLine2Intersect;
    float lengthLine2Cylinder;
    [Header("Cylinder Angles")]
    float angleA;
    float angleB;
    float angleC;
    [Header("Cylinder Angles Radians")]
    float angleArad;
    float angleBrad;
    float angleCrad;
    [Header("Cylinder Point I Lengths")]
     float lengthA;
     float lengthB;
     float lengthC;
    [Header("Cylinder Plane Angles")]
     float angleIA;
     float angleIB;
     float angleIC;
    [Header("Cylinder Plane Lengths")]
     float lengthIA;
     float lengthIB;
     float lengthIC;
    [Header("Cylinder Intersection Lengths")]
     float lengthGreenA;
     float lengthGreenB;
     float lengthGreenC;
    [Header("Cylinder Intersection Angles")]
     float angleGreenA;
     float angleGreenB;
     float angleGreenC;


    private void Update() {
        // show planes
        cylinderXplane.GetComponent<MeshRenderer>().enabled = showXPlane;
        cylinderYplane.GetComponent<MeshRenderer>().enabled = showYPlane;
        cylinderZplane.GetComponent<MeshRenderer>().enabled = showZPlane;


        // line setup
        lineDirection = lineObject.transform.up;
        linePoint = lineObject.transform.localPosition;

        // cylinder setup
        cylinderCenterPoint = cylinderObject.transform.position;
        
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

        //DrawToCylinder(linePoint, Color.white);
        //Debug.DrawLine(cylinderTopPoint, linePoint, Color.white);
        //Debug.DrawLine(cylinderCenterPoint, linePoint, Color.white);
        //Debug.DrawLine(cylinderBottomPoint, linePoint, Color.white);
        
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
        cylinderTopPoint = cylinderNormal.normalized * cylinderObject.transform.localScale.x + cylinderCenterPoint;
        cylinderBottomPoint = -cylinderNormal.normalized * cylinderObject.transform.localScale.x + cylinderCenterPoint;


        
        Vector3 angleRef = lineNormal.normalized * 10f + linePoint;
        //pointL.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
        cylinderTrefPoint.transform.position = angleRef;
        cylinderTrefPoint.transform.rotation = Quaternion.FromToRotation(Vector3.up, lineNormal);


        tAngleA = Vector3.Angle((cylinderPoint - linePoint),(angleRef - linePoint));
        //tAngleB;
        //tAngleC;

        tLengthA = radius;
        //tLengthB;
        tLengthC = Vector3.Distance(linePoint, cylinderPoint);








    // find the axis intercepts
    Vector3 xOrPoint = Vector3.zero;
        float xDistance = 0f;
        Vector3 yOrPoint = Vector3.zero;
        float yDistance = 0f;
        Vector3 zOrPoint = Vector3.zero;
        float zDistance = 0f;



        // get X axis 
        Vector3 lineXorthagonal;
        if (ScottCast(out lineXorthagonal, linePoint, lineNormal, -cylinderObject.transform.forward, cylinderPoint)) {
            cylinderXIntersect.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
            cylinderXIntersect.transform.position = lineXorthagonal;
            cylinderXIntersect.transform.rotation = Quaternion.FromToRotation(Vector3.up, lineNormal);
            //DrawToCylinder(lineXorthagonal, Color.red);

            CastDown(out xOrPoint, lineXorthagonal);
            xDistance = Vector3.Distance(xOrPoint, cylinderPoint);
            showXPlane = (xDistance <= radius) ? true : false;

        }

        // get y axis
        Vector3 lineYorthagonal;
        if (ScottCast(out lineYorthagonal, linePoint, lineNormal, cylinderNormal, cylinderPoint)) {
            cylinderYIntersect.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
            cylinderYIntersect.transform.position = lineYorthagonal;
            cylinderYIntersect.transform.rotation = Quaternion.FromToRotation(Vector3.up, lineNormal);
            //DrawToCylinder(lineYorthagonal, Color.blue);
            
            CastDown(out yOrPoint, lineYorthagonal);
            yDistance = Vector3.Distance(yOrPoint, cylinderPoint);
            showYPlane = (yDistance <= radius) ? true : false;
            //yDistance = (showYPlane) ? yDistance : 0f;

        }

        // get y axis
        Vector3 lineZorthagonal;
        if (ScottCast(out lineZorthagonal, linePoint, lineNormal, -cylinderObject.transform.right, cylinderPoint)) {
            cylinderZIntersect.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
            cylinderZIntersect.transform.position = lineZorthagonal;
            cylinderZIntersect.transform.rotation = Quaternion.FromToRotation(Vector3.up, lineNormal);
            //DrawToCylinder(lineZorthagonal, Color.green);
            
            CastDown(out zOrPoint, lineZorthagonal);
            zDistance = Vector3.Distance(zOrPoint, cylinderPoint);
            showZPlane = (zDistance <= radius) ? true : false;
            //zDistance = (showZPlane) ? zDistance : 0f;
        }

        if (!showXPlane && !showYPlane && !showZPlane) {
            Debug.Log("No Intercepts");
            return false;
        }
        Debug.Log("An Intercept Somewhere...");

        // find which axis to work off of
        // refPoint will be the point to derive the triangulation
        Vector3 orTref;     // flat plane reference of closest plane intersect
        Vector3 lineTref;   // flat plane reference of the line
        CastDown(out lineTref, linePoint);

        if (xDistance < yDistance) {
            orTref = (xDistance < zDistance) ? xOrPoint : zOrPoint;
        } else {
            orTref = (yDistance < zDistance) ? yOrPoint : zOrPoint;
        }
        DrawToCylinder(orTref, Color.green);
        pointN.transform.position = orTref;

        Debug.DrawLine(orTref, cylinderPoint, Color.yellow);
        Debug.DrawLine(lineTref, orTref, Color.yellow);
        Debug.DrawLine(lineTref, cylinderPoint, Color.yellow);
        /*
        tLengthB = radius;
        tLengthC = Vector3.Distance(cylinderPoint, orTref);

        tAngleB = Vector3.Angle((lineTref - orTref), (cylinderPoint - orTref));
        // C = arcsin (c·sin(B) / b)
        float tAngleBrad = tAngleB * Mathf.Deg2Rad;
        float tval = Mathf.Sin(tAngleBrad) * tLengthC;
        tval /= tLengthB;
        float tAngleCrad = Mathf.Asin(tval);
        tAngleC = tAngleCrad / Mathf.Deg2Rad;
        // A = 180° - B - C 
        tAngleA = 180f - tAngleC - tAngleB;
        float tAngleArad = tAngleA * Mathf.Deg2Rad;


        // a = b·sin(A) / sin(B)
        tLengthA = tLengthB * Mathf.Sin(tAngleArad) / Mathf.Sin(tAngleBrad);

        // TrefPoint
        // targetDirection = target = position
        Vector3 tRefPointDir = lineTref - cylinderPoint;
        Vector3 tRefPoint = tRefPointDir.normalized * radius + cylinderPoint;
        //pointL.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
        cylinderTrefPoint.transform.position = tRefPoint;
        cylinderTrefPoint.transform.rotation = Quaternion.FromToRotation(Vector3.up, tRefPointDir);
        */

        // lineTrefPoint
        // targetDirection = target = position
        Vector3 lineTrefDir = lineTref - orTref;
        Vector3 linetRefPoint = lineTrefDir.normalized * tLengthA + orTref;
        //pointL.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
        lineTrefPoint.transform.position = linetRefPoint;
        lineTrefPoint.transform.rotation = Quaternion.FromToRotation(Vector3.up, lineTrefDir);


        // intersection
//float intersectAngleA;
        //float intersectAngleB;
        //float intersectAngleC;
        //float intersectLengthA;
        //float intersectLengthB = Vector3.Distance(orTref, linetRefPoint);
        //float intersectLengthC = Vector3.Distance(orTref);






        Vector3 lineCylinderNormalPoint;
        if (ScottCast(out lineCylinderNormalPoint, linePoint, -cylinderNormal, cylinderNormal, cylinderPoint)) {
        }


        distanceFromCylinderPoint = Vector3.Distance(lineYorthagonal, cylinderPoint);



        // plane triangle lengths (blue)
        lengthCylinder2Intersect = Vector3.Distance(lineYorthagonal, cylinderPoint);
        lengthLine2Cylinder = Vector3.Distance(linePoint, cylinderPoint);
        lengthLine2Intersect = Vector3.Distance(linePoint, lineYorthagonal);

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
        lengthIB = Vector3.Distance(cylinderPoint, lineYorthagonal);


        // magenta angles
        angleIA = Vector3.Angle((cylinderPoint - lineYorthagonal), (lineCylinderNormalPoint - lineYorthagonal));
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
        Vector3 lDir = lineCylinderNormalPoint - lineYorthagonal;
        Vector3 lPoint = lDir.normalized * lengthIC + lineYorthagonal;
        pointL.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
        pointL.transform.position = lPoint;
        pointL.transform.rotation = Quaternion.FromToRotation(Vector3.up, lDir);
       

        // pointM
        // cylinder2intersect
        Vector3 mDir = lineYorthagonal - cylinderPoint;
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
        angleGreenA = Vector3.Angle((linePoint - lineYorthagonal), (lineCylinderNormalPoint - lineYorthagonal));
        float angleGreedArad = angleGreenA * Mathf.Deg2Rad;
        angleGreenB = 90f;
        float angleGreedBrad = angleGreenB * Mathf.Deg2Rad;
        angleGreenC = 180f - angleGreenA - angleGreenB;
        float angleGreedCrad = angleGreenC * Mathf.Deg2Rad;

        // green lengths
        // distance between orthogonal and plane intersect
        lengthGreenC = Vector3.Distance(lineYorthagonal, lPoint);
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

    void DrawToCylinder(Vector3 FromPoint, Color color) {
        Debug.DrawLine(cylinderTopPoint, FromPoint, color);
        Debug.DrawLine(cylinderCenterPoint, FromPoint, color);
        Debug.DrawLine(cylinderBottomPoint, FromPoint, color);
    }

    void CastDown(out Vector3 pointTo, Vector3 pointFrom) {
        // line2cylinderPlane
        // shoots down in direction of cylinder normal to find the height difference between point and cylinder
        pointTo = Vector3.zero;
        if (ScottCast(out pointTo, pointFrom, -cylinderNormal, cylinderNormal, cylinderCenterPoint)) {
            Debug.DrawLine(pointTo, cylinderCenterPoint, Color.magenta);
            Debug.DrawLine(pointTo, pointFrom, Color.magenta);
        }
    }

    

}
