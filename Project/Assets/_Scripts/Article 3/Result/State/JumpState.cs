using UnityEngine;

namespace Tips.Part_3_End
{
    public class JumpState : State
    {
        private AgentAnimations m_agentAnimations;
        private IAgentMovementInput m_input;
        private BasicCharacterControllerMover m_mover;
        private AgentStats m_agentStats;

        private float m_verticalVelocity;

        MovementHelper m_movementHelper = new();

        public JumpState(BasicCharacterControllerMover mover, AgentAnimations agentAnimations, IAgentMovementInput input, AgentStats agentStats)
        {
            m_agentAnimations = agentAnimations;
            m_input = input;
            m_mover = mover;
            m_agentStats = agentStats;
        }

        public override void Enter()
        {
            m_verticalVelocity = Mathf.Sqrt(m_agentStats.JumpHeight * -2f * m_agentStats.Gravity);
            m_agentAnimations.SetTrigger(AnimationTriggerType.Jump);
            m_agentAnimations.SetBool(AnimationBoolType.Grounded, false);
        }

        public override void Exit()
        {
            m_agentAnimations.ResetTrigger(AnimationTriggerType.Jump);
        }

        protected override void StateUpdate(float deltaTime)
        {
            m_verticalVelocity += m_agentStats.Gravity * Time.deltaTime;

            //float targetMovementSpeed = m_input.SprintInput ? m_agentStats.SprintSpeed : m_agentStats.MoveSpeed;
            //targetMovementSpeed = m_input.MovementInput == Vector2.zero ? 0 : targetMovementSpeed;
            //m_mover.Move(
            //    new Vector3(m_input.MovementInput.x, m_verticalVelocity, m_input.MovementInput.y), targetMovementSpeed);
            m_movementHelper.PerformMovement(m_input, m_agentStats, m_mover, m_verticalVelocity);
        }
    }
}