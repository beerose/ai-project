using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Dungeon {
    public class BuildableRoomBehaviour : MonoBehaviour
    {
        int NORTH = (int)Direction.North;
        int EAST = (int)Direction.East;
        int SOUTH = (int)Direction.South;
        int WEST = (int)Direction.West;

        public Transform[] Walls;
        public Transform[] Doors;
        public Transform Floor;
        public Transform Roof;

        private Transform[] CollectDescendants(string containerChildName)
        {
            var list = new List<Transform>();
            var container = transform.Find(containerChildName);
            foreach (Transform t in container)
            {
                list.Add(t);
            }
            return list.ToArray();
        }

        public void CollectDoorsAndWalls()
        {
            Floor = transform.Find("Floor");
            Roof = transform.Find("Roof");
            Walls = CollectDescendants("Walls");
            Doors = CollectDescendants("Doors");
        }

        private Vector2 _size;
        public Vector2 Size
        {
            get
            {
                return _size;
            }
            set
            {
                SetSize(value);
            }
        }

        private Vector3 _position;
        public Vector3 Position
        {
            get
            {
                return _position;
            }
            set
            {
                SetPosition(value);
            }
        }

        public void SetupFromModel(RoomModel roomModel)
        {
            SetSize(roomModel.size);
            SetDoors(roomModel.Neighbours);
            SetPosition(roomModel.position);
        }


        public void SetSize(Vector2 size)
        {
            var floorScale = Floor.localScale;
            var roofScale = Roof.localScale;
            floorScale.z = size.y / 10;
            floorScale.x = size.x / 10;
            roofScale.z = size.y + 0.01f;
            roofScale.x = size.x + 0.01f;
            Floor.localScale = floorScale;
            Roof.localScale = roofScale;

            var northScale = Walls[NORTH].localScale;
            northScale.x = size.x;
            Walls[NORTH].localScale = northScale;
            var northPos = Walls[NORTH].position;
            northPos.z = size.y / 2 - 0.25f;
            Walls[NORTH].position = northPos;

            var southScale = Walls[SOUTH].localScale;
            southScale.x = size.x;
            Walls[SOUTH].localScale = northScale;
            var southPos = Walls[SOUTH].position;
            southPos.z = -size.y / 2 + 0.25f;
            Walls[SOUTH].position = southPos;

            var westScale = Walls[WEST].localScale;
            westScale.x = size.y;
            Walls[WEST].localScale = westScale;
            var westPos = Walls[WEST].position;
            westPos.x = -size.x / 2 + .25f;
            Walls[WEST].position = westPos;

            var eastScale = Walls[EAST].localScale;
            eastScale.x = size.y;
            Walls[EAST].localScale = eastScale;
            var eastPos = Walls[EAST].position;
            eastPos.x = size.x / 2 - .25f;
            Walls[EAST].position = eastPos;

            _size = size;
        }

        public void SetDoors(RoomModel[] neighbours)
        {
            var northPos = Doors[NORTH].position;
            northPos.z = _size.y / 2 - .6f;
            Doors[NORTH].position = northPos;

            var southPos = Doors[SOUTH].position;
            southPos.z = -_size.y / 2 + .6f;
            Doors[SOUTH].position = southPos;

            var westPos = Doors[WEST].position;
            westPos.x = _size.x / 2 - .6f;
            Doors[WEST].position = westPos;

            var eastPos = Doors[EAST].position;
            eastPos.x = -_size.x / 2 + .6f;
            Doors[EAST].position = eastPos;

            int i = 0;
            Debug.Log(
                string.Join(
                    ", ",
                    neighbours
                        .Select(x => x == null ? "null" : x.ToString())
                        .ToArray()
                )
            );
            foreach (var neighbour in neighbours)
            {
                if (neighbour == null) Doors[i].gameObject.SetActive(false);
                i++;
            }
        }

        public void SetPosition(Vector2 pos)
        {
            var position = new Vector3(pos.x, 0, pos.y);
            _position = position;
            transform.position = position;
        }


        void Awake()
        {
        }

        void Update()
        {
        }
    }

}
