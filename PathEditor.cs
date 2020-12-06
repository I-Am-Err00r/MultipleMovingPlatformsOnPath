using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MultiplePlatformManager), true)]
public class PathEditor : Editor
{
    protected virtual void OnSceneGUI()
    {
        int next = new int();
        Handles.color = Color.green;
        MultiplePlatformManager path = target as MultiplePlatformManager;
        for (int i = 0; i < path.numberOfPaths.Count; i++)
        {
            Vector2 position = path.numberOfPaths[i];
            next = i + 1;
            if (next == path.numberOfPaths.Count)
            {
                next = 0;
            }
            position = Handles.FreeMoveHandle(position, Quaternion.identity, .5f, new Vector2(.5f, .5f), Handles.CircleHandleCap);
            Handles.DrawDottedLine(path.numberOfPaths[i], path.numberOfPaths[next], 5);
            Handles.Label(path.numberOfPaths[i], "Path" + i.ToString());
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(target, "MoveablePlatform");
                path.numberOfPaths[i] = position;
            }
        }
        Handles.color = Color.red;
        MultiplePlatformManager platform = target as MultiplePlatformManager;
        for (int i = 0; i < platform.platformSpawns.Count; i++)
        {
            Vector2 position = platform.platformSpawns[i];
            next = i + 1;
            if (next == platform.platformSpawns.Count)
            {
                next = 0;
            }
            position = Handles.FreeMoveHandle(position, Quaternion.identity, .5f, new Vector2(.5f, .5f), Handles.RectangleHandleCap);
            Handles.DrawDottedLine(platform.platformSpawns[i], platform.platformSpawns[next], 5);
            Handles.Label(platform.platformSpawns[i], "Platform" + i.ToString());
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(target, "MoveablePlatform");
                platform.platformSpawns[i] = position;
            }
        }
    }
}