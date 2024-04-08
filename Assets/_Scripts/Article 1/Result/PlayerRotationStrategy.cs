using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

    public class PlayerRotationStrategy : AgentRoatationStrategy
    {
        [SerializeField]
        private GameObject _mainCamera;
        private void Awake()
        {
            // get a reference to our main camera
            if (_mainCamera == null)
            {
                _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
            }
        }

        protected override float RotationStrategy(Vector3 inputDirection)
            => Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + _mainCamera.transform.eulerAngles.y;
    }
