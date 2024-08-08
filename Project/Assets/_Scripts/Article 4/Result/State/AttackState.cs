using UnityEngine;

namespace Tips.Part_4_End
{
    /// <summary>
    /// Attack state where the agent will play the attack animation.
    /// We need m_mover to stop the movement of the agent before we play 
    /// the attack animation.
    /// </summary>
    public class AttackState : State
    {
        private AgentAnimations m_agentAnimations;
        private IAgentMover m_mover;

        public AttackState(AgentAnimations agentAnimations, IAgentMover mover)
        {
            m_agentAnimations = agentAnimations;
            m_mover = mover;
        }

        public override void Enter()
        {
            m_mover.Move(Vector3.zero, 0);
            m_agentAnimations.SetFloat(AnimationFloatType.Speed, 0);
            m_agentAnimations.SetTrigger(AnimationTriggerType.Attack);
        }

        public override void Exit()
        {
            return;
        }

        protected override void StateUpdate(float deltaTime)
        {
            return;
        }
    }
}