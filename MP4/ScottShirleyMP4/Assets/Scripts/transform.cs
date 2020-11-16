using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class transform : MonoBehaviour
{

    public GameObject A;
    public GameObject B;
    public GameObject C;

    public Vector3 pivot;

    public Transform Mb;
    public Transform Mt;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Matrix4x4 m = Matrix4x4.TRS(translation, rotation, scale);
    }

    // Mt * A
    // TRS T(p) * A
}
