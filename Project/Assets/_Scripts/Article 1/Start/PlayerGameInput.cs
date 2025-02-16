using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Tips.Part_1_Start
{
    /// <summary>
    /// PlayerInput 처리용 로직..
    /// 이는 String Paths가 아닌, Input Action Map으로도 처리가 가능하다.
    /// </summary>
    public class PlayerGameInput : MonoBehaviour
    {
        private PlayerInput m_input;
        
        public Vector2 MovementInput { get; private set; }
        public Vector2 CameraInput { get; private set; }
        public bool SprintInput { get; private set; }

        private void Awake()
        {
            m_input = GetComponent<PlayerInput>();
        }

        // InputSystem 연결
        private void OnEnable()
        {
            m_input.actions["Player/Move"].performed += OnMove;
            m_input.actions["Player/Move"].canceled += OnMove;
            m_input.actions["Player/Look"].performed += OnLook;
            m_input.actions["Player/Look"].canceled += OnLook;
            m_input.actions["Player/Sprint"].performed += OnSprint;
        }

        // InputSystem 연결 해제
        private void OnDisable()
        {
            m_input.actions["Player/Move"].performed -= OnMove;
            m_input.actions["Player/Move"].canceled -= OnMove;
            m_input.actions["Player/Look"].performed -= OnSprint;
            m_input.actions["Player/Look"].canceled -= OnLook;
            m_input.actions["Player/Sprint"].performed -= OnSprint;
        }

        private void OnLook(InputAction.CallbackContext context)
        {
            CameraInput = context.ReadValue<Vector2>();
        }

        private void OnMove(InputAction.CallbackContext context)
        {
            MovementInput = context.ReadValue<Vector2>();
        }

        private void OnSprint(InputAction.CallbackContext context)
        {
            SprintInput = context.ReadValueAsButton();
        }
    }
}