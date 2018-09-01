using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PathfindingManager : MonoBehaviour {

    public Tilemap map;
    public TileBase baseTile;

    public Transform player;
    public Transform enemy;

    public string wallName = "wall";

    public ulong currentPathID = 0;

    List<TileData> open = new List<TileData>();
    List<TileData> closed = new List<TileData>();

    public Vector3Int playerPosition
    {
        get
        {
            return Vector3Int.RoundToInt(player.position);
        }
    }

    public Dictionary<Vector3Int, TileData> tileDataMap = new Dictionary<Vector3Int, TileData> ();

    void Start()
    {
        GenerateTileData();              
        List<Vector3Int> path = GetPath(Vector3Int.RoundToInt(player.position), Vector3Int.RoundToInt(enemy.position));
        for (var i = 1; i < path.Count; i++)
        {
            Debug.DrawLine(path[i - 1], path[i], Color.red, 10f);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            player.position = playerPosition;
            map.GetTile(playerPosition).name.Log();          
        }
    }

    void GenerateTileData()
    {
        tileDataMap = new Dictionary<Vector3Int, TileData>();
        foreach (var i in map.cellBounds.allPositionsWithin)
        {
            Vector3Int localPlace = new Vector3Int(i.x, i.y, i.z);

            if (!map.HasTile(localPlace))
            {
                continue;
            }

            TileBase tileBase = map.GetTile(i);
            bool isWalkable = tileBase.name != wallName;

            TileData data = new TileData(tileBase, localPlace, isWalkable);

            tileDataMap.Add(localPlace, data);

            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    if ((x == 0 && y == 0) || (Mathf.Abs(x) + Mathf.Abs(y) == 2))
                    {
                        continue;
                    }                    

                    if (data.isWalkable)
                    {
                        Debug.DrawLine((Vector3)localPlace, (Vector3)localPlace + new Vector3(x, y, 0) * 0.45f, Color.green, 10f);
                    }
                    else
                    {
                        Debug.DrawLine((Vector3)localPlace, (Vector3)localPlace + new Vector3(x, y, 0) * 0.45f, Color.red, 10f);
                    }
                }
            }
                    
        }
    }

    public List<Vector3Int> GetPath(Vector3Int positionStart, Vector3Int positionTarget)
    {        
        if (!tileDataMap.ContainsKey(positionStart))
        {
            Debug.LogError("No tile found with start position of " + positionStart.ToString());
            return null;
        }

        currentPathID++;

        // TODO check end node 

        open = new List<TileData>();
        closed = new List<TileData>();

        TileData currentNode = tileDataMap[positionStart];

        currentNode.gScore = 0;
        currentNode.fScore = GetHeuristicCost(currentNode.position,positionTarget);

        open.Add(currentNode);

        int lbkr = 50;
        while (open.Count > 0)
        {
            lbkr--;
            if (lbkr == 0)
            {
                break;
            }
            currentNode = GetLowestOpen();

            if (currentNode.position == positionTarget)
            {
                return ConstructPath(currentNode);
            }

            open.Remove(currentNode);
            closed.Add(currentNode);
            currentNode.isClosed = true;

            List<TileData> neghibors = GetNeghibors(currentNode.position);
            foreach (var i in neghibors)
            {
                if (i.isClosed)
                {
                    continue;
                }
                int tempGScore = currentNode.gScore + 1;

                if (!i.isOpen)
                {
                    i.isOpen = true;
                    open.Add(i);
                }
                else if (tempGScore > i.gScore)
                {
                    continue;
                }

                i.previousData = currentNode;
                i.gScore = tempGScore;
                i.fScore = tempGScore + GetHeuristicCost(i.position,positionTarget);
            }
        }

        Debug.LogError("Path Not Found");
        return null;
    }

    List<TileData> GetNeghibors(Vector3Int currentPosition)
    {
        List<TileData> retList = new List<TileData>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if ((x == 0 && y == 0) || (Mathf.Abs(x) + Mathf.Abs(y) == 2))
                {
                    continue;
                }



                if (tileDataMap.ContainsKey(currentPosition + new Vector3Int(x,y,0)))
                {
                    TileData data = tileDataMap[currentPosition + new Vector3Int(x, y, 0)];

                    if (!data.isWalkable)
                    {
                        continue;
                    }

                    if (data.pathID != currentPathID)
                    {
                        data.pathID = currentPathID;
                        data.isOpen = false;
                        data.isClosed = false;
                        data.gScore = int.MaxValue;
                        data.fScore = int.MaxValue;
                    }

                    retList.Add(data);
                }
            }
        }
        return retList;
    }

    

    int GetHeuristicCost(Vector3Int postionStart, Vector3Int positionTarget)
    {
        int dx = Mathf.Abs(postionStart.x - positionTarget.x);
        int dy = Mathf.Abs(postionStart.y - positionTarget.y);
        return 1 * (dx + dy);
    }

    public TileData GetLowestOpen()
    {
        TileData retData = null;
        int lowestF = int.MaxValue;
        foreach (var i in open)
        {
            if (i.fScore < lowestF)
            {
                retData = i;
                lowestF = i.fScore;
            }
        }
        return retData;
    }

    public List<Vector3Int> ConstructPath(TileData lastData)
    {
        List<Vector3Int> retList = new List<Vector3Int>();        

        TileData currentData = lastData;
        while (currentData.previousData != null)
        {
            retList.Add(currentData.position);
            currentData = currentData.previousData;
        }
        retList.Add(currentData.position);
        return retList;
    }

}
