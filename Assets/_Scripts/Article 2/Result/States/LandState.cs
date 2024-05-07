using System;

namespace Tips.Part_2_Result
{
    public class LandState : IState
    {
        private float m_landAnimationDelay = 0.533f;
        private float m_transitionDelayDelta;
        private AgentAnimations m_agentAnimations;

        public LandState(AgentAnimations agentAnimations)
        {
            m_agentAnimations = agentAnimations;
        }

        public event Action<Type> OnTransition;

        public void Enter()
        {
            m_transitionDelayDelta = m_landAnimationDelay;
        }

        public void Exit()
        {
            return;
        }

        public void Update(float deltaTime)
        {
            if(m_transitionDelayDelta <= 0)
            {
                m_agentAnimations.SetTrigger(AnimationTriggerType.Land);
                OnTransition?.Invoke(typeof(MovementState));
                return;
            }
            m_transitionDelayDelta -= deltaTime;
        }
    }
}

