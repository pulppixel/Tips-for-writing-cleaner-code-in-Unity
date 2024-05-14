using System;

namespace Tips.Part_2_End
{
    public class JumpFallTransition : ITransitionRule
    {
        public Type NextState => typeof(FallState);

        private BasicCharacterControllerMover m_mover;
        //Delay to let the JumpSate call Move() on the m_mover so that we don't imediatelly
        //exit from Jump to Fall state
        private float m_checkDelay = 0.2f;

        public JumpFallTransition(BasicCharacterControllerMover mover)
        {
            m_mover = mover;
        }

        public bool ShouldTransition(float deltaTime)
        {
            if (m_checkDelay <= 0)
                return m_mover.CurrentVelocity.y <= 0;
            m_checkDelay -= deltaTime;
            return false;
        }
    }
}