using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    [SerializeField] private WayPoint startPoint, endPoint;

    Dictionary<Vector2Int, WayPoint> grid = new Dictionary<Vector2Int, WayPoint>();

    Queue<WayPoint> queue = new Queue<WayPoint>();

    WayPoint searchPoint;

    List<WayPoint> path = new List<WayPoint>();

    bool isRunning = true;


    Vector2Int[] directions =
    {
        Vector2Int.up,
        Vector2Int.right,
        Vector2Int.down,
        Vector2Int.left
    };



    public List<WayPoint> GetPath()
    {
        if (path.Count > 0)
        {
            return path;
        }
        else
        {
            LoadBlocks();
            SetColorStartEnd();
            Pathfind();
            CreatePath();
            return path;
        }
    }

    private void CreatePath()
    {
        AddPointPath(endPoint);

        WayPoint prevPoint = endPoint.exploredFrom;

        while (prevPoint != startPoint)
        {
            prevPoint.SetTopColor(Color.green);
            AddPointPath(prevPoint);
            prevPoint = prevPoint.exploredFrom;
        }

        AddPointPath(startPoint);
        path.Reverse();
    }

    private void AddPointPath(WayPoint wayPoint)
    {
        path.Add(wayPoint);
        wayPoint.isPlaceable = false;
    }


    private void Pathfind()
    {
        queue.Enqueue(startPoint);

        while (queue.Count > 0 && isRunning == true)
        {
            searchPoint = queue.Dequeue();
            searchPoint.isExplored = true;
            CheckForEndpoint();
            ExploreNearPoints();
        }
    }


    private void CheckForEndpoint()
    {
        if(searchPoint == endPoint)
        {
            print("Нашли свой конец");
            searchPoint.SetTopColor(Color.white);
            isRunning = false;
        }
    }

    private void ExploreNearPoints()
    {
        if (isRunning == true)
        {
            foreach (Vector2Int direction in directions)
            {
                Vector2Int nearPointCoordinates = searchPoint.GetGridPos() + direction;

                try
                {
                    WayPoint nearPoint = grid[nearPointCoordinates];

                    if (nearPoint.isExplored || queue.Contains(nearPoint)) 
                    {
                        return;
                    }
                    else
                    {
                        nearPoint.exploredFrom = searchPoint;
                        nearPoint.SetTopColor(Color.cyan);
                        queue.Enqueue(nearPoint);
                        
                    }
                }
                catch
                {
                   
                }
            }
        }
    }




    private void SetColorStartEnd()
    {
        startPoint.SetTopColor(Color.green);
        endPoint.SetTopColor(Color.red);
    }

    private void LoadBlocks()
    {
        var waypoints = FindObjectsOfType<WayPoint>();

        foreach(WayPoint waypoint in waypoints)
        {
            Vector2Int gridPos = waypoint.GetGridPos();
            
            if (grid.ContainsKey(gridPos))
            {
                Debug.LogWarning("povtor " + waypoint);
            }
            else
            {
                grid.Add(gridPos, waypoint);
                
            }
        }
        print("Блоков добавилось " + grid.Count);
    }
}
