using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testSpawn : MonoBehaviour
{

    public GameObject spawn;
    float previousTime = 0;
    float spawnInterval = 5f;

    // Update is called once per frame
    void Update()
    {
        previousTime += Time.deltaTime;
        if (previousTime >= spawnInterval) {
            SpawnObject();
            Debug.Log("Reset Time");
            previousTime = 0;
        }
    }

    void SpawnObject() {
        GameObject clone = Instantiate(spawn, transform.position, Quaternion.identity);
    }
}
