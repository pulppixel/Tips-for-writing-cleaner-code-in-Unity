using System;

namespace Tips.Part_3_End
{
    public class GroundedFallTransition : ITransitionRule
    {
        public Type NextState => typeof(FallState);
        private GroundedDetector m_groundedDetector;

        public GroundedFallTransition(GroundedDetector groundedDetector)
        {
            m_groundedDetector = groundedDetector;
        }

        public bool ShouldTransition(float deltaTime)
        {
            return m_groundedDetector.Grounded == false && m_groundedDetector.StairsGrounded == false;
        }
    }
}
