using Codebase.Game.Root.Input;
using UnityEngine;

namespace Codebase.Game.Gameplay.Player
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Movement settings")]
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _jumpForce;
        [SerializeField] private LayerMask _groundLayers;
        
        [Header("Look settings")]
        [SerializeField] private Transform _playerCamera;
        [SerializeField] private float _rotationSpeed;

        
        private bool IsGrounded => Physics.Raycast(transform.position, Vector3.down,  0.2f, _groundLayers);
        private Vector3 ForwardDirection => _playerCamera.TransformDirection(Vector3.forward);
        private Vector3 RightDirection => _playerCamera.TransformDirection(Vector3.right);

        private CapsuleCollider _collider;
        private Rigidbody _rigidBody;
        private Animator _animator;
        private InputManager _inputManager;
        
        private Quaternion _targetRotation;
        private Vector2 _moveDirection;

        private void Start()
        {
            /*Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;*/
            _inputManager = new InputManager();
            _inputManager.OnJump += Jump;
            
            _rigidBody = GetComponent<Rigidbody>();
            _collider = GetComponent<CapsuleCollider>();
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            Move(_inputManager.Movement);
            
            transform.rotation = Quaternion.RotateTowards(transform.rotation, _targetRotation, _rotationSpeed * Time.deltaTime);
            
            _animator.SetFloat("Direction", _inputManager.Movement.magnitude);
            _animator.SetBool("IsGrounded", IsGrounded);
            print(IsGrounded);
            /*if(Input.GetKeyDown(KeyCode.Space))
                Jump();*/
        }

        private void Move(Vector2 direction)
        {
            _moveDirection = direction;
            
            if (_moveDirection.magnitude <= 0.1f) return;
            
            var velocityY = _rigidBody.velocity.y;
            var moveDirection = (ForwardDirection * _moveDirection.y + RightDirection * _moveDirection.x).normalized;
            
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
