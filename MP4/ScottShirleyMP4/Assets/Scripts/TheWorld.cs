using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[ExecuteInEditMode]
public class TheWorld : MonoBehaviour
{
    public SceneNode TheRoot;

    bool isPause = false;
    int timeScale = 1;

    void Start() {
        
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = timeScale;
        Matrix4x4 i = Matrix4x4.identity;
        TheRoot.CompositeXform(ref i);

        if (Input.GetKeyDown(KeyCode.H)) {
            SceneManager.LoadScene("ScottShirleyMP4");
        }

        if (Input.GetKeyDown(KeyCode.P)) {
            timeScale = (isPause) ? 1 : 0;
            isPause = !isPause;
        }

        if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }
    }
}
