
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour {

    public UIDriver uiDriver;


    [SerializeField]
    public GameObject currentSelection;

    public Slider[] sliders;
    GameObject target;

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
}
