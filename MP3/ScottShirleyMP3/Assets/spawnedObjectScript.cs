using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

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
    public GameObject Projection;
    public bool activeProjection = false;

    public string name;
    public float dot;
    public float magnitude;


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
    }

    private void Update() {
        dot = Vector3.Dot(transform.position, plane.transform.position);
        magnitude = transform.up.magnitude;
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
        Vector3 planeNormal = plane.transform.up;
        Projection.transform.rotation = Quaternion.FromToRotation(Vector3.up, planeNormal);

        Ray ray = new Ray(transform.position, -planeNormal);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1000f, 11)) {

            Debug.DrawLine(transform.position, hit.point, Color.red);
            Projection.transform.position = hit.point;
            name = hit.transform.name;
            if (hit.collider.CompareTag("thunderDome")) {
                activeProjection = true;
            } else {
                activeProjection = false;
            }
        }
        Projection.GetComponent<MeshRenderer>().enabled = activeProjection;
    }
}
