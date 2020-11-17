using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(primitiveMovement))]
public class primitiveMovementEditor : Editor
{
    //SerializedProperty child;
    void OnEnable() {
        //child = serializedObject.FindProperty("child");
    }
    public override void OnInspectorGUI() {
        //base.OnInspectorGUI();
        primitiveMovement myScript = target as primitiveMovement;

        EditorGUILayout.LabelField("Rotation Object", EditorStyles.boldLabel);
        //serializedObject.Update();
        //child.objectReferenceValue = (GameObject)EditorGUILayout.ObjectField("Child", child.objectReferenceValue, typeof(GameObject), true);
        //serializedObject.ApplyModifiedProperties();
        //myScript.child = (GameObject)child.objectReferenceValue;

        myScript.pivot = EditorGUILayout.Vector3Field("Rotation Pivot", myScript.pivot);



        // x
        EditorGUILayout.LabelField("X Rotation", EditorStyles.boldLabel);
        myScript.xRotate = EditorGUILayout.Toggle("Use Rotation", myScript.xRotate);
        if (myScript.xRotate) {
            myScript.xRotateSpeed = EditorGUILayout.FloatField("Rotation Speed", myScript.xRotateSpeed);
            myScript.xBound = EditorGUILayout.Toggle("Bound Rotation", myScript.xBound);
            
            if (myScript.xBound) {
                myScript.xMaxAngle = EditorGUILayout.FloatField("Max Angle", myScript.xMaxAngle);
            }
        }

        // y
        EditorGUILayout.LabelField("Y Rotation", EditorStyles.boldLabel);
        myScript.yRotate = EditorGUILayout.Toggle("Use Rotation", myScript.yRotate);
        if (myScript.yRotate) {
            myScript.yRotateSpeed = EditorGUILayout.FloatField("Rotation Speed", myScript.yRotateSpeed);
            myScript.yBound = EditorGUILayout.Toggle("Bound Rotation", myScript.yBound);

            if (myScript.yBound) {
                myScript.yMaxAngle = EditorGUILayout.FloatField("Max Angle", myScript.xMaxAngle);
            }
        }

        // z
        EditorGUILayout.LabelField("Z Rotation", EditorStyles.boldLabel);
        myScript.zRotate = EditorGUILayout.Toggle("Use Rotation", myScript.zRotate);
        if (myScript.zRotate) {
            myScript.zRotateSpeed = EditorGUILayout.FloatField("Rotation Speed", myScript.zRotateSpeed);
            myScript.zBound = EditorGUILayout.Toggle("Bound Rotation", myScript.zBound);

            if (myScript.zBound) {
                myScript.zMaxAngle = EditorGUILayout.FloatField("Max Angle", myScript.zMaxAngle);
            }
        }
    }
}
