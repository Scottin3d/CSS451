using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using Utils;

public class spawnedObjectScript : MonoBehaviour
{
    float moveSpeed = 10;
    float lifeCycle = 15;
    float spawnTime = 0;

    public ContactPoint[] colliders;

    
    Vector3 mdir;
    Vector3 normal;
    Vector3 Vr;
    Vector3 Von;

    GameObject p1;
    GameObject p2;

    GameObject plane;

    Vector3 ballToPlanePoint;

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
    bool reflectBall = false;

    private void Awake() {
        plane = GameObject.Find("TheBarrier");
        Projection = Instantiate(Resources.Load("Prefabs/projection") as GameObject, this.transform);
        Projection.GetComponent<MeshRenderer>().enabled = activeProjection;
        shadowTarget = GameObject.Find("thunderDome");
        radius = shadowTarget.transform.localScale.x / 2f;
        reflectionOffsetBias = transform.localScale.x / 2f;

        reflectionPlaneBias = plane.transform.localScale.x * 5f;
    }
    public void Initialize(float _speed, float _lifeCyle, GameObject _P1, GameObject _P2, GameObject _plane) {
        //transform.rotation = Quaternion.FromToRotation(Vector3.up, transform.up);
        moveSpeed = _speed;
        lifeCycle = _lifeCyle;
        spawnTime = 0;
        p1 = _P1;
        p2 = _P2;
        plane = _plane;
    }

    private void Update() {
        UpdateMovement();
        IsDead();
        Projections();
    }

    private void Reflect() {
        //R = 2 * dot(-mDir, n) * n - (-mDir);
        mdir = transform.up;
        normal = plane.transform.up;
        normal.Normalize();
        Vr = 2 * Vector3.Dot(-mdir, normal) * normal - (-mdir);
        transform.rotation = Quaternion.FromToRotation(Vector3.up, Vr);
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
        if (distanceToReflectionPoint - reflectionOffsetBias <= 0 
         && distancePlaneToReflectionPoint <= reflectionPlaneBias
         && planeCullCheckDot >= 0) {
            Reflect();
        }
    }

    // destroys after lifecycle
    private void IsDead() {
        spawnTime += Time.deltaTime;
        if (spawnTime >= lifeCycle) {
            Debug.Log("Destroyed");
            Destroy(this.gameObject);
        }
    }

    // projects shadow blob onto flat sphere on plane
    private void Projections() {
        Debug.DrawLine(transform.localPosition, plane.transform.position, Color.green);
        radius = shadowTarget.transform.localScale.x / 2f;

        Vector3 lineDir = transform.up;
        Vector3 linePt = transform.position;
        Vector3 planeNormal = -plane.transform.up;
        Vector3 planePt = plane.transform.position;

        // shadowblob
        
        if (Utils.vectorUtils.ScottCast(out shadowIntersectPoint, linePt, -planeNormal, planeNormal, planePt)) {
            Debug.DrawLine(shadowIntersectPoint, linePt, Color.black);

            // culled face? dot > 0
            Vector3 o = lineDir;
            o.Normalize();
            Vector3 n = planeNormal;
            n.Normalize();
            planeCullCheckDot = Vector3.Dot(o, n);

            distance = Utils.vectorUtils.Distance(shadowIntersectPoint, planePt);
            if (distance >= radius) {
                Projection.GetComponent<MeshRenderer>().enabled = false;
            } else {
                if (planeCullCheckDot >= 0) { 
                    Projection.GetComponent<MeshRenderer>().enabled = true;
                }
            }

            Projection.transform.rotation = Quaternion.FromToRotation(Vector3.up, planeNormal);
            Vector3 pos = shadowIntersectPoint;
            pos.y += 0.1f;
            Projection.transform.position = pos;
        }


        if (Utils.vectorUtils.ScottCast(out ballToPlanePoint, linePt, lineDir, planeNormal, planePt)) {
            Debug.DrawLine(ballToPlanePoint, linePt, Color.blue);
        }

        // forward
        if (Utils.vectorUtils.ScottCast(out barrierReflectionPoint, linePt, transform.up, planeNormal, planePt)) {
            Debug.DrawLine(barrierReflectionPoint, linePt, Color.magenta);
        }
    }
}
