using UnityEngine;
using UnityEditor;
using System.Collections;

namespace ForceZones
{
    [CustomEditor(typeof(ForceZone))]
    public class ForceZoneHandles : Editor
    {
        public void OnSceneGUI()
        {
            EditorGUI.BeginChangeCheck();
            ForceZone forceZone = target as ForceZone;
            switch (forceZone.forceZoneShape)
            {
                case ForceZone.ForceZoneShape.Box:
                    //Draw Box
                    Handles.color = Color.green;
                    Vector3 boxHalfLength = forceZone.transform.forward.normalized * forceZone.length / 2;
                    Vector3 boxHalfWidth = forceZone.transform.right.normalized * forceZone.width / 2;
                    Vector3 boxHalfHeight = forceZone.transform.up.normalized * forceZone.height / 2;

                    Handles.DrawLine(forceZone.transform.position + boxHalfLength + boxHalfHeight + boxHalfWidth, forceZone.transform.position + boxHalfLength + boxHalfHeight - boxHalfWidth);
                    Handles.DrawLine(forceZone.transform.position - boxHalfLength + boxHalfHeight + boxHalfWidth, forceZone.transform.position - boxHalfLength + boxHalfHeight - boxHalfWidth);
                    Handles.DrawLine(forceZone.transform.position + boxHalfLength - boxHalfHeight + boxHalfWidth, forceZone.transform.position + boxHalfLength - boxHalfHeight - boxHalfWidth);
                    Handles.DrawLine(forceZone.transform.position - boxHalfLength - boxHalfHeight + boxHalfWidth, forceZone.transform.position - boxHalfLength - boxHalfHeight - boxHalfWidth);

                    Handles.DrawLine(forceZone.transform.position + boxHalfLength + boxHalfHeight + boxHalfWidth, forceZone.transform.position - boxHalfLength + boxHalfHeight + boxHalfWidth);
                    Handles.DrawLine(forceZone.transform.position + boxHalfLength + boxHalfHeight - boxHalfWidth, forceZone.transform.position - boxHalfLength + boxHalfHeight - boxHalfWidth);
                    Handles.DrawLine(forceZone.transform.position + boxHalfLength - boxHalfHeight + boxHalfWidth, forceZone.transform.position - boxHalfLength - boxHalfHeight + boxHalfWidth);
                    Handles.DrawLine(forceZone.transform.position + boxHalfLength - boxHalfHeight - boxHalfWidth, forceZone.transform.position - boxHalfLength - boxHalfHeight - boxHalfWidth);

                    Handles.DrawLine(forceZone.transform.position + boxHalfLength + boxHalfHeight + boxHalfWidth, forceZone.transform.position + boxHalfLength - boxHalfHeight + boxHalfWidth);
                    Handles.DrawLine(forceZone.transform.position + boxHalfLength + boxHalfHeight - boxHalfWidth, forceZone.transform.position + boxHalfLength - boxHalfHeight - boxHalfWidth);
                    Handles.DrawLine(forceZone.transform.position - boxHalfLength + boxHalfHeight + boxHalfWidth, forceZone.transform.position - boxHalfLength - boxHalfHeight + boxHalfWidth);
                    Handles.DrawLine(forceZone.transform.position - boxHalfLength + boxHalfHeight - boxHalfWidth, forceZone.transform.position - boxHalfLength - boxHalfHeight - boxHalfWidth);

                    Handles.color = Color.green;
                    forceZone.height = Handles.ScaleValueHandle(forceZone.height, forceZone.transform.position + boxHalfHeight, forceZone.transform.rotation, .2f, Handles.CubeCap, 1);
                    forceZone.height = Handles.ScaleValueHandle(forceZone.height, forceZone.transform.position - boxHalfHeight, forceZone.transform.rotation, .2f, Handles.CubeCap, 1);
                    forceZone.width = Handles.ScaleValueHandle(forceZone.width, forceZone.transform.position + boxHalfWidth, forceZone.transform.rotation, .2f, Handles.CubeCap, 1);
                    forceZone.width = Handles.ScaleValueHandle(forceZone.width, forceZone.transform.position - boxHalfWidth, forceZone.transform.rotation, .2f, Handles.CubeCap, 1);
                    forceZone.length = Handles.ScaleValueHandle(forceZone.length, forceZone.transform.position + boxHalfLength, forceZone.transform.rotation, .2f, Handles.CubeCap, 1);
                    forceZone.length = Handles.ScaleValueHandle(forceZone.length, forceZone.transform.position - boxHalfLength, forceZone.transform.rotation, .2f, Handles.CubeCap, 1);

                    break;
                case ForceZone.ForceZoneShape.Cylinder:
                    //Draw Cylinder
                    Handles.color = Color.green;
                    Vector3 cylHalfHeight = forceZone.transform.forward.normalized * forceZone.height / 2;

                    Handles.DrawWireDisc(forceZone.transform.position + cylHalfHeight, forceZone.transform.forward, forceZone.radius);
                    Handles.DrawWireDisc(forceZone.transform.position - cylHalfHeight, forceZone.transform.forward, forceZone.radius);
                    Handles.DrawLine(forceZone.transform.position + cylHalfHeight + (forceZone.transform.up.normalized * forceZone.radius), forceZone.transform.position - cylHalfHeight + (forceZone.transform.up.normalized * forceZone.radius));
                    Handles.DrawLine(forceZone.transform.position + cylHalfHeight + (forceZone.transform.up.normalized * -forceZone.radius), forceZone.transform.position - cylHalfHeight + (-forceZone.transform.up.normalized * forceZone.radius));
                    Handles.DrawLine(forceZone.transform.position + cylHalfHeight + (forceZone.transform.right.normalized * forceZone.radius), forceZone.transform.position - cylHalfHeight + (forceZone.transform.right.normalized * forceZone.radius));
                    Handles.DrawLine(forceZone.transform.position + cylHalfHeight + (forceZone.transform.right.normalized * -forceZone.radius), forceZone.transform.position - cylHalfHeight + (-forceZone.transform.right.normalized * forceZone.radius));

                    //Draw Scale Handles
                    Handles.color = Color.yellow;
                    forceZone.radius = Handles.ScaleValueHandle(forceZone.radius, forceZone.transform.position + cylHalfHeight + (forceZone.transform.up.normalized * forceZone.radius), forceZone.transform.rotation, .2f, Handles.CubeCap, 1);
                    forceZone.height = Handles.ScaleValueHandle(forceZone.height, forceZone.transform.position + cylHalfHeight, forceZone.transform.rotation, .2f, Handles.CubeCap, 1);
                    break;
                default:
                    break;
            }

            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(forceZone);
            }
        }
    }
}