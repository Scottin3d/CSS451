using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CubeBehavior : ObjectBehavior {

    //public Material material;
    private void Start() {
        spawnPosition = transform.position;

        offset = scale / 2f;
        transform.position = new Vector3(transform.position.x, offset, transform.position.z);

        material = GetComponent<MeshRenderer>().material;
        //material = Instantiate(material);
        material.color = color;
    }
    private void Update() {
        // get direction vector
        moveDirection = posDir ? Vector3.up : Vector3.down;

        // move
        Move(moveDirection);

        // update movement direction
        if (transform.position.y >= rangeLimit + offset) {
            posDir = false;
        }

        if (transform.position.y <= offset) {
            posDir = true;
        }
    }
}
