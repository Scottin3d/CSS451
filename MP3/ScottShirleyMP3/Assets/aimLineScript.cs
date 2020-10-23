using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aimLineScript : MonoBehaviour
{
    [SerializeField]
    Transform p1;

    [SerializeField]
    Transform p2;

    [SerializeField]
    float lineWidth = 1;

    private void Start() {
    }

    private void Update() {
        Vector3 V = p2.localPosition - p1.localPosition;
        float length = V.magnitude;

        transform.localPosition = p1.localPosition + 0.5f * V;
        transform.localScale = new Vector3(lineWidth, length * 0.5f, lineWidth);

        transform.localRotation = Quaternion.FromToRotation(Vector3.up, V);
    }
}
