using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Tips.Partm_1m_Start
{
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

        //Connecting to the InputSystem
        private void OnEnable()
        {
            m_input.actions["Player/Move"].performed += OnMove;
            m_input.actions["Player/Move"].canceled += OnMove;
            m_input.actions["Player/Look"].performed += OnLook;
            m_input.actions["Player/Look"].canceled += OnLook;
            m_input.actions["Player/Sprint"].performed += OnSprint;
        }
        
        //Disconnecting from the InputSystem
        private void OnDisable()
        {
            m_input.actions["Player/Move"].performed -= OnMove;
            m_input.actions["Player/Move"].canceled -= OnMove;
            m_input.actions["Player/Look"].performed -= OnLook;
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