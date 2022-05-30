using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZooming : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("�������� ��������������� ������ ������")]
    public float _speed = 1;
    [Tooltip("��������� ������ ��������������� ������")]
    public float _radius = 5;
    [Tooltip("����� �� ������� ��������� ��������� ������ ����������� ������ (���� ��� �� ������ � ����������, �� �� ��������� �� ������������ ��� ���������, �� ������� ��������� ��������)")]
    public Transform _target;

    private UnityEngine.Touch _touchStart;
    private UnityEngine.Touch _touchEnd;
    private Vector3 _targetPos;

    private void Start()
    {
        if (_target == null)
        {
            _target = transform;
        }

        _targetPos = _target.position;
    }

    void Update()
    {
        if (Input.touchCount == 2)
        {
            _touchStart = Input.GetTouch(0);
            _touchEnd = Input.GetTouch(1);

            Vector2 touchStartDeltaPos = _touchStart.position - _touchStart.deltaPosition;
            Vector2 touchEndDeltaPos = _touchEnd.position - _touchEnd.deltaPosition;

            float distDeltaTouches = (touchStartDeltaPos - touchEndDeltaPos).magnitude;
            float currentDistTouchesPos = (_touchStart.position - _touchEnd.position).magnitude;

            float distance = distDeltaTouches - currentDistTouchesPos;

            Zooming(distance);

            var _touchBDirection = _touchEnd.position - _touchEnd.deltaPosition;
            var _touchADirection = _touchStart.position - _touchStart.deltaPosition;

         /*   if (_touchBDirection != _touchEnd.position)   //�������� ������ ��������
            {
                var angle = Vector3.SignedAngle(_touchEnd.position - _touchStart.position,
                    _touchBDirection - _touchADirection, -transform.forward);

                transform.RotateAround(transform.position, -transform.up, angle);

            }  */
        }
    }

    private void Zooming(float value)
    {
        float height = this.transform.position.y + (value * _speed * Time.deltaTime);
        float delta = Mathf.Abs(height - _targetPos.y);

        if (delta <= _radius)
            this.transform.position = new Vector3(this.transform.position.x, height, this.transform.position.z);
    }
}
