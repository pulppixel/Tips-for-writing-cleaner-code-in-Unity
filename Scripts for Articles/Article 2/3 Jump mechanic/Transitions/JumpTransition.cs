using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tips.Part_2_End
{
    public class JumpTransition : ITransitionRule
    {
        /// <summary>
        /// Transition that utilizes IAgentJumpInput to listen to Input event in order to trigger the transition.
        /// It adds a bit of a delay so that we can't jump immediatelly after landing. Thic condition is optional.
        /// </summary>
        public Type NextState => typeof(JumpState);
        public float m_jumpTimeout = 0.20f;

        private IAgentJumpInput m_jumpInput;

        public JumpTransition(IAgentJumpInput jumpInput)
        {
            m_jumpInput = jumpInput;
        }

        public bool ShouldTransition(float deltaTime)
        {
            if (m_jumpTimeout <= 0 && m_jumpInput.JumpInput)
                return true;
            m_jumpTimeout -= deltaTime;
            return false;
        }
    }
}
