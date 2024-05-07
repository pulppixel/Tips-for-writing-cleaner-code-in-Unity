using System;
using UnityEngine;


namespace Tips.Part_2_Result
{
    public class FallState : IState
    {
        private BasicCharacterControllerMover m_mover;
        private GroundedDetector m_groundedDetector;
        private AgentAnimations m_agentAnimations;
        private IAgentMovementInput m_input;

        private float m_moveSpeed = 2.0f;
        private float m_sprintSpeed = 5.335f;

        private float m_verticalVelocity;
        private float m_gravity = -15.0f;

        private float m_fallTimeoutDelta;
        private bool m_fallTransition = false;
        private float m_fallTimeout = 0.15f;
        

        public event Action<Type> OnTransition;
        public FallState(BasicCharacterControllerMover mover, GroundedDetector groundedDetector, AgentAnimations agentAnimations, IAgentMovementInput movementInput)
        {
            m_mover = mover;
            m_groundedDetector = groundedDetector;
            m_agentAnimations = agentAnimations;
            m_input = movementInput;
        }

        public void Enter()
        {
            m_agentAnimations.SetBool(AnimationBoolType.Grounded, false);
            m_agentAnimations.SetTrigger(AnimationTriggerType.Fall);

        }

        public void Exit()
        {
            m_agentAnimations.ResetTrigger(AnimationTriggerType.Fall);
            m_agentAnimations.SetBool(AnimationBoolType.Grounded, true);
        }

        public void Update(float deltaTime)
        {
            if (m_fallTransition == false && m_fallTimeoutDelta > 0)
            {
                m_fallTransition = true;
                m_fallTimeoutDelta = m_fallTimeout;
            }
            else
            {
                m_fallTimeoutDelta -= Time.deltaTime;
                if (m_fallTimeoutDelta <= 0)
                {
                    m_agentAnimations.SetTrigger(AnimationTriggerType.Fall);
                }
                m_fallTransition = false;
            }
            //Transition to Move state
            if (m_groundedDetector.Grounded == true)
            {
                OnTransition?.Invoke(typeof(LandState));
                return;
            }
            float targetMovementSpeed = m_input.SprintInput ? m_sprintSpeed : m_moveSpeed;
            targetMovementSpeed = m_input.MovementInput == Vector2.zero ? 0 : targetMovementSpeed;
            m_verticalVelocity += m_gravity * Time.deltaTime;
            m_mover.Move(
                new Vector3(m_input.MovementInput.x, m_verticalVelocity, m_input.MovementInput.y), targetMovementSpeed);

        }
    }
}