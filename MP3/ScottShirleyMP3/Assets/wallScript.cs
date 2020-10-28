using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallScript : MonoBehaviour
{
    public GameObject point;
    private endPointScript eps;

    private void Start() {
        /* LineEndPt: This is a scaled cylinder S(2, 0.2, 2). 
         * A user can always left - mouse click and drag a LineEndPt.
         */
        eps = point.GetComponent<endPointScript>();
        point.transform.localScale = new Vector3(2f, 0.2f, 2f);
        point.transform.position = transform.position;
        point.transform.rotation = transform.rotation;
    }

    public void MovePoint(Vector3 _pos) {
        //check locked axis
        if (eps.LockX()) { 
            _pos.x = transform.position.x;
        }
        if (eps.LockY()) { 
            _pos.y = transform.position.y;
        }
        if (eps.LockZ()) {
            _pos.z = transform.position.z;
        }

        point.transform.position = _pos;
    }
}
