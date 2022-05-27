using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]

public class WayPoint : MonoBehaviour
{
    const int _gridSize = 10;

    public WayPoint exploredFrom;

    Vector2Int gridPos;

    public bool isExplored = false;
    public bool isPlaceable = true;



    public int GetGridSize()
    {
        return _gridSize;
    }


    public Vector2Int GetGridPos()
    {
        gridPos.x = Mathf.RoundToInt(transform.position.x / _gridSize);
        gridPos.y = Mathf.RoundToInt(transform.position.z / _gridSize);
        return new Vector2Int(gridPos.x, gridPos.y);
    }

    public void SetTopColor(Color color)
    {
        MeshRenderer topMeshRenderer = transform.Find("Top").GetComponent<MeshRenderer>();
        topMeshRenderer.material.color = color;
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
            {
            if (isPlaceable)
            {
                FindObjectOfType<TowersScript>().AddTower(this);
            }
            else
            {
                print("Нельзя строить");
            }
        }
    }
}
