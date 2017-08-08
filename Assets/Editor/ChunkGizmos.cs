using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(Chunk))]
public class ChunkGizmos : Editor {

	void OnSceneGUI()
    {
        Chunk chunk = target as Chunk;

        Handles.color = Color.green;
        Vector3 boxHalfLength = chunk.transform.forward.normalized * chunk.spawnBounds.z / 2;
        Vector3 boxHalfWidth = chunk.transform.right.normalized * chunk.spawnBounds.x / 2;
        Vector3 boxHalfHeight = chunk.transform.up.normalized * chunk.spawnBounds.y / 2;

        Vector3 spawnCenter = chunk.transform.position + chunk.spawnBoundsCenter;

        Handles.DrawLine(spawnCenter + boxHalfLength + boxHalfHeight + boxHalfWidth, spawnCenter + boxHalfLength + boxHalfHeight - boxHalfWidth);
        Handles.DrawLine(spawnCenter - boxHalfLength + boxHalfHeight + boxHalfWidth, spawnCenter - boxHalfLength + boxHalfHeight - boxHalfWidth);
        Handles.DrawLine(spawnCenter + boxHalfLength - boxHalfHeight + boxHalfWidth, spawnCenter + boxHalfLength - boxHalfHeight - boxHalfWidth);
        Handles.DrawLine(spawnCenter - boxHalfLength - boxHalfHeight + boxHalfWidth, spawnCenter - boxHalfLength - boxHalfHeight - boxHalfWidth);

        Handles.DrawLine(spawnCenter + boxHalfLength + boxHalfHeight + boxHalfWidth, spawnCenter - boxHalfLength + boxHalfHeight + boxHalfWidth);
        Handles.DrawLine(spawnCenter + boxHalfLength + boxHalfHeight - boxHalfWidth, spawnCenter - boxHalfLength + boxHalfHeight - boxHalfWidth);
        Handles.DrawLine(spawnCenter + boxHalfLength - boxHalfHeight + boxHalfWidth, spawnCenter - boxHalfLength - boxHalfHeight + boxHalfWidth);
        Handles.DrawLine(spawnCenter + boxHalfLength - boxHalfHeight - boxHalfWidth, spawnCenter - boxHalfLength - boxHalfHeight - boxHalfWidth);

        Handles.DrawLine(spawnCenter + boxHalfLength + boxHalfHeight + boxHalfWidth, spawnCenter + boxHalfLength - boxHalfHeight + boxHalfWidth);
        Handles.DrawLine(spawnCenter + boxHalfLength + boxHalfHeight - boxHalfWidth, spawnCenter + boxHalfLength - boxHalfHeight - boxHalfWidth);
        Handles.DrawLine(spawnCenter - boxHalfLength + boxHalfHeight + boxHalfWidth, spawnCenter - boxHalfLength - boxHalfHeight + boxHalfWidth);
        Handles.DrawLine(spawnCenter - boxHalfLength + boxHalfHeight - boxHalfWidth, spawnCenter - boxHalfLength - boxHalfHeight - boxHalfWidth);

        Handles.color = Color.green;        
        chunk.spawnBounds.x = Handles.ScaleValueHandle(chunk.spawnBounds.x, spawnCenter + boxHalfWidth, chunk.transform.rotation, .2f, Handles.CubeCap, 1);
        chunk.spawnBounds.x = Handles.ScaleValueHandle(chunk.spawnBounds.x, spawnCenter - boxHalfWidth, chunk.transform.rotation, .2f, Handles.CubeCap, 1);
        chunk.spawnBounds.y = Handles.ScaleValueHandle(chunk.spawnBounds.y, spawnCenter + boxHalfHeight, chunk.transform.rotation, .2f, Handles.CubeCap, 1);
        chunk.spawnBounds.y = Handles.ScaleValueHandle(chunk.spawnBounds.y, spawnCenter - boxHalfHeight, chunk.transform.rotation, .2f, Handles.CubeCap, 1);
        chunk.spawnBounds.z = Handles.ScaleValueHandle(chunk.spawnBounds.z, spawnCenter + boxHalfLength, chunk.transform.rotation, .2f, Handles.CubeCap, 1);
        chunk.spawnBounds.z = Handles.ScaleValueHandle(chunk.spawnBounds.z, spawnCenter - boxHalfLength, chunk.transform.rotation, .2f, Handles.CubeCap, 1);
    }
}
