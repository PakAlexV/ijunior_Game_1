using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTargetPlayer : MonoBehaviour
{
    [SerializeField] Transform _cameraTransform;
    [SerializeField] Transform _playerTransform;
    private int _offcetCenter = 2;

    private void FixedUpdate()
    {
        _cameraTransform.position = new Vector3(_playerTransform.position.x, _playerTransform.position.y + _offcetCenter, _cameraTransform.position.z);
    }
}
