using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MP4LookAtObj : MonoBehaviour
{

    public void UpdateXPosition(float value) {
        Vector3 position = transform.position;
        position.x = value;
        transform.position = position;
    }

    public void UpdateYPosition(float value) {
        Vector3 position = transform.position;
        position.y = value;
        transform.position = position;
    }

    public void UpdateZPosition(float value) {
        Vector3 position = transform.position;
        position.z = value;
        transform.position = position;
    }
}
