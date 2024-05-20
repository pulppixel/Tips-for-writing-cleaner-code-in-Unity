using UnityEngine;

namespace Tips.Part_2_End
{
    /// <summary>
    /// JumpState triggers the Jump animation. In OnEnter it calculates the Jump vertical velocity. It is applied in the Update for consistency.
    /// This might cause some issues as some transition will need to wait until the Update runs before they can check the Y velocity. For example
    /// JumpFallTransition checks this value so we need to give it a small delay before it runs it check.
    /// </summary>
    public class JumpState : State
    {
        private AgentAnimations m_agentAnimations;
        private IAgentMovementInput m_input;
        private BasicCharacterControllerMover m_mover;

        private float m_verticalVelocity;
        private float m_jumpHeight = 1.2f;
        private float m_gravity = -15.0f;
        private float m_moveSpeed = 2.0f;
        private float m_sprintSpeed = 5.335f;

        public JumpState(BasicCharacterControllerMover mover, AgentAnimations agentAnimations, IAgentMovementInput input)
        {
            m_agentAnimations = agentAnimations;
            m_input = input;
            m_mover = mover;
        }

        public override void Enter()
        {
            m_verticalVelocity = Mathf.Sqrt(m_jumpHeight * -2f * m_gravity);
            m_agentAnimations.SetTrigger(AnimationTriggerType.Jump);
            m_agentAnimations.SetBool(AnimationBoolType.Grounded, false);
        }

        public override void Exit()
        {
            m_agentAnimations.ResetTrigger(AnimationTriggerType.Jump);
        }

        protected override void StateUpdate(float deltaTime)
        {
            float targetMovementSpeed = m_input.SprintInput ? m_sprintSpeed : m_moveSpeed;
            targetMovementSpeed = m_input.MovementInput == Vector2.zero ? 0 : targetMovementSpeed;

            m_verticalVelocity += m_gravity * Time.deltaTime;
            m_mover.Move(
                new Vector3(m_input.MovementInput.x, m_verticalVelocity, m_input.MovementInput.y), targetMovementSpeed);
        }
    }
}