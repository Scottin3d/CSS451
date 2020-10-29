using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utils {
    public static class vectorUtils {

        // raycast - returns intersection point on a plane
        public static bool ScottCast(out Vector3 intersection, Vector3 linePoint, Vector3 lineVec, Vector3 planeNormal, Vector3 planePoint) {
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

                //vector = SetVectorLength(lineVec, length);

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
    }

}