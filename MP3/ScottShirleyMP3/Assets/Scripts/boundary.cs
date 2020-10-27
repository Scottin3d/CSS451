using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class boundary : MonoBehaviour
{
    private bool showBounds = false;
    public Toggle toggle;

    void Awake()
    {
        if (!toggle) {
            toggle = GameObject.Find("ShowBoundary").GetComponent<Toggle>();
        }
        toggle.isOn = showBounds;
    }

    public void ShowBounds(bool b) {
        transform.gameObject.SetActive(b);
    }
}
