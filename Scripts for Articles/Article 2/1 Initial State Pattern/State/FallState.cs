using UnityEngine;

namespace Tips.Part_2_End
{
    /// <summary>
    /// Fall state triggers the Fall animation and applies gravity force to the character until we are again grounded.
    /// </summary>
    public class FallState : State
    {
        private BasicCharacterControllerMover m_mover;
        private AgentAnimations m_agentAnimations;
        private IAgentMovementInput m_input;

        private float m_moveSpeed = 2.0f;
        private float m_sprintSpeed = 5.335f;

        private float m_verticalVelocity;
        private float m_gravity = -15.0f;

        private float m_fallTimeoutDelta;
        private bool m_fallTransition = false;
        private float m_fallTimeout = 0.15f;

        public FallState(BasicCharacterControllerMover mover, AgentAnimations agentAnimations, IAgentMovementInput movementInput)
        {
            m_mover = mover;
            m_agentAnimations = agentAnimations;
            m_input = movementInput;
        }

        public override void Enter()
        {
            m_agentAnimations.SetBool(AnimationBoolType.Grounded, false);
            m_agentAnimations.SetTrigger(AnimationTriggerType.Fall);

        }

        public override void Exit()
        {
            m_agentAnimations.ResetTrigger(AnimationTriggerType.Fall);
            m_agentAnimations.SetBool(AnimationBoolType.Grounded, true);
        }

        protected override void StateUpdate(float deltaTime)
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

            float targetMovementSpeed = m_input.SprintInput ? m_sprintSpeed : m_moveSpeed;
            targetMovementSpeed = m_input.MovementInput == Vector2.zero ? 0 : targetMovementSpeed;
            m_verticalVelocity += m_gravity * Time.deltaTime;
            m_mover.Move(
                new Vector3(m_input.MovementInput.x, m_verticalVelocity, m_input.MovementInput.y), targetMovementSpeed);

        }
    }
}
