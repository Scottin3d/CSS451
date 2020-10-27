using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.UI;


public class endPointScript : MonoBehaviour
{
    [Header("Transform Lock")]
    public bool lockX;
    public bool lockY;
    public bool lockZ;


    [SerializeField]
    Color color = Color.red;

    private void Start() {
        GetComponent<MeshRenderer>().material.color = color;
    }



}
