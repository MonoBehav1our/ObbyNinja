using System;
using UnityEngine;

namespace Codebase.Game.Root.Input
{
    public class InputManager
    {
        public Vector2 Movement => _currentInput.Movement;
        
        public event Action<Vector2> OnMouseDeltaChange;
        public event Action OnJump;

        private IInputHandler _currentInput;
        private InputMap _inputMap;
        
        public InputManager()
        {
            _inputMap = new InputMap();
            _inputMap.Enable();
            
            //InitializeStandaloneInput(_inputMap);
            InitializeMobileInput(_inputMap);
        }

        private void InitializeStandaloneInput(InputMap inputMap)
        {
            _currentInput = new StandaloneInputHandler(inputMap);
            
            _currentInput.OnMouseDeltaChange += MouseDeltaChangeHandle;
            _currentInput.OnJump += JumpEventHandle;
        }

        private void InitializeMobileInput(InputMap inputMap)
        {
            _currentInput = new MobileInputHandler(inputMap);
            
            _currentInput.OnMouseDeltaChange += MouseDeltaChangeHandle;
            _currentInput.OnJump += JumpEventHandle;
        }

        private void MouseDeltaChangeHandle(Vector2 value) => 
            OnMouseDeltaChange?.Invoke(value);

        private void JumpEventHandle() => 
            OnJump?.Invoke();
    }
}
