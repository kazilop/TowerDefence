using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(WayPoint))]
public class CubeEditor : MonoBehaviour
{
    
    TextMesh _label;
    string _labelName;
   
    WayPoint _wayPoint;

    private void Awake()
    {
        _wayPoint = GetComponent<WayPoint>();
    }

    private void Start()
    {
        _label = GetComponentInChildren<TextMesh>();
    }
    void Update()
    {

        SnapToGrid();

        UpdeteLabel();

    }

    private void SnapToGrid()
    {
        int _gridSize = _wayPoint.GetGridSize();
        transform.position = new Vector3(_wayPoint.GetGridPos().x * _gridSize, 0f, _wayPoint.GetGridPos().y * _gridSize);
    }
    private void UpdeteLabel()
    {
        int _gridSize = _wayPoint.GetGridSize();
        _labelName = _wayPoint.GetGridPos().x  + ":" + _wayPoint.GetGridPos().y ;
        _label.text = _labelName;
        gameObject.name = _labelName;
    }
}
