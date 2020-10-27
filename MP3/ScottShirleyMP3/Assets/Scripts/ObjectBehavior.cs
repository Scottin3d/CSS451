using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectBehavior : MonoBehaviour
{
    public bool isBound = false;
    public Toggle boundToggle;

    public bool posDir = true;

    protected float offset;
    public float scale;

    public float speed;
    Vector3 spawnPos;
    public Vector3 moveDirection = Vector3.zero;
    public Vector3 spawnPosition;

    public float rotationSpeed;
    public Vector3 rotationAxis = Vector3.zero;

    public int  rangeLimit;
    public Vector3 rangeAxis;

    public Color color;
    public Color colorChange;

    public Material material;

    private void Awake() {
        boundToggle = GameObject.Find("Bounded").GetComponent<Toggle>();
        boundToggle.isOn = isBound;
    }

    private void FixedUpdate() {
        // cahnge color
        ChangeColor();
        // rotation
        Rotate();
        isBound = boundToggle.isOn;
    }

    public void Move(Vector3 dir) {
        transform.Translate(dir * speed * Time.deltaTime);
    }

    // color change of obj
    public void ChangeColor() {
        material.color = (posDir) ? color : colorChange;
    }

    public void Rotate() {
        // cube - about y-axis, 90-degrees per second
        transform.rotation *= Quaternion.AngleAxis(rotationSpeed * Time.deltaTime, Vector3.up);

    }

    public void BindObjectBounds(bool b) {
        isBound = b;
    }

    
}

