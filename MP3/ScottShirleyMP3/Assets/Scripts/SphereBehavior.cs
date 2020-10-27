using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereBehavior : ObjectBehavior {

    //public Material material;
    private void Start() {
        spawnPosition = transform.position;

        offset = scale / 2f;
        transform.position = new Vector3(transform.position.x, offset, transform.position.z);

        material = GetComponent<MeshRenderer>().material;
        //material = Instantiate(material);
        material.color = color;
    }
    // Update is called once per frame
    void Update()
    {
        // get direction vector
        moveDirection = posDir ? Vector3.right : Vector3.left;

        // move
        Move(moveDirection);


        if (!isBound) {
            // update movement direction
            if (transform.position.x >= rangeLimit) {
                posDir = false;
            }

            if (transform.position.x <= 0) {
                posDir = true;
            }
        } else {
            // update movement direction
            if (transform.position.x >= spawnPosition.x + (rangeLimit / 2) 
                || transform.position.x >= 7 - offset) {
                posDir = false;
            }

            if (transform.position.x <= spawnPosition.x - (rangeLimit / 2) 
                || transform.position.x <= -7 + offset) {
                posDir = true;
            }
        }
    }
}
