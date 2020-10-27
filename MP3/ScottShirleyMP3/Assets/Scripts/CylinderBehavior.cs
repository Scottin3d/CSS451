using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylinderBehavior : ObjectBehavior
{
    //public Material material;

    private void Start() {
        spawnPosition = transform.position;

        offset = scale * 2f;
        transform.position = new Vector3(transform.position.x, offset, transform.position.z);
        transform.localScale = new Vector3(scale, 2 * scale, scale);

        material = GetComponent<MeshRenderer>().material;
        //material = Instantiate(material);
        material.color = color;
    }

    // Update is called once per frame
    void Update()
    {
        // get direction vector
        moveDirection = posDir ? Vector3.forward : Vector3.back;

        // move
        Move(moveDirection);

        if (!isBound) {
            if (transform.position.z >= rangeLimit) {
                posDir = false;
            }

            if (transform.position.z <= 0) {
                posDir = true;
            }
        } else {
            // unbound by 0 -5
            if (transform.position.z >= spawnPosition.z + (rangeLimit / 2)
                || transform.position.z >= 7 - (scale / 2)) {
                posDir = false;
            }

            if (transform.position.z <= spawnPosition.z - (rangeLimit / 2)
                || transform.position.z <= -7 + (scale / 2)) {
                posDir = true;
            }
        }
    }
}
