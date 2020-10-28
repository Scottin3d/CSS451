using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnedObjectScript : MonoBehaviour
{
    public float moveSpeed = 10;
    public float lifeCycle = 15;
    public float spawnTime = 0;

    public ContactPoint[] colliders;

    public float t1;
    public float h;
    public Vector3 mdir;
    public Vector3 normal;
    public Vector3 Vr;
    public Vector3 Von;

    GameObject P1;
    GameObject P2;

    public GameObject Projection;
    public GameObject Pt;

    float kNormalSize = 5f;
    float kMaxProjectedSize = 10f;
    float kVerySmall = 0.0001f; // let's avoid this

    public void Initialize(float _speed, float _lifeCyle, GameObject _P1, GameObject _P2) {
        //transform.rotation = Quaternion.FromToRotation(Vector3.up, transform.up);
        moveSpeed = _speed;
        lifeCycle = _lifeCyle;
        spawnTime = 0;
        P1 = _P1;
        P2 = _P2;
    }

    private void Update() {
        transform.position += transform.up * Time.deltaTime * moveSpeed;
        spawnTime += Time.deltaTime;
        if (spawnTime >= lifeCycle) {
            Debug.Log("Destroyed");
            Destroy(this.gameObject);
        }
    }


    private void OnTriggerEnter(Collider other) {

        if (other.CompareTag("barrier")) {
            mdir = transform.up;
            normal = other.transform.up;
            normal.Normalize();
            Vr = 2 * Vector3.Dot(-mdir, normal) * normal - (-mdir);
            //R = 2 * dot(-mDir, n) * n - (-mDir);

            transform.rotation = Quaternion.FromToRotation(Vector3.up, Vr);
        }
    }
}

/*
 Debug.Log("Reflect!");
            Debug.DrawLine(P1.transform.localPosition, P2.transform.localPosition, Color.black);

            Pt = other.gameObject;

            // normal
            normal = -Pt.transform.forward;
            Vector3 center = Pt.transform.localPosition;
            float d = Vector3.Dot(normal, center);

            // line
            Von = P2.transform.localPosition - P1.transform.localPosition;
            Von.Normalize();

            // distance
            float denom = Vector3.Dot(normal, Von);
      

            // intersection distant
            t1 = (d - Vector3.Dot(normal, P1.transform.localPosition)) / denom;
            //other.transform.localPosition = P1.transform.localPosition + t1 * Von;

            //h = Dot(Von, N)
            h = Vector3.Dot(center, normal) - d;

            //Projected.transform.localPosition = ThePoint.transform.localPosition - (n * h);
            float s = h * 0.50f;

            if (s < 0) {
                s = 0.5f;
            }

            Vr = 2 * (Vector3.Dot(Von, normal)) * normal - Von;
            Vector3 drawPt = Pt.transform.localPosition + Vr * 30;
            Debug.DrawLine(Pt.transform.localPosition, drawPt, Color.red);
 */
