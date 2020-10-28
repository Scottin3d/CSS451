using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.UI;


public class endPointScript : MonoBehaviour
{
    [Header("Transform Lock")]
    [SerializeField]
    bool[] lockAxis = new bool[3];


    [SerializeField]
    Color color = Color.red;

    private void Start() {
        GetComponent<MeshRenderer>().material.color = color;
    }

    public bool LockX() {
        return lockAxis[0];
    }
    public bool LockY() { 
        return lockAxis[1];
    }
    public bool LockZ() {
        return lockAxis[2];
    }


}
