using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

[ExecuteInEditMode]
public class cylinderScript : MonoBehaviour {
    [Header("Show Planes")]
    public bool xPlaneOn = false;
    public bool yPlaneOn = false;
    public bool zPlaneOn = false;

    [Header("Cylinder")]
    Vector3 cylinderPoint;
    Vector3 cylinderXNormal;
    Vector3 cylinderYNormal;
    Vector3 cylinderZNormal;

    public GameObject xIntersect;
    public Vector3 xIntersectPoint;
    public GameObject yIntersect;
    public Vector3 yIntersectPoint;
    public GameObject zIntersect;
    public Vector3 zIntersectPoint;



    [Header("Planes")]
    public GameObject xPlane;
    MeshRenderer xPlaneRenderer;
    public GameObject yPlane;
    MeshRenderer yPlaneRenderer;
    public GameObject zPlane;
    MeshRenderer zPlaneRenderer;

    [Header("Line")]
    public GameObject lineObject;
    Vector3 linePoint;
    Vector3 lineNormal;

    private void Awake() {
        xPlaneRenderer = xPlane.GetComponent<MeshRenderer>();
        yPlaneRenderer = yPlane.GetComponent<MeshRenderer>();
        zPlaneRenderer = zPlane.GetComponent<MeshRenderer>();

        cylinderPoint = transform.localPosition;
        cylinderXNormal = -transform.forward;
        cylinderYNormal = -transform.up;
        cylinderZNormal = -transform.right;

        linePoint = lineObject.transform.position;
        lineNormal = lineObject.transform.up;
    }



    // Update is called once per frame
    void Update() {
        xPlaneRenderer.enabled = xPlaneOn;
        yPlaneRenderer.enabled = yPlaneOn;
        zPlaneRenderer.enabled = zPlaneOn;
    }

    void FindClosestPlane() {

        // get x
        if (Utils.vectorUtils.ScottCast(out xIntersectPoint, linePoint, lineNormal, cylinderXNormal, cylinderPoint)) {
            xIntersect.transform.position = xIntersectPoint;
        }

        // get y
        if (Utils.vectorUtils.ScottCast(out yIntersectPoint, linePoint, lineNormal, cylinderYNormal, cylinderPoint)) {
            yIntersect.transform.position = yIntersectPoint;
        }
        // get z
        if (Utils.vectorUtils.ScottCast(out zIntersectPoint, linePoint, lineNormal, cylinderZNormal, cylinderPoint)) {
            zIntersect.transform.position = zIntersectPoint;
        }
    }
}
