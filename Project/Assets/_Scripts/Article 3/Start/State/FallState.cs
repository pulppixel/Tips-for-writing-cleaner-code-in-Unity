using UnityEngine;

namespace Tips.Part_3_Start
{
    public class FallState : State
    {
        private BasicCharacterControllerMover m_mover;
        private AgentAnimations m_agentAnimations;
        private IAgentMovementInput m_input;
        private AgentStats m_agentStates;

        private float m_verticalVelocity;
        private float m_fallTimeoutDelta;
        private bool m_fallTransition = false;

        MovementHelper m_movementHelper = new();

        public FallState(BasicCharacterControllerMover mover, AgentAnimations agentAnimations, IAgentMovementInput movementInput, AgentStats agentStats)
        {
            m_mover = mover;
            m_agentAnimations = agentAnimations;
            m_input = movementInput;
            m_agentStates = agentStats;
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
                m_fallTimeoutDelta = m_agentStates.FallTimeout;
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

            m_verticalVelocity += m_agentStates.Gravity * Time.deltaTime;

            //float targetMovementSpeed = m_input.SprintInput ? m_agentStates.SprintSpeed : m_agentStates.MoveSpeed;
            //targetMovementSpeed = m_input.MovementInput == Vector2.zero ? 0 : targetMovementSpeed;
            //m_mover.Move(
            //    new Vector3(m_input.MovementInput.x, m_verticalVelocity, m_input.MovementInput.y), targetMovementSpeed);
            m_movementHelper.PerformMovement(m_input, m_agentStates, m_mover, m_verticalVelocity);

        }
    }
}
