using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MP4DragCameraScript : MonoBehaviour {
    public Vector3 panStart;

    void Update() {

        if (Input.GetMouseButtonDown(0)) {
            panStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(0)) { 
            Vector3 direction = panStart - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position += direction;
        }
    }


}
