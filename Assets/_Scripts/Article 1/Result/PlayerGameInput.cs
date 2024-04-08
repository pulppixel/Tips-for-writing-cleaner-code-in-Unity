using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

namespace Tips.Part_1_Result
{
    public class PlayerGameInput : MonoBehaviour, IAgentMovementInput
    {
        private PlayerInput _input;

        public Vector2 MovementInput { get; private set; }
        public Vector2 CameraInput { get; private set; }
        public bool SprintInput { get; private set; }

        private void Awake()
        {
            _input = GetComponent<PlayerInput>();
        }
        private void OnEnable()
        {
            _input.actions["Player/Move"].performed += OnMove;
            _input.actions["Player/Move"].canceled += OnMove;
            _input.actions["Player/Look"].performed += OnLook;
            _input.actions["Player/Look"].canceled += OnLook;
            _input.actions["Player/Sprint"].performed += OnInput;
        }

        private void OnDisable()
        {
            _input.actions["Player/Move"].performed -= OnMove;
            _input.actions["Player/Move"].canceled -= OnMove;
            _input.actions["Player/Look"].performed -= OnLook;
            _input.actions["Player/Look"].canceled -= OnLook;
            _input.actions["Player/Sprint"].performed -= OnInput;
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
