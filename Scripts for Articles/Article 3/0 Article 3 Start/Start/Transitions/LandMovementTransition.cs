using System;

namespace Tips.Part_3_Start
{
    public class LandMovementTransition : ITransitionRule
    {
        //Land Animation length in seconds
        private float m_landAnimationDelay = 0.533f;

        public Type NextState => typeof(MovementState);

        public bool ShouldTransition(float deltaTime)
        {
            if (m_landAnimationDelay <= 0)
            {
                return true;
            }
            m_landAnimationDelay -= deltaTime;
            return false;
        }
    }
}

