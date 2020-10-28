using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.UI;

public partial class UIDriver : MonoBehaviour {

    private static bool ignoreValueChanges = false;
    
    int state = 0;

    [SerializeField]
    public Dropdown dropDown;

    [SerializeField]
    GameLogic gameLogic;

    [SerializeField]
    Text objectSelected;

    // plane xform controller
    [SerializeField]
    Text[] SliderValueText = new Text[3];
    [SerializeField]
    Slider[] xformSliders = new Slider[3];

    // object prefab
    [SerializeField]
    Text[] travellingBallsText = new Text[3];
    [SerializeField]
    Slider[] travellingBallsSliders = new Slider[3];


    const float TMAX = 20f;
    const float TMIN = -20f;
    const float SMAX = 5f;
    const float SMIN = 1f;
    const float RMAX = 180f;
    const float RMIN = -180f;

    //toggles
    [SerializeField]
    Toggle translateToggle;
    [SerializeField]
    Toggle scaleToggle;
    [SerializeField]
    Toggle rotationToggle;


    private void Start() {
        InitializeSliders();
        InitializeToggles();
    }
    private void InitializeSliders() {
        ToggleValues(true);
    }

    private void InitializeToggles() {
        translateToggle.isOn = true;
        scaleToggle.isOn = false;
        rotationToggle.isOn = false;
        ToggleValues(true);
    }

    private void Update() {
        UpdateSelection();
        UpdateSliderText();
    }


    private void UpdateSelection() {
        if (gameLogic.GetCurrentSelection()) {
            objectSelected.text = gameLogic.GetCurrentSelection().name;
        } else {
            objectSelected.text = "none";
        }
    }

    private void UpdateSliderText() {
        SliderValueText[0].text = xformSliders[0].value.ToString();
        SliderValueText[1].text = xformSliders[1].value.ToString();
        SliderValueText[2].text = xformSliders[2].value.ToString();
        travellingBallsText[0].text = travellingBallsSliders[0].value.ToString();
        travellingBallsText[1].text = travellingBallsSliders[1].value.ToString();
        travellingBallsText[2].text = travellingBallsSliders[2].value.ToString();
    }

    public void ChangeState() {
        if (translateToggle.isOn) {
            state = 0;
        } else if (scaleToggle.isOn) {
            state = 1;
        } else if (rotationToggle.isOn) {
            state = 2;
        }
    }

    public void ToggleValues(bool b) {
        /* translation between -10 to + 10
         * scaling between 1 to 5.
         * rotation between -180 to 180
         */
        ignoreValueChanges = true;
        ChangeState();

        //reclamp values
        //set values
        //update text

        //assign right min/max
        //update label
        if (gameLogic.GetCurrentSelection() == null) {
            xformSliders[0].value = 0;
            xformSliders[1].value = 0;
            xformSliders[2].value = 0;
        }else if (translateToggle.isOn) {
            //clamp values
            xformSliders[0].minValue = TMIN;
            xformSliders[0].maxValue = TMAX;
            //set value
            xformSliders[0].value = gameLogic.GetCurrentSelection().transform.position.x;
            //set text
            SliderValueText[0].text = xformSliders[0].value.ToString();

            //clamp values
            xformSliders[1].minValue = TMIN;
            xformSliders[1].maxValue = TMAX;
            //set value
            xformSliders[1].value = gameLogic.GetCurrentSelection().transform.position.y;
            //set text
            SliderValueText[1].text = xformSliders[1].value.ToString();

            //clamp values
            xformSliders[2].minValue = TMIN;
            xformSliders[2].maxValue = TMAX;
            //set value
            xformSliders[2].value = gameLogic.GetCurrentSelection().transform.position.z;
            //set text
            SliderValueText[2].text = xformSliders[2].value.ToString();
        } else if (scaleToggle.isOn) {
            xformSliders[0].minValue = SMIN;
            xformSliders[0].maxValue = SMAX;
            //set value
            xformSliders[0].value = gameLogic.GetCurrentSelection().transform.localScale.x;
            //set text
            SliderValueText[0].text = xformSliders[0].value.ToString();

            xformSliders[1].minValue = SMIN;
            xformSliders[1].maxValue = SMAX;
            //set value
            xformSliders[1].value = gameLogic.GetCurrentSelection().transform.localScale.y;
            //set text
            SliderValueText[1].text = xformSliders[1].value.ToString();

            xformSliders[2].minValue = SMIN;
            xformSliders[2].maxValue = SMAX;
            //set value
            xformSliders[2].value = gameLogic.GetCurrentSelection().transform.localScale.z;
            //set text
            SliderValueText[2].text = xformSliders[2].value.ToString();

        } else if (rotationToggle.isOn) {
            xformSliders[0].minValue = xformSliders[1].minValue = xformSliders[2].minValue = RMIN;
            xformSliders[0].maxValue = xformSliders[1].maxValue = xformSliders[2].maxValue = RMAX;
            //xSlider.minValue = RMIN;
            //xSlider.maxValue = RMAX;
            //set value
            xformSliders[0].value = gameLogic.GetCurrentSelection().transform.rotation.x;
            //set text
            SliderValueText[0].text = xformSliders[0].value.ToString();

            //ySlider.minValue = RMIN;
            //ySlider.maxValue = RMAX;
            //set value
            xformSliders[1].value = gameLogic.GetCurrentSelection().transform.rotation.y;
            //set text
            SliderValueText[1].text = xformSliders[1].value.ToString();

            //zSlider.minValue = RMIN;
            //zSlider.maxValue = RMAX;
            //set value
            xformSliders[2].value = gameLogic.GetCurrentSelection().transform.rotation.z;
            //set text
            SliderValueText[2].text = xformSliders[2].value.ToString();
        }

        ignoreValueChanges = false;
    }

    public int State() {
        return state;
    }

    public static bool IgnoreChange() {
        return ignoreValueChanges;
    }

    public int GetDropDownValue() {
        return dropDown.value;
    }
    public void ResetDropdown() {
        dropDown.value = 0;
    }
}
