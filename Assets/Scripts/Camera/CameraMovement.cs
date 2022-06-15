using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("Speed")]
    public float _speed = 1;
    [Tooltip("Radius")]
    public float _radius = 10;
    [Tooltip("Target)")]
    public Transform _target;

    private UnityEngine.Touch _touch;
    private Vector3 _targetPos;

    private void Start()
    {
        if (_target == null)
        {
            _target = this.transform;
        }

        _targetPos = _target.position;
    }

    private void Update()
    {
        if (Input.touchCount == 1)
        {
            _touch = Input.GetTouch(0);

            if (_touch.phase == TouchPhase.Moved)
            {
                Vector3 movePos = new Vector3(
                    transform.position.x + _touch.deltaPosition.x * _speed * -1 * Time.deltaTime,
                    transform.position.y,
                    transform.position.z + _touch.deltaPosition.y * _speed * -1 * Time.deltaTime);

                Vector3 distance = movePos - _targetPos;

                if (distance.magnitude < _radius)
                    transform.position = movePos;
            }
            else
            {
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit raycastHit;

                if (Physics.Raycast(ray, out raycastHit))
                {
                    var rig = raycastHit.collider.GetComponent<WayPoint>();

                    if (rig != null && rig.isPlaceable)
                    {
                        FindObjectOfType<TowersScript>().AddTower(rig);
                    }


                }
            }
        }
    }
}
