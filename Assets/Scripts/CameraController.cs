using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform _cameraTransform = default;
    [SerializeField] Transform _playerTransform = default;

    private void FixedUpdate()
    {
        _cameraTransform.position = new Vector3(_playerTransform.position.x, _playerTransform.position.y + 2, -10);
    }
}
