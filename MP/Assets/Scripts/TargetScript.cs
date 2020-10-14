using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TargetScript : MonoBehaviour {

    // Update is called once per frame
    void Update() {
        //if click,
        if (Input.GetMouseButtonDown(0)) {
            click();
        }
    }
    void click() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit)) {
            if (!EventSystem.current.IsPointerOverGameObject()) {
                // object? delete
                if (hit.transform.gameObject.CompareTag("object")) {
                    Destroy(hit.transform.gameObject);
                } else {

                    // plane? move object
                    if (hit.transform.name == "CreationPlane") {
                        Debug.Log("Hit Plane");
                        MoveTarget(hit.point);
                    }
                }
            }
        }
    }

    void MoveTarget(Vector3 pos) {
        // 0.25f offset above plane
        pos.y += 0.25f;
        transform.position = pos;
    }
}
