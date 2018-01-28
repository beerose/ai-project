using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MathUtils {
    static System.Func<Color> randomColor = () => new Color(Random.value, Random.value, Random.value);

    static System.Action<Vector2, Color> drawCorner = (Vector2 corner, Color color) =>
    {
        Debug.DrawLine(
            new Vector3(corner.x, 0, corner.y),
            new Vector3(corner.x, 0, corner.y) + Vector3.up * 30,
            randomColor(),
            30
        );
    };

    static System.Action<RoomModel> drawModelRect = (RoomModel room) =>
    {
        var color = randomColor();
        drawCorner(room.LeftCorner, color);
        drawCorner(room.RightCorner, color);
    };

    public static Vector2 GetRandomPointInCircle(float radius) {
        float r;
        float t = 2 * Mathf.PI * Random.value;
        float u = Random.value * Random.value;
        if (u > 1) {
            r = 2 - u;
        } else {
            r = u;
        };
        return new Vector2
        {
            x = Mathf.Ceil(radius * r * Mathf.Cos(t)),
            y = Mathf.Ceil(radius * r * Mathf.Sin(t))
        };
    }

    public static Vector2 GetRandomSize() {
        var v = new Vector2(Random.Range(4, 15), Random.Range(4, 15)) * 2;
        return v;
    }

    public static bool RoomsOverlap(RoomModel a, RoomModel b)
    {
        var rectA = new Rect(a.LeftCorner, a.size);
        var rectB = new Rect(b.LeftCorner, b.size);
        return rectA.Overlaps(rectB) || rectA.Contains(b.position) || rectB.Contains(a.position);
    }

    public static bool AnyRoomOverlaps(List<RoomModel> rooms, RoomModel newRoom) {
        return rooms.Any(room => RoomsOverlap(room, newRoom));
    }
}


