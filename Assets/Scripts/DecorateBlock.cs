using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class DecorateBlock : MonoBehaviour
{
    private int _gridSize = 10;


    void Update()
    {
        SnapToGrid();
    }

    private void SnapToGrid()
    {
        Vector3 _position;

        _position.x = Mathf.RoundToInt(transform.position.x / _gridSize) * _gridSize;
        _position.y = -10; //Mathf.RoundToInt(transform.position.y / _gridSize) * _gridSize;
        _position.z = Mathf.RoundToInt(transform.position.z / _gridSize) * _gridSize;

        transform.position = _position;
    }
}
