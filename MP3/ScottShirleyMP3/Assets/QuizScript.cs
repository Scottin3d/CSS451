using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizScript : MonoBehaviour
{

    Vector3 pos;
    Vector3 normal;
    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position;
        normal = transform.position.normalized;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
