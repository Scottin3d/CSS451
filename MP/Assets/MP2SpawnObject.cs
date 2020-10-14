using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MP2SpawnObject : MonoBehaviour {

    [SerializeField]
    GameLogic gameLogic;

    [SerializeField]
    UIDriver uiDriver;

    [SerializeField]
    GameObject[] prefabList = new GameObject[3];

    // Start is called before the first frame update
    void Start() {
        if (!gameLogic) {
            gameLogic = GameObject.Find("GameLogic").GetComponent<GameLogic>();
        }

        if (!uiDriver) {
            uiDriver = GameObject.Find("Canvas").GetComponent<UIDriver>();
        }
    }

    public void SpawnObject() {
        // find spawn point
        //spawnPosition = target.transform.position;
        // instantiate prfab
        if (uiDriver.dropDown.value > 0) {
            // is selection null
            Vector3 spawnPosition;
            if (gameLogic.currentSelection) {
                spawnPosition = gameLogic.currentSelection.transform.position + new Vector3(0.1f, 0.1f, 0.1f);
            } else {
                spawnPosition = new Vector3(1f, 1f, 1f);
            }
         
            GameObject obj = Instantiate(prefabList[uiDriver.dropDown.value - 1], spawnPosition, Quaternion.identity, gameLogic.currentSelection.transform);
        }
        uiDriver.dropDown.value = 0;
    }
}
