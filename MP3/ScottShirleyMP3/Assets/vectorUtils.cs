using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utils {
    public static class vectorUtils {

        // raycast - returns intersection point on a plane
        public static bool ScottCast(out Vector3 intersection, Vector3 linePoint, Vector3 lineVec, 
                                         Vector3 planeNormal, Vector3 planePoint) {
            float length;
            float dotNumerator;
            float dotDenominator;
            Vector3 vector;
            intersection = Vector3.zero;

            //calculate the distance between the linePoint and the line-plane intersection point
            dotNumerator = Vector3.Dot((planePoint - linePoint), planeNormal);
            dotDenominator = Vector3.Dot(lineVec, planeNormal);

            if (dotDenominator != 0.0f) {
                length = dotNumerator / dotDenominator;
                vector = lineVec.normalized * length;
                intersection = linePoint + vector;
                return true;
            } else
                return false;
        }

        // draws a normal tangent line
        public static void DrawNormal(GameObject obj) {
            Vector3 pt = obj.transform.localPosition;
            pt.y += 2f;
            Debug.DrawLine(obj.transform.localPosition, pt, Color.red);
        }

        public static float Distance(Vector3 lhs, Vector3 rhs) {
            return (lhs - rhs).magnitude;
        }

        public static void AdjustLine(GameObject line, Vector3 p1, Vector3 p2, float lineWidth = 0.1f) {
            Vector3 V = p2 - p1;
            float length = V.magnitude;
            line.transform.localPosition = p1 + 0.5f * V;
            line.transform.localScale = new Vector3(lineWidth, length * 0.5f, lineWidth);
            line.transform.localRotation = Quaternion.FromToRotation(Vector3.up, V);
        }
    }

}