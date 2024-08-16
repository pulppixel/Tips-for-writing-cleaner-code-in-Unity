using System;
using UnityEngine;

namespace Tips.Part_5_End
{
    /// <summary>
    /// PlayerAgent represents our Players behavior. We make it depend on
    /// the Jump input and Interaction input and detector.
    /// </summary>
    public class PlayerAgent : Agent
    {
        private IAgentJumpInput m_jumpInput;

        private IAgentInteractInput m_interactInput;

        private IAgentAttackInput m_attackInput;

        private InteractionDetector m_interactDetector;

        [SerializeField]
        private WeaponHelper m_weaponHelper;

        private IAgentToggleWeaponInput m_toggleWeaponInput;

        [SerializeField]
        private HitDetector m_hitDetector;

        private Health m_health;

        protected override void Awake()
        {
            base.Awake();
            m_jumpInput = GetComponent<IAgentJumpInput>();
            m_interactInput = GetComponent<IAgentInteractInput>();
            m_interactDetector = GetComponent<InteractionDetector>();
            m_weaponHelper = GetComponent<WeaponHelper>();
            m_toggleWeaponInput = GetComponent<IAgentToggleWeaponInput>();
            m_attackInput = GetComponent<IAgentAttackInput>();
            m_hitDetector = GetComponent<HitDetector>();
            m_health = GetComponent<Health>();
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
            //We add DrawWeaponState and AttackState to the StateFactory
            else if (stateType == typeof(DrawWeaponState))
            {
                newState = new DrawWeaponState(m_weaponHelper, m_mover, m_agentAnimations, m_input, m_groundDetector, m_agentStats);
                newState.AddTransition(new DelayedTransition(0.2f, typeof(MovementState)));
            }
            else if (stateType == typeof(AttackState))
            {
                newState = new AttackState(m_agentAnimations, m_mover, m_agentStats, gameObject, m_hitDetector, 0.03f);
                newState.AddTransition(new DelayedTransition(0.35f, typeof(MovementState)));
            }
            else if (stateType == typeof(GetHitState))
            {
                newState = new GetHitState(m_agentAnimations, m_mover, m_agentStats);
                newState.AddTransition(new DelayedTransition(0.32f, typeof(MovementState)));
            }
            else
            {
                newState = base.StateFactory(stateType);
                if (stateType == typeof(MovementState))
                {
                    // To reuse the Interact input as Attack input we use
                    // WeaponHelper by checking if player has weapon and
                    // if it is drawn or holstered.
                    newState.AddTransition(new JumpTransition(m_jumpInput));
                    if (m_weaponHelper.HasWeapon == false || m_weaponHelper.IsWeaponHolstered)
                    {
                        newState.AddTransition(new MoveInteractTransition(m_interactInput, m_interactDetector));
                    }
                    if (m_weaponHelper.HasWeapon && m_weaponHelper.IsWeaponHolstered == false)
                    {
                        newState.AddTransition(new MoveAttackTransition(m_attackInput));
                    }
                    if (m_weaponHelper.HasWeapon)
                    {
                        newState.AddTransition(new MoveDrawWeaponTransition(m_toggleWeaponInput));
                    }
                    newState.AddTransition(new GetHitTransition(m_health));
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