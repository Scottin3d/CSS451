using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SpawnObjectScript : MonoBehaviour {
    public GameObject target;
    public Vector3 spawnPosition;
    public Dropdown dropDown;

    [SerializeField]
    List<GameObject> spawnPrefabs;

    private void Start() {
        spawnPrefabs = new List<GameObject>();
    }

    public void SpawnObject() {
        // find spawn point
        //spawnPosition = target.transform.position;
        // instantiate prfab
        spawnPosition = target.transform.position;
        if (dropDown.value > 0) { 
            GameObject obj = Instantiate(spawnPrefabs[dropDown.value - 1], spawnPosition, Quaternion.identity);
            spawnPrefabs.Add(obj);
        }
        dropDown.value = 0;
    } 
}
