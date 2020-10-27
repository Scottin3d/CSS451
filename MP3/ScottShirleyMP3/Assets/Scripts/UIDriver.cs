using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.UI;

public class UIDriver : MonoBehaviour {

    private static bool ignoreValueChanges = false;
    
    int state = 0;

    [SerializeField]
    public Dropdown dropDown;

    [SerializeField]
    GameLogic gameLogic;

    [SerializeField]
    Text objectSelected;

    [SerializeField]
    Text[] SliderValueText = new Text[3];

    //sliders
    [SerializeField]
    Slider[] sliders = new Slider[3];



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
        SliderValueText[0].text = sliders[0].value.ToString();
        SliderValueText[1].text = sliders[1].value.ToString();
        SliderValueText[2].text = sliders[2].value.ToString();
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
            sliders[0].value = 0;
            sliders[1].value = 0;
            sliders[2].value = 0;
        }else if (translateToggle.isOn) {
            //clamp values
            sliders[0].minValue = TMIN;
            sliders[0].maxValue = TMAX;
            //set value
            sliders[0].value = gameLogic.GetCurrentSelection().transform.position.x;
            //set text
            SliderValueText[0].text = sliders[0].value.ToString();

            //clamp values
            sliders[1].minValue = TMIN;
            sliders[1].maxValue = TMAX;
            //set value
            sliders[1].value = gameLogic.GetCurrentSelection().transform.position.y;
            //set text
            SliderValueText[1].text = sliders[1].value.ToString();

            //clamp values
            sliders[2].minValue = TMIN;
            sliders[2].maxValue = TMAX;
            //set value
            sliders[2].value = gameLogic.GetCurrentSelection().transform.position.z;
            //set text
            SliderValueText[2].text = sliders[2].value.ToString();
        } else if (scaleToggle.isOn) {
            sliders[0].minValue = SMIN;
            sliders[0].maxValue = SMAX;
            //set value
            sliders[0].value = gameLogic.GetCurrentSelection().transform.localScale.x;
            //set text
            SliderValueText[0].text = sliders[0].value.ToString();

            sliders[1].minValue = SMIN;
            sliders[1].maxValue = SMAX;
            //set value
            sliders[1].value = gameLogic.GetCurrentSelection().transform.localScale.y;
            //set text
            SliderValueText[1].text = sliders[1].value.ToString();

            sliders[2].minValue = SMIN;
            sliders[2].maxValue = SMAX;
            //set value
            sliders[2].value = gameLogic.GetCurrentSelection().transform.localScale.z;
            //set text
            SliderValueText[2].text = sliders[2].value.ToString();

        } else if (rotationToggle.isOn) {
            sliders[0].minValue = sliders[1].minValue = sliders[2].minValue = RMIN;
            sliders[0].maxValue = sliders[1].maxValue = sliders[2].maxValue = RMAX;
            //xSlider.minValue = RMIN;
            //xSlider.maxValue = RMAX;
            //set value
            sliders[0].value = gameLogic.GetCurrentSelection().transform.rotation.x;
            //set text
            SliderValueText[0].text = sliders[0].value.ToString();

            //ySlider.minValue = RMIN;
            //ySlider.maxValue = RMAX;
            //set value
            sliders[1].value = gameLogic.GetCurrentSelection().transform.rotation.y;
            //set text
            SliderValueText[1].text = sliders[1].value.ToString();

            //zSlider.minValue = RMIN;
            //zSlider.maxValue = RMAX;
            //set value
            sliders[2].value = gameLogic.GetCurrentSelection().transform.rotation.z;
            //set text
            SliderValueText[2].text = sliders[2].value.ToString();
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
