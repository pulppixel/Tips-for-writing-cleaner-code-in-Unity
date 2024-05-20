using UnityEngine;

namespace Tips.Part_3_Start
{

    public class InteractState : State
    {
        private AgentAnimations m_agentAnimations;
        private float m_slowDownDelay = 0.3f;
        private float m_delayTemp;
        private float m_startAnimationSpeed;
        public InteractState(AgentAnimations agentAnimations)
        {
            m_agentAnimations = agentAnimations;
            m_delayTemp = m_slowDownDelay;
        }

        public override void Enter()
        {
            m_agentAnimations.SetTrigger(AnimationTriggerType.Interact);
            m_startAnimationSpeed = m_agentAnimations.GetFloat(AnimationFloatType.Speed);
            Debug.Log("Interaction State: Interacting!");
        }

        public override void Exit()
        {
            m_agentAnimations.ResetTrigger(AnimationTriggerType.Interact);
        }

        protected override void StateUpdate(float deltaTime)
        {
            if (m_delayTemp >= 0)
            {
                m_agentAnimations.SetFloat(AnimationFloatType.Speed, m_startAnimationSpeed * m_delayTemp / m_slowDownDelay);
                m_delayTemp -= deltaTime;
            }
        }
    }
}