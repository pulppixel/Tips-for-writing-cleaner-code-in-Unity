using System;
using UnityEngine;

namespace Tips.Partm_1m_Start
{
    public class CameraFollow : MonoBehaviour
    {
        [Header("Camera")]
        public GameObject CinemachineCameraTarget;
        public float TopCameraLimit = 70.0f;
        public float BottomCameraLimit = -30.0f;
        
        private GameObject m_mainCamera;
        private float m_cinemachineTargetYaw;
        private float m_cinemachineTargetPitch;
        private const float m_cameraRotationThreshold = 0.01f;

        private PlayerGameInput m_input;
        
        private void Awake()
        {
            // get a reference to our main camera
            if (m_mainCamera == null)
            {
                m_mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
            }
            
            m_input = GetComponent<PlayerGameInput>();
        }

        private void LateUpdate()
        {
        }

        private void CameraRotation()
        {
            // Input이 있고, 카메라 위치가 고정되어 있지 않다면
            if (m_input.)
        }
    }
}










