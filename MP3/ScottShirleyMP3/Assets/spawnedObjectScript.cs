using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using Utils;

public class spawnedObjectScript : MonoBehaviour
{
    UIDriver uiDriver;

    float moveSpeed = 10;
    float lifeCycle = 15;
    float spawnTime = 0;
    
    Vector3 ballToPlanePoint;
    Vector3 mdir;
    Vector3 normal;
    Vector3 Vr;
    Vector3 Von;

    GameObject p1;
    GameObject p2;
    GameObject plane;


    [Header("Shadow Object Values")]
    GameObject shadowTarget;
    public GameObject Projection;
    public bool activeProjection = false;
    public float radius;
    public Vector3 shadowIntersectPoint;
    public float distance;

    [Header("Refections Values")]
    public float distanceToReflectionPoint;
    public float distancePlaneToReflectionPoint;
    public float reflectionPlaneBias;
    float reflectionOffsetBias;
    Vector3 barrierReflectionPoint;
    public float planeCullCheckDot;

    // lines
    bool debugMode = false;
    GameObject lineBall2Plane;
    GameObject lineBall2Intersect;
    GameObject lineIntersect2Plane;
    GameObject lineBall2Shadow;

    private void Awake() {
        // get components
        InitializeComponents();
        InitializeLineRenderers();

        // set values
        radius = shadowTarget.transform.localScale.x / 2f;
        reflectionOffsetBias = transform.localScale.x / 2f;
        reflectionPlaneBias = plane.transform.localScale.x * 5f;
    }

    // init values
    // called when instantited
    public void Initialize(float _speed, float _lifeCyle, GameObject _P1, GameObject _P2, GameObject _plane) {
        //transform.rotation = Quaternion.FromToRotation(Vector3.up, transform.up);
        moveSpeed = _speed;
        lifeCycle = _lifeCyle;
        spawnTime = 0;
        p1 = _P1;
        p2 = _P2;
        plane = _plane;
    }

    // init components
    // find if not assigned
    void InitializeComponents() {
        uiDriver = GameObject.Find("Canvas").GetComponent<UIDriver>();
        plane = GameObject.Find("TheBarrier");
        Projection = Instantiate(Resources.Load("Prefabs/projection") as GameObject, this.transform);
        Projection.GetComponent<MeshRenderer>().enabled = activeProjection;
        shadowTarget = GameObject.Find("thunderDome");
    }

    // initialize debug lines for visual aide
    void InitializeLineRenderers() {
        lineBall2Intersect = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        lineBall2Intersect.GetComponent<MeshRenderer>().material.color = Color.magenta;

        lineBall2Plane = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        lineBall2Plane.GetComponent<MeshRenderer>().material.color = Color.green;

        lineIntersect2Plane = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        lineIntersect2Plane.GetComponent<MeshRenderer>().material.color = Color.blue;

        lineBall2Shadow = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        lineBall2Shadow.GetComponent<MeshRenderer>().material.color = Color.black;

    }

    // update method
    private void Update() {
        UpdateMovement();
        IsDead();
        Projections();
        DebugMode();
    }

    // updates movement
    private void UpdateMovement() {
        // movement
        transform.position += transform.up * Time.deltaTime * moveSpeed;
        // intersect?
        distanceToReflectionPoint = Utils.vectorUtils.Distance(transform.position, barrierReflectionPoint);
        // culled face? dot > 0
        Vector3 o = transform.up;
        o.Normalize();
        Vector3 n = -plane.transform.up;
        n.Normalize();
        planeCullCheckDot = Vector3.Dot(o,n);
        // within barrier distance(intersect, planeorigin)
        distancePlaneToReflectionPoint = Utils.vectorUtils.Distance(barrierReflectionPoint, plane.transform.position);
        //&& distancePlaneToReflectionPoint <= reflectionPlaneBias
        //&& distancePlaneToReflectionPoint <= reflectionPlaneBias
        if (distanceToReflectionPoint - reflectionOffsetBias <= 0
         && planeCullCheckDot >= 0) {
            Reflect();
        }
    }

