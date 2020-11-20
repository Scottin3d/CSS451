using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class miniCameraScript : MonoBehaviour
{

    public Transform lookAt = null;
    public Transform miniCamT = null;
    public Vector3 miniCamPos = Vector3.zero;

    public GameObject root = null;
    public GameObject lookAtLine = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        miniCamT = TreeTip(root.transform);
        transform.rotation = Quaternion.FromToRotation(Vector3.up, miniCamT.up);

        miniCamPos = miniCamT.GetComponent<SceneNode>().head.position;
        transform.position = miniCamPos;
        transform.rotation = Quaternion.FromToRotation(Vector3.up, miniCamT.up);

        Vector3 dir = transform.forward;
        Vector3 pos = dir * 15f + transform.position;
        lookAt.position = pos;

        AdjustLine(lookAtLine,transform.position, lookAt.position);
    }

    private Transform TreeTip(Transform t) {
        if (t.childCount == 0) {
            return t;
        }
        return TreeTip(t.GetChild(0));
    }

    void AdjustLine(GameObject line, Vector3 p1, Vector3 p2, float lineWidth = 0.1f) {
        Vector3 V = p2 - p1;
        float length = V.magnitude;
        line.transform.localPosition = p1 + 0.5f * V;
        line.transform.localScale = new Vector3(lineWidth, length * 0.5f, lineWidth);
        line.transform.localRotation = Quaternion.FromToRotation(Vector3.up, V);
    }
}
