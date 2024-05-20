using System;

namespace Tips.Part_3_End
{
    public class InteractMoveTransition : ITransitionRule
    {
        private float m_interactAnimationDelay = 1.2f;
        public Type NextState => typeof(MovementState);

        public bool ShouldTransition(float deltaTime)
        {
            if (m_interactAnimationDelay <= 0)
                return true;
            m_interactAnimationDelay -= deltaTime;
            return false;
        }
    }
}
