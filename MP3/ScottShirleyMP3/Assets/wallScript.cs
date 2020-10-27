using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallScript : MonoBehaviour
{
    [SerializeField]
    GameObject point;

    private void Start() {
        point.transform.rotation = transform.rotation;
    }

    public void MovePoint(Vector3 _pos) {
        _pos.x = transform.position.x;
        point.transform.position = _pos;
    }
}
