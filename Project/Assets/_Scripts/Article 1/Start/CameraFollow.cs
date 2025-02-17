using System;
using UnityEngine;

namespace Tips.Part_1_Start
{
    /// <summary>
    /// Input에 따라 시네머신이 플레이어와 회전을 따라가는 로직 정의
    /// </summary>
    public class CameraFollow : MonoBehaviour
    {
        [Header("Camera")]
        public GameObject CinemachineCameraTarget;
        public float TopCameraLimit = 70.0f;
        public float BottomCameraLimit = -30.0f;

        private GameObject _mainCamera;
        private float _cinemachineTargetYaw;
        private float _cinemachineTargetPitch;
        private const float _cameraRotationThreshold = 0.01f;

        private PlayerGameInput _input;

        private void Awake()
        {
            // 메인 카메라의 참조 가져오기
            if (_mainCamera == null)
            {
                _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
            }

            _input = GetComponent<PlayerGameInput>();
        }

        private void LateUpdate()
        {
            CameraRotation();
        }

        private void CameraRotation()
        {
            // 입력이 있고, 카메라 포지션이 고정되어 있지 않을 때
            if (_input.CameraInput.sqrMagnitude >= _cameraRotationThreshold)
            {
                // Mouse Input에는 절대 Time.deltaTime;을 곱하면 안된다.
                float deltaTimeMultiplier = 1.0f;

                _cinemachineTargetYaw += _input.CameraInput.x * deltaTimeMultiplier;
                _cinemachineTargetPitch += _input.CameraInput.y * deltaTimeMultiplier;
            }
            
            // 값이 360도로 제한될 수 있도록, 회전을 고정한다. (Clamp out rotations)
            _cinemachineTargetYaw = ClampAngle(_cinemachineTargetYaw, float.MinValue, float.MaxValue);
            _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, BottomCameraLimit, TopCameraLimit);
            
            // Cinemachine은 Target을 따라다닐 것
            CinemachineCameraTarget.transform.rotation = Quaternion.Euler(_cinemachineTargetPitch, _cinemachineTargetYaw, 0.0f);
        }

        private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
        {
            if (lfAngle < -360.0f)
            {
                lfAngle += 360.0f;
            }

            if (lfAngle > 360.0f)
            {
                lfAngle -= 360.0f;
            }

            return Mathf.Clamp(lfAngle, lfMin, lfMax);
        }
    }
}










