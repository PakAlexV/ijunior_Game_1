using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFollower : MonoBehaviour
{
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private Transform _targetTransform;
    [SerializeField] private Vector3 _offcetCenter = new Vector3(0, 2, 0);

    private void FixedUpdate()
    {
        _cameraTransform.position = new Vector3(_targetTransform.position.x, _targetTransform.position.y, _cameraTransform.position.z) + _offcetCenter;
    }
}
