using UnityEngine;

namespace Tips.Part_2_End
{
    /// <summary>
    /// MovementState happens when we are grounded and we want to stand idle or move around. There is some code duplication between Movement, Jump and Fall state.
    /// I will address that in Article 3
    /// </summary>
    public class MovementState : State
    {
        private BasicCharacterControllerMover m_mover;
        private GroundedDetector m_groundedDetector;
        private AgentAnimations m_agentAnimations;
        private IAgentMovementInput m_input;

        private float m_moveSpeed = 2.0f;
        private float m_sprintSpeed = 5.335f;

        private float m_verticalVelocity;
        private float m_gravity = -15.0f;

        private float m_animationMovementSpeed;
        private float m_speedChangeRate = 10.0f;

        //One downside is a lengthy constructor. We could create a new object that encapsulates some of those.
        public MovementState(BasicCharacterControllerMover mover, GroundedDetector groundedDetector, AgentAnimations agentAnimations, IAgentMovementInput movementInput)
        {
            m_mover = mover;
            m_groundedDetector = groundedDetector;
            m_agentAnimations = agentAnimations;
            m_input = movementInput;
        }

        public override void Enter()
        {
            m_agentAnimations.SetBool(AnimationBoolType.Grounded, m_groundedDetector.Grounded);
        }

        public override void Exit()
        {
            return;
        }

        protected override void StateUpdate(float deltaTime)
        {

            if (m_groundedDetector.Grounded == false)
            {
                m_verticalVelocity += m_gravity * Time.deltaTime;
            }
            else
            {
                m_verticalVelocity = 0;
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

