using System;
using UnityEngine;

namespace Codebase.Game.Gameplay.Player
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform _follow;
        
        [SerializeField] private float _cameraSensivity;
        [SerializeField] private float _lookXLimit;

        private float _rotationX;
        private float _rotationY;
        private Vector3 _followOffset;

        private void Start()
        {
            _followOffset = transform.position - _follow.position;
        }

        private void LateUpdate()
        {
            Follow();
            RotateVision(new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")));
        }
        
        private void Follow()
        {
            transform.position = _follow.position + _followOffset;
        }

        private void RotateVision(Vector2 direction)
        {
            _rotationX += -direction.y * _cameraSensivity * Time.deltaTime;
            _rotationX = Mathf.Clamp(_rotationX, -_lookXLimit, _lookXLimit);
            _rotationY += direction.x * _cameraSensivity * Time.deltaTime;
            
            transform.rotation = Quaternion.Euler(_rotationX, _rotationY, 0);
        }
    }
}
