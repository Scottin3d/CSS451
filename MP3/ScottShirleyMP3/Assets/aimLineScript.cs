using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class aimLineScript : MonoBehaviour {
    [SerializeField]
    GameObject p1;

    [SerializeField]
    GameObject p2;

    [Range(0f,5f)]
    public float lineWidth = 0.1f;

    [SerializeField]
    Color color = Color.black;

    private void Start() {
        InitializeComponents();
    }

    void InitializeComponents() {
        GetComponent<MeshRenderer>().material.color = color;
        if (p1 == null) {
            p1 = GameObject.Find("westWallEndPt");
        }
        if (p2 == null) {
            p2 = GameObject.Find("eastWallEndPt");
        }
    }

    private void Update() {
        Utils.vectorUtils.AdjustLine(this.gameObject, p1.transform.position, p2.transform.position, lineWidth);
        /*
        Vector3 V = p2.transform.position - p1.transform.position;
        float length = V.magnitude;
        transform.localPosition = p1.transform.position + 0.5f * V;
        transform.localScale = new Vector3(lineWidth, length * 0.5f, lineWidth);
        transform.localRotation = Quaternion.FromToRotation(Vector3.up, V);
        */
    }

    public Quaternion TravelDirection() {
        Vector3 V = p2.transform.position - p1.transform.position;
        return Quaternion.FromToRotation(Vector3.up, V);
    }
}
