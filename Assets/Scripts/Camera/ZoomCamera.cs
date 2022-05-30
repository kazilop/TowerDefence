using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomCamera : MonoBehaviour
{

    [SerializeField] private float speed = 0.04F;
    Vector3 startPosition;

    public float zoomMax;
    public float zoomMin;
    public float zoomSpeed;

    private Camera _m_Camera;

    private UnityEngine.Touch _touchA;
    private UnityEngine.Touch _touchB;

    private Vector2 _touchADirection;
    private Vector2 _touchBDirection;

    private float _dstBtwTouchesPosition;
    private float _dstBtwTouchesDirection;
    private float _zoom;

    private void Start()
    {
        startPosition = transform.position;
        _m_Camera = Camera.main;
    }
    void Update()
    {
        if(Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;

            transform.Translate(-touchDeltaPosition.x * speed, -touchDeltaPosition.y * speed, 0);
                   
        }
        else if(Input.touchCount == 2)
        {
            _touchA = Input.GetTouch(0);
            _touchB = Input.GetTouch(1);

            _touchADirection = _touchA.position - _touchA.deltaPosition;
            _touchBDirection = _touchB.position - _touchB.deltaPosition;

            _dstBtwTouchesPosition = Vector2.Distance(_touchA.position, _touchB.position);
            _dstBtwTouchesDirection = Vector2.Distance(_touchADirection, _touchBDirection);

            _zoom = _dstBtwTouchesPosition - _dstBtwTouchesDirection;

            var currentZoom = _m_Camera.orthographicSize - _zoom * zoomSpeed;
            _m_Camera.orthographicSize = Mathf.Clamp(currentZoom, zoomMin, zoomMax);

            if(_touchBDirection != _touchB.position)
            {
                var angle = Vector3.SignedAngle(_touchB.position - _touchA.position,
                    _touchBDirection - _touchADirection, - _m_Camera.transform.forward);

                _m_Camera.transform.RotateAround(_m_Camera.transform.position, -_m_Camera.transform.forward, angle);

            }

        }
    }
}
