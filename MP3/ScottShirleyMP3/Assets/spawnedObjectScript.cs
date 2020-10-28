using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnedObjectScript : MonoBehaviour
{

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


}
