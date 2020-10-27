
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour {

    [SerializeField]
    public GameObject currentSelection;

    public UIDriver uiDriver;

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
            if (Physics.Raycast(ray, out hit)) {
                selected = true;
                target = hit.collider.gameObject;
                Debug.Log(target.name);
            }
        } else {
            if (Physics.Raycast(ray, out hit)) {
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

    public void ResetSelection() {
        if (currentSelection) {
            currentSelection.GetComponent<MP2ObjectBehavior>().ResetTransform();
        }
    }

    public void DeleteChildren() {
        // if theres a selections
        if (currentSelection) {
            // if its not a default obj
            if (currentSelection.name != "GrandParent" ||
                currentSelection.name != "Parent" ||
                currentSelection.name != "Child") {
                currentSelection.GetComponent<MP2ObjectBehavior>().DestroyChildren();
            }
        }

    }
}
