using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Codebase.Game.Root.Input
{
    public class StandaloneInputHandler : IInputHandler
    {
        public Vector2 Movement => _inputMap.Standalone.Movement.ReadValue<Vector2>();
        
        public event Action<Vector2> OnMouseDeltaChange;
        public event Action OnJump;

        private readonly InputMap _inputMap;
        
        public StandaloneInputHandler(InputMap inputMap)
        {
            _inputMap = inputMap;
            
            inputMap.Standalone.MouseDelta.performed += InvokeOnMouseDeltaChange;
            inputMap.Standalone.Jump.started += InvokeJumpEvent;
        }
        
        private void InvokeOnMouseDeltaChange(InputAction.CallbackContext context) => 
            OnMouseDeltaChange?.Invoke(context.ReadValue<Vector2>());

        private void InvokeJumpEvent(InputAction.CallbackContext context) => 
            OnJump?.Invoke();
    }
}
