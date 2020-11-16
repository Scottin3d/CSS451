using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MP4DragCameraScript : MonoBehaviour {
    public Vector3 panStart;
    public float distance;

    private Vector3 mouseOrigin;
    //pan
    public float panSpeed = 0.5f;
    private bool isPanning;

    void Update() {

        // Get the right mouse button
        if (Input.GetMouseButtonDown(1)) {
            // Get mouse origin
            mouseOrigin = Input.mousePosition;
            isPanning = true;
        }

        if (!Input.GetMouseButton(1)) isPanning = false;

        // Move the camera on it's XY plane
        if (isPanning) {
            Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - mouseOrigin);

            Vector3 move = new Vector3(pos.x * panSpeed, pos.y * panSpeed, 0);
            transform.Translate(move, Space.Self);
        }
    }


}
