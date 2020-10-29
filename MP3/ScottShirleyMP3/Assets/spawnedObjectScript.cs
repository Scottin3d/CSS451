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
    public GameObject plane;

    public GameObject Projection;
    public bool activeProjection = false;
    public float radius;
    public Vector3 intersect;
    public float distance;


    private void Awake() {
        Projection = Instantiate(Resources.Load("Prefabs/projection") as GameObject, this.transform);
        Projection.GetComponent<MeshRenderer>().enabled = activeProjection;
        
    }
    public void Initialize(float _speed, float _lifeCyle, GameObject _P1, GameObject _P2, GameObject _plane) {
        //transform.rotation = Quaternion.FromToRotation(Vector3.up, transform.up);
        moveSpeed = _speed;
        lifeCycle = _lifeCyle;
        spawnTime = 0;
        p1 = _P1;
        p2 = _P2;
        plane = _plane;
        radius = GameObject.Find("thunderDome").transform.localScale.x / 2f;
    }

    private void Update() {
        UpdateMovement();
        IsDead();
        ProjectShadowBlob();
    }


    // for reflection
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("barrier")) {
            //R = 2 * dot(-mDir, n) * n - (-mDir);
            mdir = transform.up;
            normal = other.transform.up;
            normal.Normalize();
            Vr = 2 * Vector3.Dot(-mdir, normal) * normal - (-mdir);
            transform.rotation = Quaternion.FromToRotation(Vector3.up, Vr);
        }
    }

    // updates movement
    private void UpdateMovement() {
        transform.position += transform.up * Time.deltaTime * moveSpeed;
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
    private void ProjectShadowBlob() {
        Debug.DrawLine(transform.localPosition, plane.transform.position, Color.green);

        Vector3 lineDir = transform.up;
        Vector3 linePt = transform.position;
        Vector3 planeNormal = -plane.transform.up;
        Vector3 planePt = plane.transform.position;

        if (Utils.vectorUtils.ScottCast(out intersect, linePt, -planeNormal, planeNormal, planePt)) {
            Debug.DrawLine(intersect, linePt, Color.black);

            distance = Utils.vectorUtils.Distance(intersect, planePt);
            if (distance >= radius) {
                Projection.GetComponent<MeshRenderer>().enabled = false;
            } else {
                Projection.GetComponent<MeshRenderer>().enabled = true;
            }

            Projection.transform.rotation = Quaternion.FromToRotation(Vector3.up, planeNormal);
            Vector3 pos = intersect;
            pos.y += 0.1f;
            Projection.transform.position = pos;
        }

        if (Utils.vectorUtils.ScottCast(out intersect, linePt, lineDir, planeNormal, planePt)) {
            Debug.DrawLine(intersect, linePt, Color.blue);
        }
    }
}
