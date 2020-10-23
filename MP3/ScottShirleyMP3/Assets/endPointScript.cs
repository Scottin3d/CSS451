using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.UI;


public class endPointScript : MonoBehaviour
{
    private Vector3 mOffset;
    private float mZCoord;
    Vector3 mousePos;

    [SerializeField]
    GameObject plane;

    GameObject target;
    bool selected = false;

    void Update() {
        //if click,
        if (Input.GetMouseButton(0)) {
            click();
        }

        if (Input.GetMouseButtonUp(0)) {
            selected = false;
        }
    }

    void click() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (!selected) {
            if (Physics.Raycast(ray, out hit)) {
                // plane? move object
                if (hit.collider.CompareTag("point")) {
                    target = hit.collider.gameObject;
                    Debug.Log(target);
                    selected = true;
                }
            }
        } else {
            if (Physics.Raycast(ray, out hit)){
                if (hit.collider.gameObject == plane) {
                    target.transform.position = hit.point;
                }
            }
        }


    }

}
