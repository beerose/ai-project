using UnityEngine;
using UnityEditor;

using Dungeon;

[CustomEditor(typeof(DungeonBuilder))]
public class DungeonEditor : Editor
{
    Vector2 cachedRoomSize;
    RoomModel room = new RoomModel(Vector2.one, Vector3.zero);

    private void clearRooms(DungeonBuilder dungeon) {
        var childCount = dungeon.transform.childCount;
        for (var i = childCount - 1; i >= 0; --i)
        {
            DestroyImmediate(dungeon.transform.GetChild(i).gameObject);
        }
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUILayout.Separator();

        DungeonBuilder dungeon = (DungeonBuilder)target;

        room.position = EditorGUILayout.Vector3Field("Room position", room.position);
        room.size = EditorGUILayout.Vector2Field("Room size", room.size);

        if (GUILayout.Button("Create room"))
        {
            dungeon.CreateRoomGameObject(room);
        }

        var seed = dungeon.seed;
        var radius = dungeon.radius;
        var roomCount = dungeon.roomCount;

        if (GUILayout.Button("Create rooms"))
        {
            clearRooms(dungeon);
            dungeon.BuildDungeon(radius, seed, roomCount);
        }

        if (GUILayout.Button("Clear rooms"))
        {
            clearRooms(dungeon);
        }
    }
}