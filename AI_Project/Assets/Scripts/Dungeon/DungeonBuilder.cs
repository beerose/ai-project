using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Dungeon {
    public class DungeonBuilder : MonoBehaviour
    {
        public GameObject RoomPrefab;
        public int seed;
        public int roomCount;
        public int radius;

        public void CollectRooms()
        {
            Random.InitState(1211);
            Debug.Log(Random.value);
        }

        public GameObject CreateRoomGameObject(RoomModel roomModel)
        {
            GameObject roomGameObject = Instantiate(RoomPrefab, Vector3.zero, Quaternion.identity, transform);
            var room = roomGameObject.GetComponent<BuildableRoomBehaviour>();
            room.SetupFromModel(roomModel);
            //behaviour.SetDoors(roomModel.Neighbours);
            return roomGameObject;
        }

        public void BuildDungeon(int radius, int seed, int roomCount)
        {
            var oldRandomState = Random.state;
            Random.InitState(seed);

            var placedRooms = new List<RoomModel>(roomCount);
            var position = MathUtils.GetRandomPointInCircle(radius);
            var size = MathUtils.GetRandomSize();

            placedRooms.Add(new RoomModel(size, position));

            while (placedRooms.Count < roomCount)
            {
                var childSize = MathUtils.GetRandomSize();
                var parent = placedRooms[Random.Range(0, placedRooms.Count)];

                var cantPlaceNewRoom = System.Array.IndexOf(parent.Neighbours, null) == -1;
                if (cantPlaceNewRoom) continue;

                var newRoomSlot = parent.Neighbours
                      .Select((room, index) => new { room, index })
                      .Where(x => x.room == null)
                      .Select(x => x.index)
                      .ToList()
                      .GetRandom();

                   
                RoomModel newRoomModel = null;
                switch (newRoomSlot)
                {
                    case (int)Direction.North:
                        newRoomModel = new RoomModel(
                                childSize,
                                parent.position + new Vector2(0, parent.size.y / 2) + new Vector2(0, childSize.y / 2)
                        );
                        break;
                    case (int)Direction.West:
                        newRoomModel = new RoomModel(
                            childSize,
                            parent.position + new Vector2(parent.size.x / 2, 0) + new Vector2(childSize.x / 2, 0)
                        );
                        break;
                    case (int)Direction.East:
                        newRoomModel = new RoomModel(
                                childSize,
                                parent.position - new Vector2(parent.size.x / 2, 0) - new Vector2(childSize.x / 2, 0)
                        );
                        break;
                    case (int)Direction.South: //botom
                        newRoomModel = new RoomModel(
                                childSize,
                                parent.position - new Vector2(0, parent.size.y / 2) - new Vector2(0, childSize.y / 2)
                        );
                        break;
                    default:
                        throw new UnityException("wtf");
                }
                if (newRoomModel != null && !MathUtils.AnyRoomOverlaps(placedRooms, newRoomModel))
                {
                    placedRooms.Add(newRoomModel);
                    parent.Neighbours[newRoomSlot] = newRoomModel;
                    newRoomModel.Neighbours[(newRoomSlot + 2) % 4] = parent;
                };
            }

            foreach (RoomModel room in placedRooms)
            {
                CreateRoomGameObject(room);
            }

            Random.state = oldRandomState;
        }



        private RoomModel[] Separate(RoomModel[] rooms)
        {
            for (var i = 0; i < rooms.Length; i++)
            {
                Debug.Log("outer loop");
                RoomModel a = rooms[i];
                for (var j = i + 1; j < rooms.Length; j++)
                {
                    Debug.Log("inner loop");
                    RoomModel b = rooms[j];
                    if (MathUtils.RoomsOverlap(a, b))
                    {
                        Debug.Log("rooms overlapps");
                        var dx = Mathf.Ceil(Mathf.Min(a.Right - b.Left, a.Left - b.Right));
                        var dy = Mathf.Ceil(Mathf.Min(a.Bottom - b.Top, a.Top - b.Bottom));

                        if (Mathf.Abs(dx) < Mathf.Abs(dy))
                        {
                            dy = 0;
                        }
                        else
                        {
                            dx = 0;
                        }

                        Debug.Log(a.position);
                        Debug.Log(b.position);
                        Debug.Log(dx);
                        Debug.Log(dy);

                        var dxa = -dx / 2;
                        var dxb = dx + dxa - 1;

                        var dya = -dy / 2;
                        var dyb = dya + dy - 1;

                        rooms[i] = shiftRoom(a, dxa, dya);
                        rooms[j] = shiftRoom(b, dxb, dyb);

                    }
                }
            }
            return rooms;
        }

        private RoomModel shiftRoom(RoomModel room, float dx, float dy)
        {
            room.position.x += (int)dx;
            room.position.y += (int)dy;
            return room;
        }
    }

}
