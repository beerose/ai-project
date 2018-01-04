using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(BuildableRoomBehaviour))]
public class LookAtPointEditor : Editor
{
    Vector2 cachedRoomSize;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUILayout.Separator();

        BuildableRoomBehaviour room = (BuildableRoomBehaviour) target;
        cachedRoomSize = room.Size;
        cachedRoomSize = EditorGUILayout.Vector2Field("Room size", room.Size);
        if (cachedRoomSize != room.Size)
        {
            room.SetSize(cachedRoomSize);
        }

        if (GUILayout.Button("Collect doors and walls"))
        {
            room.CollectDoorsAndWalls();    
        }
    }
}