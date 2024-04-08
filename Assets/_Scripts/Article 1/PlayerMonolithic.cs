using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
using UnityEngine.InputSystem;

using PlayerInput = UnityEngine.InputSystem.PlayerInput;
using System;

namespace Tips.Part_1_Start
{
    public class PlayerMonolithic : MonoBehaviour
    {
        [Header("Movement Parameters")]
        public float MoveSpeed = 2.0f;
        public float SprintSpeed = 5.335f;
        public float RotationSmoothTime = 0.12f;
        public float SpeedChangeRate = 10.0f;
        public float FallTimeout = 0.15f;

        private CharacterController _controller;
        private PlayerInput _input;
        private float _speed;
        private float _targetRotation = 0.0f;
        private float _rotationVelocity;
        private float _verticalVelocity;
        private float _fallTimeoutDelta;

        [Header("Grounded Check")]
        public float Gravity = -15.0f;
        public bool Grounded = true, StairsGrounded = true;
        public float GroundedOffset = 0.21f, StairOffset = 0.07f;
        public float GroundedRadius = 0.28f;
        public LayerMask GroundLayers;

        [Header("Camera")]
        public GameObject CinemachineCameraTarget;
        public float TopCameraLimit = 70.0f;
        public float BottomCameraLimit = -30.0f;

        private GameObject _mainCamera;
        private float _cinemachineTargetYaw;
        private float _cinemachineTargetPitch;
        private const float _cameraRotationThreshold = 0.01f;

        [Header("Animations")]
        public string AnimationSpeedFloat;
        public string AnimationGroundedBool;
        public string AnimationFallTrigger;

        private Animator _animator;
        private float _animationMovementSpeed;

        //Input
        private Vector2 _movementInput, _lookInput;
        private bool _isSprintingInput;

        

        private void Awake()
        {
            // get a reference to our main camera
            if (_mainCamera == null)
            {
                _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
            }
            _animator = GetComponent<Animator>();
            _controller = GetComponent<CharacterController>();
            _input = GetComponent<PlayerInput>();
        }

        #region INPUT
        private void OnEnable()
        {
            _input.actions["Player/Move"].performed += OnMove;
            _input.actions["Player/Move"].canceled += OnMove;
            _input.actions["Player/Look"].performed += OnLook;
            _input.actions["Player/Look"].canceled += OnLook;
            _input.actions["Player/Sprint"].performed += OnSprint;
        }

        private void OnDisable()
        {
            _input.actions["Player/Move"].performed -= OnMove;
            _input.actions["Player/Move"].canceled -= OnMove;
            _input.actions["Player/Look"].performed -= OnLook;
            _input.actions["Player/Look"].canceled -= OnLook;
            _input.actions["Player/Sprint"].performed -= OnSprint;
        }

        private void OnLook(InputAction.CallbackContext context)
        {
            _lookInput = context.ReadValue<Vector2>();

        }

        private void OnMove(InputAction.CallbackContext context)
        {
            _movementInput = context.ReadValue<Vector2>();
        }

        private void OnSprint(InputAction.CallbackContext context)
        {
            _isSprintingInput = context.ReadValueAsButton();

        }

        #endregion

        private void Update()
        {
            if (Grounded == false)
            {
                _verticalVelocity += Gravity * Time.deltaTime;
                _fallTimeoutDelta -= Time.deltaTime;
                if (_fallTimeoutDelta <= 0 && StairsGrounded == false)
                {
                    _animator.SetTrigger(AnimationFallTrigger);
                }
            }
            else
            {
                _verticalVelocity = 0;
                _fallTimeoutDelta = FallTimeout;
                _animator.ResetTrigger(AnimationFallTrigger);
            }

            CharacterMovementCalculation();
            RotationCalculation();

            Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;

            //move the character controller
            _controller.Move(targetDirection.normalized * (_speed * Time.deltaTime) +
                             new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);

            //play animations
            _animator.SetFloat(AnimationSpeedFloat, _animationMovementSpeed);
        }

