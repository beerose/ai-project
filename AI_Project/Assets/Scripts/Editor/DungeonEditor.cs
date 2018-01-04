using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(DungeonBuilder))]
public class DungeonEditor : Editor
{
    Vector2 cachedRoomSize;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUILayout.Separator();

        DungeonBuilder dungeon = (DungeonBuilder)target;

        var position = dungeon.GetModel.position;
        var size = dungeon.GetModel.size;

        position = EditorGUILayout.Vector3Field("Room position", dungeon.GetModel.position);
        size = EditorGUILayout.Vector2Field("Room size", dungeon.GetModel.size);

		dungeon.SetModel(new DungeonBuilder.RoomModel() {
			position = position,
			size = size,
		});

        if (GUILayout.Button("Create room"))
        {
            Debug.LogFormat(position.ToString());
            Debug.Log(size);
            dungeon.CreateRoom(size, position);
        }
    }
}