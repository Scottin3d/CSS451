using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.UI;

public partial class UIDriver : MonoBehaviour {
    bool gameIsPaused = false;
    bool debugMode = false;

    [SerializeField]
    GameObject quitScreen;
    bool quitScreenActive = false;

    private static bool ignoreValueChanges = false;
    public int state = 0;

    [SerializeField]
    Image controlsBackground;
    Color controlsBackgroundColor;

    [SerializeField]
    GameLogic gameLogic;

    [SerializeField]
    Text objectSelected;

    // plane xform controller
    public Text[] SliderValueText = new Text[3];
    public Slider[] xformSliders = new Slider[3];

    // object prefab
    public Text[] travellingBallsText = new Text[3];
    public Slider[] travellingBallsSliders = new Slider[3];

    const float TMAX = 20f;
    const float TMIN = -20f;
    const float SMAX = 5f;
    const float SMIN = 1f;
    const float RMAX = 180f;
    const float RMIN = -180f;

    //toggles
    public Toggle translateToggle;
    public Toggle scaleToggle;
    public Toggle rotationToggle;

    private void Start() {
        controlsBackgroundColor = controlsBackground.color;
        InitializeComponents();
        InitializeSliders();
        InitializeToggles();
        
    }

    void InitializeComponents() {
        if (!gameLogic) {
            gameLogic = GameObject.Find("theWorld").GetComponent<GameLogic>();
        }
        if (!objectSelected) {
            objectSelected = GameObject.Find("SelectionBackground").transform.GetChild(1).GetComponent<Text>();
        }
        if (!controlsBackground) {
            controlsBackground = GameObject.Find("ControlsPanel").GetComponent<Image>();
        }
        if (!quitScreen) {
            quitScreen = GameObject.Find("QuitScreen");
        }

        quitScreen.SetActive(quitScreenActive);
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
        //GameTime();
        // T
        if (Input.GetKeyDown(KeyCode.T)) {
            translateToggle.isOn = true;
            ToggleValues(true);
        }

        // S
        if (Input.GetKeyDown(KeyCode.S)) {
            scaleToggle.isOn = true;
            ToggleValues(true);
        }

        // R
        if (Input.GetKeyDown(KeyCode.R)) {
            rotationToggle.isOn = true;
            ToggleValues(true);
        }

        // P: Pause Time
        if (Input.GetKeyDown(KeyCode.P)) {
            if (gameIsPaused) {
                Time.timeScale = 1;
                controlsBackground.color = controlsBackgroundColor;
            } else {
                Time.timeScale = 0;
                Color c = Color.red;
                controlsBackground.color = c;
            }
            gameIsPaused = !gameIsPaused;
        }

        // F1: Toggle Debug Lines
        if (Input.GetKeyDown(KeyCode.F1)) {
            debugMode = !debugMode;
        }

        // Esc
        if (Input.GetKeyDown(KeyCode.Escape)) {
            gameIsPaused = true;
            quitScreen.SetActive(!quitScreenActive);
            quitScreenActive = !quitScreenActive;
        }
    }

    private void UpdateSelection() {
        if (gameLogic.GetCurrentSelection()) {
            objectSelected.text = gameLogic.GetCurrentSelection().name;
        } else {
            //objectSelected.text = "none";
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

    private void GameTime() {
        if (gameIsPaused) {
            Time.timeScale = 1;
            controlsBackground.color = controlsBackgroundColor;
        } else {
            Time.timeScale = 0;
            Color c = Color.red;
            controlsBackground.color = c;
        }
        
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
            xformSliders[0].value = xformSliders[1].value = xformSliders[2].value = 0;
        } else if (translateToggle.isOn) {
            //clamp values
            xformSliders[0].minValue = xformSliders[1].minValue = xformSliders[2].minValue = TMIN;
            xformSliders[0].maxValue = xformSliders[1].maxValue = xformSliders[2].maxValue = TMAX;
            xformSliders[0].wholeNumbers = xformSliders[1].wholeNumbers = xformSliders[2].wholeNumbers = false;

            //set value
            xformSliders[0].value = gameLogic.GetCurrentSelection().transform.position.x;
            //set text
            SliderValueText[0].text = xformSliders[0].value.ToString();
            //set value
            xformSliders[1].value = gameLogic.GetCurrentSelection().transform.position.y;
            //set text
            SliderValueText[1].text = xformSliders[1].value.ToString();
            //set value
            xformSliders[2].value = gameLogic.GetCurrentSelection().transform.position.z;
            //set text
            SliderValueText[2].text = xformSliders[2].value.ToString();
        } else if (scaleToggle.isOn) {
            xformSliders[0].minValue = xformSliders[1].minValue = xformSliders[2].minValue = SMIN;
            xformSliders[0].maxValue = xformSliders[1].maxValue = xformSliders[2].maxValue = SMAX;
            xformSliders[0].wholeNumbers = xformSliders[1].wholeNumbers = xformSliders[2].wholeNumbers = false;

            //set value
            xformSliders[0].value = gameLogic.GetCurrentSelection().transform.localScale.x;
            //set text
            SliderValueText[0].text = xformSliders[0].value.ToString();
            //set value
            xformSliders[1].value = gameLogic.GetCurrentSelection().transform.localScale.y;
            //set text
            SliderValueText[1].text = xformSliders[1].value.ToString();
            //set value
            xformSliders[2].value = gameLogic.GetCurrentSelection().transform.localScale.z;
            //set text
            SliderValueText[2].text = xformSliders[2].value.ToString();

        } else if (rotationToggle.isOn) {
            xformSliders[0].minValue = xformSliders[1].minValue = xformSliders[2].minValue = RMIN;
            xformSliders[0].maxValue = xformSliders[1].maxValue = xformSliders[2].maxValue = RMAX;
            xformSliders[0].wholeNumbers = xformSliders[1].wholeNumbers = xformSliders[2].wholeNumbers = true;

            //set value
            
            float rotX = gameLogic.GetCurrentSelection().transform.rotation.eulerAngles.x;
            rotX = (rotX > 180) ? rotX - 360 : rotX;
            xformSliders[0].value = rotX;
            //set text
            SliderValueText[0].text = xformSliders[0].value.ToString();
            //set value
            float rotY = gameLogic.GetCurrentSelection().transform.rotation.eulerAngles.y;
            rotY = (rotY > 180) ? rotY - 360 : rotY;
            xformSliders[1].value = rotY;
            //set text
            SliderValueText[1].text = xformSliders[1].value.ToString();
            //set value
            float rotZ = gameLogic.GetCurrentSelection().transform.rotation.eulerAngles.z;
            rotZ = (rotZ > 180) ? rotZ - 360 : rotZ;
            xformSliders[2].value = rotZ;
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

    public void ButtonQuit() {
        Application.Quit();
    }

    public void ButtonBack() {
        gameIsPaused = false;

        quitScreen.SetActive(!quitScreenActive);
        quitScreenActive = !quitScreenActive;
    }

    public bool DebugMode() {
        return debugMode;
    }
}
