using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Codebase.Game.Root.Input
{
    public class MobileInputHandler : IInputHandler
    {
        public Vector2 Movement => _inputMap.Mobile.Movement.ReadValue<Vector2>();
        
        public event Action<Vector2> OnMouseDeltaChange;
        public event Action OnJump;

        private readonly InputMap _inputMap;
        
        public MobileInputHandler(InputMap inputMap)
        {
            _inputMap = inputMap;
            
            inputMap.Mobile.Jump.started += InvokeJumpEvent;
            
            inputMap.Mobile.TouchInZone.started += StartLookChange;
            inputMap.Mobile.TouchInZone.canceled += StopLookChange;
        }

        private void StartLookChange(InputAction.CallbackContext context)
        {
            _inputMap.Mobile.TouchDelta.performed += OnLookChange;
            Debug.Log("FSDFSDF");
        }

        private void StopLookChange(InputAction.CallbackContext context) => 
            _inputMap.Mobile.TouchDelta.performed -= OnLookChange;

        private void OnLookChange(InputAction.CallbackContext context) => 
            OnMouseDeltaChange?.Invoke(context.ReadValue<Vector2>());

        private void InvokeJumpEvent(InputAction.CallbackContext obj) => 
            OnJump?.Invoke();
    }
}
