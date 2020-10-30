
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour {

    public UIDriver uiDriver;

    [SerializeField]
    LayerMask ignoredLayer = 0;

    [SerializeField]
    GameObject barrierPlane;
    Vector3 barrierStartPos;
    Vector3 barrierStartRot;
    Vector3 barrierStartScale;

    [SerializeField]
    public GameObject currentSelection;

    public Slider[] sliders;

    bool selected = false;
    GameObject target;

    private void Start() {
        //currentSelection = null;
        if (!barrierPlane) {
            barrierPlane = GameObject.Find("TheBarrier");
        }
        barrierStartPos = barrierPlane.transform.position;
        barrierStartRot = barrierPlane.transform.rotation.eulerAngles;
        barrierStartScale = barrierPlane.transform.localScale;

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
                    //Debug.Log(target.name);
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

    public void UpdatePosition() {
        if (!UIDriver.IgnoreChange()) {
            switch (uiDriver.State()) {
                //translate
                case 0:
                    Vector3 position = new Vector3(sliders[0].value, sliders[1].value, sliders[2].value);
                    currentSelection.transform.position = position;
                    break;
                //scale
                case 1:
                    Vector3 scale = new Vector3(sliders[0].value, sliders[1].value, sliders[2].value);
                    currentSelection.transform.localScale = scale;
                    break;
                //rotate
                case 2:
                    Vector3 rotation = new Vector3(sliders[0].value, sliders[1].value, sliders[2].value);
                    currentSelection.transform.eulerAngles = rotation;
                    break;
                default:
                    break;
            }
        }
    }

    public void ResetBarrier() {
        barrierPlane.transform.position = barrierStartPos;
        barrierPlane.transform.rotation = Quaternion.FromToRotation(Vector3.up, barrierStartRot);
        barrierPlane.transform.localScale = barrierStartScale;
    }
}
