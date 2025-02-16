using UnityEngine;
using UnityEngine.InputSystem;

using PlayerInput = UnityEngine.InputSystem.PlayerInput;

namespace Tips.Part_1_Start
{
    /// <summary>
    /// Monolithic script that defines a character controller for a player character.
    /// </summary>
    public class PlayerMonolithic : MonoBehaviour
    {
        [Header("Movement Parameters")]
        public float MoveSpeed = 2.0f;
        public float SprintSpeed = 5.335f;
        public float RotationSmoothTime = 0.12f;
        public float SpeedChangeRate = 10.0f;
        public float FallTimeout = 0.15f;

        private CharacterController m_controller;
        private float m_speed;
        private float m_targetRotation = 0.0f;
        private float m_rotationVelocity;
        private float m_verticalVelocity;
        private float m_fallTimeoutDelta;

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

        private GameObject m_mainCamera;
        private float m_cinemachineTargetYaw;
        private float m_cinemachineTargetPitch;
        private const float m_cameraRotationThreshold = 0.01f;

        [Header("Animations")]
        public string AnimationSpeedFloat;
        public string AnimationGroundedBool;
        public string AnimationFallTrigger;

        private Animator m_animator;
        private float m_animationMovementSpeed;

        //Input
        private PlayerGameInput m_input;
        
        // 유니티 참조 얻기..
        private void Awake()
        {
            // Main Camera에 대한 참조 얻기
            if (m_mainCamera == null)
            {
                m_mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
            }
            m_animator = GetComponent<Animator>();
            m_controller = GetComponent<CharacterController>();
            m_input = GetComponent<PlayerGameInput>();
        }

        // 이동과 관련된 모든 로직은 업데이트에서 관리
        private void Update()
        {
            // 중력 및 낙하 애니메이션 제어
            if (Grounded == false)
            {
                m_verticalVelocity += Gravity * Time.deltaTime;
                m_fallTimeoutDelta -= Time.deltaTime;
                if (m_fallTimeoutDelta <= 0 && StairsGrounded == false)
                {
                    m_animator.SetTrigger(AnimationFallTrigger);
                }
            }
            else
            {
                m_verticalVelocity = 0;
                m_fallTimeoutDelta = FallTimeout;
                m_animator.ResetTrigger(AnimationFallTrigger);
            }

            CharacterMovementCalculation();
            RotationCalculation();

            Vector3 targetDirection = Quaternion.Euler(0.0f, m_targetRotation, 0.0f) * Vector3.forward;

            // 캐릭터 컨트롤러의 Move
            m_controller.Move(targetDirection.normalized * (m_speed * Time.deltaTime) + new Vector3(0.0f, m_verticalVelocity, 0.0f) * Time.deltaTime);

            // 애니메이션 재생
            m_animator.SetFloat(AnimationSpeedFloat, m_animationMovementSpeed);
        }

        // MainCamera와 PlayerInput으로 Avatar rotation base don 계산
        private void RotationCalculation()
        {
            // 입력 방향 정규화 (Normalize)
            Vector3 inputDirection = new Vector3(m_input.MovementInput.x, 0.0f, m_input.MovementInput.y).normalized;

            // 회전 담당 코드
            if (m_input.MovementInput != Vector2.zero)
            {
                m_targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + m_mainCamera.transform.eulerAngles.y;
                float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, m_targetRotation, ref m_rotationVelocity, RotationSmoothTime);

                // 카메라 위치를 기준으로, Input 방향을 향하여 회전한다. 
                transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
            }
        }

        // 이동 속도 계산 및 "움직임" 애니메이션 제어
        private void CharacterMovementCalculation()
        {
            float targetSpeed = m_input.SprintInput ? SprintSpeed : MoveSpeed;


            if (m_input.MovementInput == Vector2.zero)
            {
                targetSpeed = 0.0f;
            }

            // 플레이어의 현재 Horizontal Velocity에 대한 참조
            float currentHorizontalSpeed = new Vector3(m_controller.velocity.x, 0.0f, m_controller.velocity.z).magnitude;

            float speedOffset = 0.1f;
            float inputMagnitude = m_input.MovementInput.magnitude;

            // 목표 속도로 가속 or 감속
            if (currentHorizontalSpeed < targetSpeed - speedOffset ||
                currentHorizontalSpeed > targetSpeed + speedOffset)
            {
                // creates curved result rather than a linear one giving a more organic speed change
                // note T in Lerp is clamped, so we don't need to clamp our speed
                m_speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude, Time.deltaTime * SpeedChangeRate);

                // round speed to 3 decimal places
                m_speed = Mathf.Round(m_speed * 1000f) / 1000f;
            }
            else
            {
                m_speed = targetSpeed;
            }

            m_animationMovementSpeed = Mathf.Lerp(m_animationMovementSpeed, targetSpeed, Time.deltaTime * SpeedChangeRate);
            if (m_animationMovementSpeed < 0.01f)
            {
                m_animationMovementSpeed = 0f;
            }
        }

        // 일관된 결과를 위한.. CameraMovement (LateUpdate)
        private void LateUpdate()
        {
            CameraRotation();
        }

        private void CameraRotation()
        {
            // Input이 있고, 카메라 위치가 고정되어 있지 않은 경우
            if (m_input.CameraInput.sqrMagnitude >= m_cameraRotationThreshold)
            {
                // 마우스 입력에는 Time.deltaTime; 을 곱하면 안된다.
                float deltaTimeMultiplier = 1.0f;

                m_cinemachineTargetYaw += m_input.CameraInput.x * deltaTimeMultiplier;
                m_cinemachineTargetPitch += m_input.CameraInput.y * deltaTimeMultiplier;
            }

            // 값이 360도로 제한되도록 회전값 고정
            m_cinemachineTargetYaw = ClampAngle(m_cinemachineTargetYaw, float.MinValue, float.MaxValue);
            m_cinemachineTargetPitch = ClampAngle(m_cinemachineTargetPitch, BottomCameraLimit, TopCameraLimit);

            // 시네머신은 이 Target을 따른다.
            CinemachineCameraTarget.transform.rotation = Quaternion.Euler(m_cinemachineTargetPitch, m_cinemachineTargetYaw, 0.0f);
        }

        private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
        {
            if (lfAngle < -360f) lfAngle += 360f;
            if (lfAngle > 360f) lfAngle -= 360f;
            return Mathf.Clamp(lfAngle, lfMin, lfMax);
        }

        // FALL과 MOVEMENT 애니메이션, Behavior 사이클을 전환할 수 있는 Grounded Check
        private void FixedUpdate()
        {
            Grounded = GroundedCheck(GroundedOffset);
            StairsGrounded = GroundedCheck(StairOffset);
            m_animator.SetBool(AnimationGroundedBool, Grounded);
        }

        // 아래쪽으로 Sphere Casting하여 Ground에 닿았는지 확인
        private bool GroundedCheck(float groundedOffset)
        {
            // Sphere 위치 설정, Offset 포함
            Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y + groundedOffset, transform.position.z);
            return Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers, QueryTriggerInteraction.Ignore);
        }

        // Ground Check의 시각화
        private void OnDrawGizmosSelected()
        {
            Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
            Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

            if (Grounded) Gizmos.color = transparentGreen;
            else Gizmos.color = transparentRed;

            // when selected, draw a gizmo in the position of, and matching radius of, the grounded collider
            Gizmos.DrawSphere(new Vector3(transform.position.x, transform.position.y + GroundedOffset, transform.position.z), GroundedRadius);
        }
    }
}

