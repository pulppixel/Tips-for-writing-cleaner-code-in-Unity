using System;

namespace Tips.Part_2_End
{
    /// <summary>
    /// Waits the duration of Land Animation before transitioning.
    /// One downside is that if we modify Land Animation we need to manually change it.
    /// An alternative would be to ask Animator through AgentAnimations script about this length
    /// or in some other way obtain it directly from the animation.
    /// </summary>
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
