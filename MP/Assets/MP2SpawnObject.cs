using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MP2SpawnObject : MonoBehaviour {

    [SerializeField]
    GameObject masterTarget;

    [SerializeField]
    GameLogic gameLogic;

    [SerializeField]
    UIDriver uiDriver;

    [SerializeField]
    GameObject[] prefabList = new GameObject[3];

    List<GameObject> spawnedPrefabs;

    static int defaultXZ = 1, defaultY = 1;

    // Start is called before the first frame update
    void Start() {
        if (!gameLogic) {
            gameLogic = GameObject.Find("GameLogic").GetComponent<GameLogic>();
        }

        if (!uiDriver) {
            uiDriver = GameObject.Find("Canvas").GetComponent<UIDriver>();
        }

        if (!masterTarget) {
            masterTarget = GameObject.Find("GrandParent");
        }

        spawnedPrefabs = new List<GameObject>();
    }

    public void SpawnObject() {

        //if null
        //spawn away
            // find spawn point
            //spawnPosition = target.transform.position;
            // instantiate prfab
        if (uiDriver.dropDown.value > 0) {
            // is selection null
            Vector3 spawnPosition;
            GameObject parent;
            Color color;
            if (gameLogic.GetCurrentSelection()) {
                parent = gameLogic.GetCurrentSelection();
                int childCount = parent.GetComponent<MP2ObjectBehavior>().ChildCount();
                parent.GetComponent<MP2ObjectBehavior>().AddChild();

                spawnPosition = gameLogic.GetCurrentSelection().transform.position + new Vector3(childCount + 1, childCount + 1, childCount + 1);
                color = Color.white;

            } else {
                spawnPosition = new Vector3(defaultXZ, defaultY, defaultXZ);

                defaultXZ++;
                if (defaultXZ >= 10) {
                    defaultXZ = 1;
                    defaultY++;
                }

                parent = masterTarget;
                color = Color.black;
            }
            Debug.Log(spawnPosition);
            GameObject obj = Instantiate(prefabList[uiDriver.GetDropDownValue() - 1], spawnPosition, Quaternion.identity, parent.transform);
            obj.GetComponent<MeshRenderer>().sharedMaterial.color = color;
            spawnedPrefabs.Add(obj);
        }
   
        uiDriver.ResetDropdown();
    }
}
