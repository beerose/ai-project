using UnityEngine;
using System.Collections;

public class RoomModel
{
    public Vector2 size;
    public Vector2 position;
    public RoomModel[] Neighbours;

    public RoomModel(Vector2 _size, Vector2 _position)
    {
        size = _size;
        position = _position;
        Neighbours = new RoomModel[4];
    }

    public override string ToString()
    {
        return string.Format(
            "[size: {0}, position: {1}]", 
            size, position);
    }

    public float Right
    {
        get
        {
            return position.x + size.x / 2;
        }
    }

    public float Left
    {
        get 
        {
			return position.x - size.x / 2;
            
        }
    }

    public float Top
    {
        get 
        {
            
            return position.y + size.y / 2;
        }
    }

    public float Bottom {
        get 
        {
            
			return position.y - size.y / 2;
        }
            
    }

    public Vector2 LeftCorner {
        get
        {
            return new Vector2(position.x - size.x / 2, position.y - size.y / 2);
        }
    }

    public Vector2 RightCorner
    {
        get
        {
            return new Vector2(position.x + size.x / 2, position.y + size.y / 2);
        }
    }

}
