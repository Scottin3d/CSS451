
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour {

    [SerializeField]
    LayerMask ignoredLayer;

    [SerializeField]
    public GameObject currentSelection;

    public Slider[] sliders;

    bool selected = false;
    GameObject target;

    private void Start() {
        //currentSelection = null;
    }

    private void Update() {
        //if click,
        if (Input.GetMouseButton(0)) {
            click();
        }

        if (Input.GetMouseButtonUp(0)) {
            selected = false;
        }
    }


    private void click() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (!selected) {

            if (Physics.Raycast(ray, out hit, 1000f, ~ignoredLayer)) {
                if (!EventSystem.current.IsPointerOverGameObject()) {
                    selected = true;
                    target = hit.collider.gameObject;
                    // debug
                    Debug.Log(target.name);
                }
            }
        } else {
            if (Physics.Raycast(ray, out hit, 1000f, ~ignoredLayer)) {
                string tag = hit.collider.tag;
                switch (tag) {
                    case "wall":
                        
                        MovePoint(target, hit);
                        break;
                    case "floor":
                        MovePoint(target, hit);
                        break;
                    default:
                        break;
                }
            }
        }
    }

    void MovePoint(GameObject target, RaycastHit hit) {
        target.GetComponent<wallScript>().MovePoint(hit.point);
    }

    public GameObject GetCurrentSelection() {
        return currentSelection;
    }
}
