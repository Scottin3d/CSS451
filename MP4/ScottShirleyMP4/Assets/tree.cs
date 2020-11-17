using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tree : MonoBehaviour
{
    public Color treeColor = Color.white;
    MeshRenderer mr;
    private void Start() {
        mr = GetComponent<MeshRenderer>();
        mr.material = Instantiate<Material>(mr.material);
    }

    private void Update() {
        mr.material.color = treeColor;
    }
}
