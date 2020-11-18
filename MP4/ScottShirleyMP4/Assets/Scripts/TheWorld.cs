using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[ExecuteInEditMode]
public class TheWorld : MonoBehaviour
{
    public SceneNode TheRoot;

    // Update is called once per frame
    void Update()
    {
        Matrix4x4 i = Matrix4x4.identity;
        TheRoot.CompositeXform(ref i);

        if (Input.GetKeyDown(KeyCode.H)) {
            SceneManager.LoadScene("ScottShirleyMP4");
        }
    }
}
