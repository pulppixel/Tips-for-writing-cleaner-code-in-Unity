
using System;
using UnityEngine;

namespace Tips.Part_2_Result
{
    public class MovementState : IState
    {
        private BasicCharacterControllerMover m_mover;
        private GroundedDetector m_groundedDetector;
        private AgentAnimations m_agentAnimations;
        private IAgentMovementInput m_input;
        private IAgentJumpInput m_jumpInput;

        private float m_moveSpeed = 2.0f;
        private float m_sprintSpeed = 5.335f;
        private float m_speedChangeRate = 10.0f;
        private float m_verticalVelocity;
        private float m_gravity = -15.0f;

        private float m_animationMovementSpeed;
        public float m_jumpTimeout = 0.20f;

        public event Action<Type> OnTransition;

        public MovementState(BasicCharacterControllerMover mover, GroundedDetector groundedDetector, AgentAnimations agentAnimations, IAgentMovementInput movementInput, IAgentJumpInput jumpInput)
        {
            m_mover = mover;
            m_groundedDetector = groundedDetector;
            m_agentAnimations = agentAnimations;
            m_input = movementInput;
            m_jumpInput = jumpInput;
        }

        public void Enter()
        {
            m_agentAnimations.SetBool(AnimationBoolType.Grounded, m_groundedDetector.Grounded);
            if (m_jumpInput != null)
                m_jumpInput.OnJumpInput += HandleJump;
        }

        public void Exit()
        {
            if (m_jumpInput != null)
                m_jumpInput.OnJumpInput -= HandleJump;
            m_agentAnimations.SetFloat(AnimationFloatType.Speed, 0);
        }

        private void HandleJump()
        {
            if(m_jumpTimeout <= 0)
                OnTransition(typeof(JumpState));
        }

        public void Update(float deltaTime)
        {
            //Transition to Fall state
            if (m_groundedDetector.Grounded == false && m_groundedDetector.StairsGrounded == false)
            {
                OnTransition?.Invoke(typeof(FallState));
                return;
            }
            if (m_groundedDetector.Grounded == false)
            {
                m_verticalVelocity += m_gravity * Time.deltaTime;
            }
            else
            {
                m_verticalVelocity = 0;
            }

            if (m_jumpTimeout >= 0.0f)
            {
                m_jumpTimeout -= Time.deltaTime;
            }


            float targetMovementSpeed = m_input.SprintInput ? m_sprintSpeed : m_moveSpeed;
            m_mover.Move(new Vector3(m_input.MovementInput.x, m_verticalVelocity, m_input.MovementInput.y), targetMovementSpeed);

            targetMovementSpeed = m_input.MovementInput == Vector2.zero ? 0 : targetMovementSpeed;
            m_animationMovementSpeed = Mathf.Lerp(m_animationMovementSpeed, targetMovementSpeed, Time.deltaTime * m_speedChangeRate);
            if (m_animationMovementSpeed < 0.01f)
                m_animationMovementSpeed = 0f;

            //play animations
            m_agentAnimations.SetFloat(AnimationFloatType.Speed, m_animationMovementSpeed);
            
        }
    }
}

