using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnedObjectScript : MonoBehaviour
{
    bool destroyLeaveScene = false;
    public float moveSpeed, lifeCycle, spawnTime;

    public void Initialize(float _speed, float _lifeCyle) {
        transform.rotation = Quaternion.FromToRotation(Vector3.up, transform.up);
        moveSpeed = _speed;
        spawnTime = 0;
    }

    private void Update() {
        transform.position -= transform.up * Time.deltaTime * moveSpeed;
        spawnTime += Time.deltaTime;
        if (spawnTime >= lifeCycle) {
            Debug.Log("Destroyed");
            // Destroy(this.gameObject);
        }

    }

    void OnCollisionEneter

        if (other.CompareTag("Wall")) {
            if (destroyLeaveScene) {
                Destroy(this.gameObject);
            }
            Vector3 relectionVector = ReflectionOverPlane(other);
        }
    }

    public Vector3 ReflectionOverPlane(Vector3 point, Plane plane) {
        float H = Vector3.Dot(transform.up, );
        Vector3 N = transform.TransformDirection(plane.normal);
        return point - 2 * N * Vector3.Dot(point, N) / Vector3.Dot(N, N);
    }


}
