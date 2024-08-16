using System;

namespace Tips.Part_5_End
{
    public class NPCDeathTransition : ITransitionRule
    {
        public Type NextState => typeof(NavMeshNPCDeathState);
        private Health m_health;

        public NPCDeathTransition(Health health)
        {
            m_health = health;
        }

        public bool ShouldTransition(float deltaTime)
        {
            return m_health.CurrentHealth <= 0;
        }
    }
}