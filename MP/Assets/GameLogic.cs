
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour {

    public GameObject currentSelection;
    public UIDriver uiDriver;

    public Slider[] sliders;

    private void Start() {
        currentSelection = null;
    }
    private void Update() {
        //if click,
        if (Input.GetMouseButtonDown(0)) {
            click(0);
        }

        if (Input.GetMouseButtonDown(1)) {
            click(1);
        }


    }

    public void UpdatePosition() {
        if (!UIDriver.ignoreValueChanges) {
            switch (uiDriver.state) {
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


    private void click(int button) {
        /* At any point, you can click the left mouse button (LMB) to select any of the GameObject defined in the scene. 
         * LMB clicking on the static plane or an empty space results in selecting nothing. 
         * Selected object must:
         *      Be displayed in a unique color (mine is some kind of yellow-ish)
         *      Be semi-transparent
         *      Unselecting an object must cause it to return to its original color.
         */

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        switch (button) {
            case 0:
                if (Physics.Raycast(ray, out hit)) {
                    if (!EventSystem.current.IsPointerOverGameObject()) {
                        if (hit.transform.CompareTag("object")) {
                            //currentSelection.GetComponent<MP2ObjectBehavior>().Selected(false);
                            currentSelection = hit.transform.gameObject;
                            currentSelection.GetComponent<MP2ObjectBehavior>().Selected(true);
                            uiDriver.ToggleValues(true);
                        } else {
                            currentSelection = null;
                        }

                    }
                }
                break;
            case 1:
                if (Physics.Raycast(ray, out hit)) {
                    if (hit.transform.CompareTag("sliderHandle")) {
                        hit.transform.parent.parent.GetComponent<Slider>().value = 0;
                    }
                }
                break;
            default:
                break;
        }
    }
}
