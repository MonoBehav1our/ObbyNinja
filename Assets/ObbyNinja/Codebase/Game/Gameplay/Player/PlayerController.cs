using UnityEngine;
using UnityEngine.Serialization;

namespace Codebase.Game.Gameplay.Player
{
    public class PlayerController : MonoBehaviour
    {
        [FormerlySerializedAs("_walkingSpeed"),Header("Movement settings")]
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _jumpForce;
        [SerializeField] private LayerMask _groundLayers;
        [SerializeField] private Transform _playerCamera;
        [SerializeField] private float _rotationSpeed;

        private CapsuleCollider _collider;
        private Rigidbody _rigidBody;
        
        private bool IsGrounded => Physics.Raycast(transform.position, Vector3.down, _collider.height / 2 + 0.1f, _groundLayers);
        private Vector3 ForwardDirection => _playerCamera.TransformDirection(Vector3.forward);
        private Vector3 RightDirection => _playerCamera.TransformDirection(Vector3.right);

        private Quaternion _targetRotation;

        private void Start()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            
            _rigidBody = GetComponent<Rigidbody>();
            _collider = GetComponent<CapsuleCollider>();
        }

        private void Update()
        {
            Move(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")));
            
            transform.rotation = Quaternion.RotateTowards(transform.rotation, _targetRotation, _rotationSpeed * Time.deltaTime);
            
            if(Input.GetKeyDown(KeyCode.Space))
                Jump();
        }

        private void Move(Vector2 direction)
        {
            if (direction.magnitude <= 0.1f) return;
            
            var velocityY = _rigidBody.velocity.y;
            var moveDirection = (ForwardDirection * direction.y + RightDirection * direction.x).normalized;
            
            var velocity = moveDirection * _moveSpeed;
            velocity.y = velocityY;
            _rigidBody.velocity = velocity;
            
            _targetRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0, moveDirection.z));
        }

        private void Jump()
        {
            if (IsGrounded) _rigidBody.velocity = new Vector3(_rigidBody.velocity.x, _jumpForce, _rigidBody.velocity.z);
        }
    }
}
