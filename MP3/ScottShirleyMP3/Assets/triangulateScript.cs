using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

[ExecuteInEditMode]
public class triangulateScript : MonoBehaviour {
    [Header("Cylinder")]
    public GameObject cylinder;
    [SerializeField]
    Vector3 cylinderPoint;
    [SerializeField]
    Vector3 cylinderNormal;
    [SerializeField]
    float cylinderRadius;
    public GameObject cylinderCenterRight;
    public GameObject cylinderCenterLeft;
    public GameObject cylinderCenterForward;
    public GameObject cylinderCenterBack;




    [Header("Line")]
    public GameObject line;
    [SerializeField]
    Vector3 linePoint;
    [SerializeField]
    Vector3 lineNormal;

    [Header("Triangulation Forward")]
    public GameObject normalizedLineForward;
    [SerializeField]
    Vector3 normalizedLineForwardPoint;
    public GameObject normalizedLineForwardRadial;
    [SerializeField]
    Vector3 normalizedLineRadialForwardPoint;
    public GameObject normalizedLineForwardPlanar;
    [SerializeField]
    Vector3 normalizedLinePlanarForwardPoint;

    [Header("Triangulation Rear")]
    public GameObject normalizedRearLine;
    [SerializeField]
    Vector3 normalizedLineRearPoint;
    public GameObject normalizedLineRearRadial;
    [SerializeField]
    Vector3 normalizedLineRadialRearPoint;
    public GameObject normalizedLineRearPlanar;
    [SerializeField]
    Vector3 normalizedLinePlanarRearPoint;

    [Header("Triangulation")]
    [SerializeField]
    float angleA;
    float angleArad;
    [SerializeField]
    float angleB;
    float angleBrad;
    [SerializeField]
    float angleC;
    float anglecRad;
    [SerializeField]
    float lengthA;
    [SerializeField]
    float lengthB;
    [SerializeField]
    float lengthC;

    public float blue;



    private void Update() {
        cylinderPoint = cylinder.transform.position;
        cylinderNormal = -cylinder.transform.up;
        cylinderRadius = cylinder.transform.localScale.x / 2f;
        linePoint = line.transform.position;
        lineNormal = -line.transform.up;

        // add cylinder ref points
        CylinderPoints(cylinderCenterRight,cylinderPoint, cylinder.transform.right, cylinderRadius);
        CylinderPoints(cylinderCenterLeft, cylinderPoint, -cylinder.transform.right, cylinderRadius);
        CylinderPoints(cylinderCenterForward, cylinderPoint, cylinder.transform.forward, cylinderRadius);
        CylinderPoints(cylinderCenterBack, cylinderPoint, -cylinder.transform.forward, cylinderRadius);


        //forward
        Vector3 forwardDir = lineNormal;
        normalizedLineForwardPoint = forwardDir.normalized * cylinderRadius + linePoint;
        normalizedLineForward.transform.position = normalizedLineForwardPoint;
        normalizedLineForward.transform.rotation = Quaternion.FromToRotation(Vector3.up, forwardDir);
        
        //normalize
        if (Utils.vectorUtils.ScottCast(out normalizedLinePlanarForwardPoint, normalizedLineForwardPoint, -cylinderNormal, cylinderNormal, cylinderPoint)) {
            Debug.DrawLine(normalizedLinePlanarForwardPoint, normalizedLineForwardPoint, Color.red);
            Debug.DrawLine(normalizedLinePlanarForwardPoint, cylinderPoint, Color.red);
            normalizedLineForwardPlanar.transform.position = normalizedLinePlanarForwardPoint;
        }
        //radial
        Vector3 forwardRadialDir = normalizedLinePlanarForwardPoint - cylinderPoint;
        normalizedLineRadialForwardPoint = forwardRadialDir.normalized * cylinderRadius + cylinderPoint;
        normalizedLineForwardRadial.transform.position = normalizedLineRadialForwardPoint;
        normalizedLineForwardRadial.transform.rotation = Quaternion.FromToRotation(Vector3.up, -cylinderNormal);

        //rear
        Vector3  rearDir = -lineNormal;
        normalizedLineRearPoint = rearDir.normalized * cylinderRadius + linePoint;
        normalizedRearLine.transform.position = normalizedLineRearPoint;
        normalizedRearLine.transform.rotation = Quaternion.FromToRotation(Vector3.up, rearDir);
        
        //normalize
        if (Utils.vectorUtils.ScottCast(out normalizedLinePlanarRearPoint, normalizedLineRearPoint, -cylinderNormal, cylinderNormal, cylinderPoint)) {
            Debug.DrawLine(normalizedLinePlanarRearPoint, normalizedLineRearPoint, Color.red);
            Debug.DrawLine(normalizedLinePlanarRearPoint, cylinderPoint, Color.red);
            normalizedLineRearPlanar.transform.position = normalizedLinePlanarRearPoint;
        }
        //radial
        Vector3 rearRadialDir = normalizedLinePlanarRearPoint - cylinderPoint;
        normalizedLineRadialRearPoint = rearRadialDir.normalized * cylinderRadius + cylinderPoint;
        normalizedLineRearRadial.transform.position = normalizedLineRadialRearPoint;


        Debug.DrawLine(normalizedLinePlanarRearPoint, normalizedLinePlanarForwardPoint, Color.green);

        lengthC = Vector3.Distance(normalizedLinePlanarForwardPoint, normalizedLineForwardPoint);
        angleA = 90f;
        angleB = Vector3.Angle((normalizedLineRearPoint - normalizedLineForwardPoint),(normalizedLinePlanarForwardPoint - normalizedLineForwardPoint));

        blue = Vector3.Distance(normalizedLinePlanarForwardPoint, normalizedLineRadialRearPoint);

        Debug.DrawLine(normalizedLineRadialForwardPoint, normalizedLinePlanarRearPoint, Color.blue);

    }

    void CylinderPoints(GameObject _point, Vector3 _cylinderCenter, Vector3 _direction, float _cylinderRadius) {
        Vector3 radialDirection = _direction;
        Vector3 radialPoint = radialDirection.normalized * _cylinderRadius + _cylinderCenter;
        _point.transform.position = radialPoint;
        _point.transform.rotation = Quaternion.FromToRotation(Vector3.up, -_direction);
    }
}
