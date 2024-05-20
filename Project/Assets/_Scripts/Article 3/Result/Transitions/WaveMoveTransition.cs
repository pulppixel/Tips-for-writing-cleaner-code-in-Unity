using System;

namespace Tips.Part_3_End
{
    public class WaveMoveTransition : ITransitionRule
    {
        private float m_waveAnimationDelay = 1.4f;
        public Type NextState => typeof(MovementState);

        public bool ShouldTransition(float deltaTime)
        {
            if (m_waveAnimationDelay <= 0)
                return true;
            m_waveAnimationDelay -= deltaTime;
            return false;
        }
    }
}

