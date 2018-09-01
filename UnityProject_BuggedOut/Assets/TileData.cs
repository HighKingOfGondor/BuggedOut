using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[System.Serializable]
public class TileData
{
    public TileBase tileBase;
    public Vector3Int position;

    public TileData previousData;

    public ulong pathID = 0;
    public bool isOpen = false;
    public bool isClosed = false;
    public bool isWalkable = true;
    public int gScore = int.MaxValue;
    public int fScore = int.MaxValue;

    public TileData(TileBase newTileBase, Vector3Int newPosition, bool walkable)
    {
        tileBase = newTileBase;
        position = newPosition;
        isWalkable = walkable;
        previousData = null;
    }

}
