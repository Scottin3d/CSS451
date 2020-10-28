using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class prefabSpawnScript : MonoBehaviour {
    [SerializeField]
    float spawnInterval = 1f;

    [SerializeField]
    float objSpeed = 15;

    [SerializeField]
    float objLifeCycle = 10;

    public GameObject lineSpawnPrefab;

    [SerializeField]
    Transform spawnPosition;

    [SerializeField]
    Transform targetPosition;

    [SerializeField]
    Color color = Color.red;

    public List<GameObject> spawnedPrefabs;
    float previousTime = 0;

    // Start is called before the first frame update
    void Start() {
        //Debug.Assert(lineSpawnPrefab != null);
        //Debug.Assert(spawnPosition != null);
        //Debug.Assert(targetPosition != null);

        spawnedPrefabs = new List<GameObject>();
    }

    private void Update() {
        previousTime += Time.deltaTime;

        if (previousTime >= spawnInterval) {
            SpawnObject();
            Debug.Log("Reset Time");
            previousTime = 0;
        }
    }


    public void SetInterval(float _interval) {
        spawnInterval = _interval;
    }

    public void SetSpeed(float _speed) {
        objSpeed = _speed;
    }


    public void SetLifeCycle(float _objLifeCycle) {
        objLifeCycle = _objLifeCycle;
    }

    void SpawnObject() {
        //instantiate

        Vector3 pos = spawnPosition.position;
        Vector3 V = spawnPosition.position - targetPosition.position;
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, V);

        GameObject spawn = Instantiate(lineSpawnPrefab);
        spawn.GetComponent<spawnedObjectScript>().Initialize(objSpeed, objLifeCycle);
        spawn.transform.position = pos;
        spawn.transform.rotation = rot;
        spawn.transform.localScale = new Vector3(1f, 1f, 1f);
        spawnedPrefabs.Add(spawn);
    }
}
