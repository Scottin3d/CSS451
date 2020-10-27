using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MP2SpawnObject : MonoBehaviour {

    [SerializeField]
    GameLogic gameLogic;

    [SerializeField]
    GameObject[] prefabList = new GameObject[3];

    List<GameObject> spawnedPrefabs;

    static int defaultXZ = 1, defaultY = 1;

    // Start is called before the first frame update
    void Start() {
        if (!gameLogic) {
            gameLogic = GameObject.Find("GameLogic").GetComponent<GameLogic>();
        }

        spawnedPrefabs = new List<GameObject>();
    }

    public void SpawnObject(int index) {

        //if null
        //spawn away
            // find spawn point
            //spawnPosition = target.transform.position;
            // instantiate prfab
        if (index > 0) {
            // is selection null
            Vector3 spawnPosition;
            GameObject parent;
            GameObject obj;
            Color color;
            if (gameLogic.GetCurrentSelection()) {
                parent = gameLogic.GetCurrentSelection();
                int childCount = parent.GetComponent<MP2ObjectBehavior>().ChildCount();
                

                spawnPosition = gameLogic.GetCurrentSelection().transform.position + new Vector3(childCount + 1, childCount + 1, childCount + 1);
                color = Color.white;
                obj = Instantiate(prefabList[index - 1], spawnPosition, Quaternion.identity, parent.transform);
                spawnedPrefabs.Add(obj);
                parent.GetComponent<MP2ObjectBehavior>().AddChild(obj);
            } else {
                spawnPosition = new Vector3(defaultXZ, defaultY, defaultXZ);

                defaultXZ++;
                if (defaultXZ >= 10) {
                    defaultXZ = 1;
                    defaultY++;
                }

                 obj = Instantiate(prefabList[index - 1], spawnPosition, Quaternion.identity);

                color = Color.black;
            }
            // set color
            obj.GetComponent<MP2ObjectBehavior>().SetColor(color);
        }
    }
}
