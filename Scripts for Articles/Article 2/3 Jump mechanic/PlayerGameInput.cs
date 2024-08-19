using UnityEngine;
using UnityEngine.InputSystem;

namespace Tips.Part_2_End
{
    /// <summary>
    /// This script contains the logic to handle PlayerInput. I use string paths to connect to InputSystem but you could
    /// also generate a script based on the Input Action Map. To learn more visit https://docs.unity3d.com/Packages/com.unity.inputsystem@1.8/manual/Concepts.html
    /// </summary>
    public class PlayerGameInput : MonoBehaviour, IAgentMovementInput, IAgentJumpInput
    {
        private PlayerInput m_input;

        public bool JumpInput { get; private set; }
        public Vector2 MovementInput { get; private set; }
        public Vector2 CameraInput { get; private set; }
        public bool SprintInput { get; private set; }

        private void Awake()
        {
            m_input = GetComponent<PlayerInput>();
        }
        private void OnEnable()
        {
            m_input.actions["Player/Jump"].performed += OnJump;
            m_input.actions["Player/Jump"].canceled += OnJump;
            m_input.actions["Player/Move"].performed += OnMove;
            m_input.actions["Player/Move"].canceled += OnMove;
            m_input.actions["Player/Look"].performed += OnLook;
            m_input.actions["Player/Look"].canceled += OnLook;
            m_input.actions["Player/Sprint"].performed += OnInput;
        }

        private void OnDisable()
        {
            m_input.actions["Player/Jump"].performed -= OnJump;
            m_input.actions["Player/Jump"].canceled -= OnJump;
            m_input.actions["Player/Move"].performed -= OnMove;
            m_input.actions["Player/Move"].canceled -= OnMove;
            m_input.actions["Player/Look"].performed -= OnLook;
            m_input.actions["Player/Look"].canceled -= OnLook;
            m_input.actions["Player/Sprint"].performed -= OnInput;
        }

        private void OnJump(InputAction.CallbackContext context)
        {
            JumpInput = context.ReadValueAsButton();
        }
        private void OnLook(InputAction.CallbackContext context)
        {
            CameraInput = context.ReadValue<Vector2>();
        }

        private void OnMove(InputAction.CallbackContext context)
        {
            MovementInput = context.ReadValue<Vector2>();
        }

        private void OnInput(InputAction.CallbackContext context)
        {
            SprintInput = context.ReadValueAsButton();
        }
    }
}
