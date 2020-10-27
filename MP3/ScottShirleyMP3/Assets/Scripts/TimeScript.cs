using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScript : MonoBehaviour
{
    public bool isPaused = false;

    public void TimeButtonClick() {
        isPaused = !isPaused;
        if (isPaused) {
            Time.timeScale = 0;
        } else {
            Time.timeScale = 1;
        }
    }
}
