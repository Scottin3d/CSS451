using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class testscript : MonoBehaviour
{
    public GameObject cylinder;
    Vector3 cylinderCenter;
    public Vector3 Ra;
    public Vector3 Rb;
    Vector3 cylinderNormal;
    public float cylinderRadius;

    public GameObject line;
    Vector3 linePoint;

    public float length;
    public float dsq;
    public float dot;


    private void Update() {
        linePoint = line.transform.position;
        cylinderCenter = cylinder.transform.position;
        cylinderNormal = -cylinder.transform.up;
        cylinderRadius = cylinder.transform.localScale.x / 2f;
        Ra = cylinderNormal.normalized * cylinder.transform.localScale.x + cylinder.transform.position;
        Rb = -cylinderNormal.normalized * cylinder.transform.localScale.x + cylinder.transform.position;


        Vector3 pDir = Rb - Ra;
        Vector3 tPoint = linePoint - Ra;
         length = Vector3.Distance(Ra, Rb);
         dot = Vector3.Dot(tPoint, pDir);

        if (dot < 0.0f || dot > length * length) {
            Debug.Log("Outside");
        } else {
             dsq = (tPoint.x * tPoint.x + tPoint.y * tPoint.y + tPoint.z * tPoint.z) - dot * dot / length;
            if (dsq > cylinderRadius) {
                Debug.Log("Outside");

            } else { 
            Debug.Log("Inside");

            }
        }
    }
}
