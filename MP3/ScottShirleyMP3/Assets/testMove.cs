using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;


public class testMove : MonoBehaviour
{
    float moveSpeed = 5;
    float life = 30f;
    float spawnTime = 0;

   public GameObject plane;

    public float radius = 2f;
    public float length;
    public float a;
    public float b;
    public float x;
    public Vector3 intersect;
    public Vector3 vector;

    public float Distance;

    private void Start() {
        plane = GameObject.Find("Plane");
    }

    // Update is called once per frame
    void Update()
    {
        IsDead();
        UpdateMovement();

        Debug.DrawLine(transform.localPosition, plane.transform.position, Color.green);

        Vector3 lineDir = transform.up;
        Vector3 linePt = transform.position;
        Vector3 planeNormal = -plane.transform.up;
        Vector3 planePt = plane.transform.position;

        if (Utils.vectorUtils.ScottCast(out intersect, linePt, -planeNormal, planeNormal, planePt)) {
            Debug.DrawLine(intersect, linePt, Color.black);

            //Distance = Vector3.Distance(intersect, transform.position);
            Distance = (intersect - transform.position).magnitude;
            if (Distance >= radius) {
                //ShadowBlob.GetComponent<MeshRenderer>().enabled = false;
            } else {
                //ShadowBlob.GetComponent<MeshRenderer>().enabled = true;
            }

            //ShadowBlob.transform.rotation = Quaternion.FromToRotation(Vector3.up, n);
            Vector3 pos = intersect;
            pos.y += 0.1f;
            //ShadowBlob.transform.position = pos;
        }

        if (Utils.vectorUtils.ScottCast(out intersect, linePt, lineDir, planeNormal, planePt)) {
            Debug.DrawLine(intersect, linePt, Color.blue);
        }
    }

    private void UpdateMovement() {
        transform.position += -transform.forward * Time.deltaTime * moveSpeed;
    }

    private void IsDead() {
        spawnTime += Time.deltaTime;
        if (spawnTime >= life) {
            Debug.Log("Destroyed");
            Destroy(this.gameObject);
        }
    }
}
