using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class miniCameraScript : MonoBehaviour
{

    public Transform lookAt = null;
    public Transform miniCam = null;
    public Vector3 miniCamPos = Vector3.zero;

    public GameObject root = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        miniCam = TreeTip(root.transform);
        transform.rotation = Quaternion.FromToRotation(Vector3.up, miniCam.up);

        miniCamPos = miniCam.GetComponent<SceneNode>().headTip;
        transform.localPosition = miniCamPos;

    }

    private Transform TreeTip(Transform t) {
        if (t.childCount == 0) {
            return t;
        }
        return TreeTip(t.GetChild(0));
    }
}