        private void RotationCalculation()
        {
            // normalise input direction
            Vector3 inputDirection = new Vector3(_movementInput.x, 0.0f, _movementInput.y).normalized;

            //Rotation Code
            if (_movementInput != Vector2.zero)
            {
                _targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg +
                                  _mainCamera.transform.eulerAngles.y;
                float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity,
                    RotationSmoothTime);

                // rotate to face input direction relative to camera position
                transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
            }
        }

        private void CharacterMovementCalculation()
        {
            float targetSpeed = _isSprintingInput ? SprintSpeed : MoveSpeed;


            if (_movementInput == Vector2.zero)
                targetSpeed = 0.0f;

            // a reference to the players current horizontal velocity
            float currentHorizontalSpeed = new Vector3(_controller.velocity.x, 0.0f, _controller.velocity.z).magnitude;

            float speedOffset = 0.1f;
            float inputMagnitude = _movementInput.magnitude;

            // accelerate or decelerate to target speed
            if (currentHorizontalSpeed < targetSpeed - speedOffset ||
                currentHorizontalSpeed > targetSpeed + speedOffset)
            {
                // creates curved result rather than a linear one giving a more organic speed change
                // note T in Lerp is clamped, so we don't need to clamp our speed
                _speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude,
                    Time.deltaTime * SpeedChangeRate);

                // round speed to 3 decimal places
                _speed = Mathf.Round(_speed * 1000f) / 1000f;
            }
            else
            {
                _speed = targetSpeed;
            }

            _animationMovementSpeed = Mathf.Lerp(_animationMovementSpeed, targetSpeed, Time.deltaTime * SpeedChangeRate);
            if (_animationMovementSpeed < 0.01f)
                _animationMovementSpeed = 0f;
        }

        private void LateUpdate()
        {
            CameraRotation();
        }

        private void CameraRotation()
        {
            // if there is an input and camera position is not fixed
            if (_lookInput.sqrMagnitude >= _cameraRotationThreshold )
            {
                //Don't multiply mouse input by Time.deltaTime;
                float deltaTimeMultiplier = 1.0f;

                _cinemachineTargetYaw += _lookInput.x * deltaTimeMultiplier;
                _cinemachineTargetPitch += _lookInput.y * deltaTimeMultiplier;
            }

            // clamp our rotations so our values are limited 360 degrees
            _cinemachineTargetYaw = ClampAngle(_cinemachineTargetYaw, float.MinValue, float.MaxValue);
            _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, BottomCameraLimit, TopCameraLimit);

            // Cinemachine will follow this target
            CinemachineCameraTarget.transform.rotation = Quaternion.Euler(_cinemachineTargetPitch,
                _cinemachineTargetYaw, 0.0f);
        }

        private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
        {
            if (lfAngle < -360f) lfAngle += 360f;
            if (lfAngle > 360f) lfAngle -= 360f;
            return Mathf.Clamp(lfAngle, lfMin, lfMax);
        }

        private void FixedUpdate()
        {
            Grounded = GroundedCheck(GroundedOffset);
            StairsGrounded = GroundedCheck(StairOffset);
            _animator.SetBool(AnimationGroundedBool, Grounded);
        }

        private bool GroundedCheck(float groundedOffset)
        {
            // set sphere position, with offset
            Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y + groundedOffset,
                transform.position.z);
            return Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers,
                QueryTriggerInteraction.Ignore);
        }

        private void OnDrawGizmosSelected()
        {
            Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
            Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

            if (Grounded) Gizmos.color = transparentGreen;
            else Gizmos.color = transparentRed;

            // when selected, draw a gizmo in the position of, and matching radius of, the grounded collider
            Gizmos.DrawSphere(
                new Vector3(transform.position.x, transform.position.y + GroundedOffset, transform.position.z),
                GroundedRadius);
        }
    }
}

