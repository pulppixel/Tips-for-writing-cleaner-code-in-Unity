using System;
using UnityEngine;

namespace Tips.Part_4_End
{
    /// <summary>
    /// PlayerAgent represents our Players behavior. We make it depend on
    /// the Jump input and Interaction input and detector.
    /// </summary>
    public class PlayerAgent : Agent
    {
        private IAgentJumpInput m_jumpInput;

        private IAgentInteractInput m_interactInput;

        private InteractionDetector m_interactDetector;

        [SerializeField]
        private WeaponHelper m_weaponHelper;

        protected override void Awake()
        {
            base.Awake();
            m_jumpInput = GetComponent<IAgentJumpInput>();
            m_interactInput = GetComponent<IAgentInteractInput>();
            m_interactDetector = GetComponent<InteractionDetector>();
            m_weaponHelper = GetComponent<WeaponHelper>();
        }

        /// <summary>
        /// We overrider StateFactory to allow for transition to JumpState and
        /// InteractState and defined how those states should be created.
        /// </summary>
        /// <param name="stateType"></param>
        /// <returns></returns>
        protected override State StateFactory(Type stateType)
        {
            State newState = null;
            if (stateType == typeof(JumpState))
            {
                newState = new JumpState(m_mover, m_agentAnimations, m_input, m_agentStats);
                newState.AddTransition(new JumpFallTransition(m_mover));
            }
            else if (stateType == typeof(InteractState))
            {
                newState = new InteractState(m_agentAnimations, m_interactDetector);
                newState.AddTransition(new InteractMoveTransition());
            }
            else
            {
                newState = base.StateFactory(stateType);
                if (stateType == typeof(MovementState))
                {
                    newState.AddTransition(new JumpTransition(m_jumpInput));
                    newState.AddTransition(new MoveInteractTransition(m_interactInput, m_interactDetector));
                }
            }
            return newState;
        }

        protected override void Update()
        {
            base.Update();
            m_interactDetector.DetectInteractable();
        }
    }
}