using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class MP2ObjectBehavior : MonoBehaviour
{
    public Color color;
    Color colorSelect = Color.yellow;
    float alpha = 0.25f;
    bool selected = false;
    int childCount = 0;

    Material material;

    Vector3 originalState;

    // Start is called before the first frame update
    void Start()
    {
        colorSelect.a = alpha;
        material = GetComponent<MeshRenderer>().material;
        originalState = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            if (!EventSystem.current.IsPointerOverGameObject()) {
                Selected(false);
            }
        }

        if (selected) {
            material.color = colorSelect;
        } else {
            material.color = color;
        }
    }

    public void Selected(bool b) {
        selected = b;
    }

    public void AddChild() {
        childCount++;
    }

    public int ChildCount() {
        return childCount;
    }

    public void ResetTransform() {
        transform.position = originalState;
        transform.eulerAngles = Vector3.zero;
        transform.localScale = Vector3.one;
    }



}