    // relfection method
    private void Reflect() {
        //R = 2 * dot(-mDir, n) * n - (-mDir);
        mdir = transform.up;
        normal = plane.transform.up;
        normal.Normalize();
        Vr = 2 * Vector3.Dot(-mdir, normal) * normal - (-mdir);
        transform.rotation = Quaternion.FromToRotation(Vector3.up, Vr);
    }

    // destroys all objects after lifecycle
    private void IsDead() {
        spawnTime += Time.deltaTime;
        if (spawnTime >= lifeCycle) {
            Debug.Log("Destroyed");
            Destroy(this.gameObject);
            Destroy(lineBall2Intersect);
            Destroy(lineBall2Plane);
            Destroy(lineIntersect2Plane);
            Destroy(lineBall2Shadow);
        }
    }

    // handle protjections
    // shadow blob
    // debug lines
    private void Projections() {

        // varialble helper because these equations are horrible to read
        Vector3 lineDir = transform.up;
        Vector3 linePt = transform.position;
        Vector3 planeNormal = -plane.transform.up;
        Vector3 planePt = plane.transform.position;
        radius = shadowTarget.transform.localScale.x / 2f;

        // shadowblob
        if (Utils.vectorUtils.ScottCast(out shadowIntersectPoint, linePt, -planeNormal, planeNormal, planePt)) {
            // ball to intersect
            //Debug.DrawLine(linePt, shadowIntersectPoint, Color.black);
            Utils.vectorUtils.AdjustLine(lineBall2Shadow, linePt, shadowIntersectPoint, 0.05f);
            //lineBall2Shadow.enabled = true;
            planeCullCheckDot = Vector3.Dot(lineDir, planeNormal);
            distance = Utils.vectorUtils.Distance(shadowIntersectPoint, planePt);
            if (distance >= radius) {
                Projection.GetComponent<MeshRenderer>().enabled = false;
                lineBall2Shadow.GetComponent<MeshRenderer>().enabled = false;
            } else {
                if (planeCullCheckDot >= 0) { 
                    Projection.GetComponent<MeshRenderer>().enabled = true;
                    if (debugMode) { 
                        lineBall2Shadow.GetComponent<MeshRenderer>().enabled = true;
                    }

                }
            }

            Projection.transform.rotation = Quaternion.FromToRotation(Vector3.up, planeNormal);
            Vector3 pos = shadowIntersectPoint;
            pos.y += 0.1f;
            Projection.transform.position = pos;
        }

        // draw line from ball to plane center
        //Debug.DrawLine(linePt, planePt, Color.green);
        Utils.vectorUtils.AdjustLine(lineBall2Plane, linePt, planePt, 0.05f);

        // draw line from ball to plane intersection
        // draw line from plane intersection to plane center
        if (Utils.vectorUtils.ScottCast(out barrierReflectionPoint, linePt, transform.up, planeNormal, planePt)) {
            //Debug.DrawLine(barrierReflectionPoint, linePt, Color.magenta);
            Utils.vectorUtils.AdjustLine(lineBall2Intersect, barrierReflectionPoint, linePt, 0.05f);
            Utils.vectorUtils.AdjustLine(lineIntersect2Plane, barrierReflectionPoint, planePt, 0.1f);
        }
    }

    // toggles debug "lines" on/off
    void DebugMode() {
        debugMode = uiDriver.DebugMode();

        if (debugMode) {
            lineBall2Plane.GetComponent<MeshRenderer>().enabled = true;
            lineBall2Intersect.GetComponent<MeshRenderer>().enabled = true;
            lineIntersect2Plane.GetComponent<MeshRenderer>().enabled = true;
            lineBall2Shadow.GetComponent<MeshRenderer>().enabled = true;
        } else {
            lineBall2Plane.GetComponent<MeshRenderer>().enabled = false;
            lineBall2Intersect.GetComponent<MeshRenderer>().enabled = false;
            lineIntersect2Plane.GetComponent<MeshRenderer>().enabled = false;
            lineBall2Shadow.GetComponent<MeshRenderer>().enabled = false;
        }
    }
}
