using System;

namespace Tips.Part_2_End
{
    public class FallLandTransition : ITransitionRule
    {
        public Type NextState => typeof(LandState);
        private GroundedDetector m_groundedDetector;

        public FallLandTransition(GroundedDetector groundedDetector)
        {
            m_groundedDetector = groundedDetector;
        }

        public bool ShouldTransition(float deltaTime)
        {
            return m_groundedDetector.Grounded;
        }
    }
}
