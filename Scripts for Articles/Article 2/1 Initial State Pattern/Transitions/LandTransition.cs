using System;

namespace Tips.Part_2_End
{
    public class LandTransition : ITransitionRule
    {
        public Type NextState => typeof(MovementState);
        private GroundedDetector m_groundedDetector;

        public LandTransition(GroundedDetector groundedDetector)
        {
            m_groundedDetector = groundedDetector;
        }

        public bool ShouldTransition(float deltaTime)
        {
            return m_groundedDetector.Grounded;
        }
    }
}
