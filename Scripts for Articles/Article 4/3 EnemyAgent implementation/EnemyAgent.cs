using System;
using UnityEngine;

namespace Tips.Part_4_End
{
    /// <summary>
    /// Enemy agent class that will extend the Agent implementation with the
    /// AttackSate and that will make the Agent depend on IAgentAttackInput so
    /// that we can trigger the new state.
    /// </summary>
    public class EnemyAgent : Agent
    {
        [SerializeField]
        private IAgentAttackInput m_attackInput;

        protected override void Awake()
        {
            base.Awake();
            m_attackInput = GetComponent<IAgentAttackInput>();
        }

        /// <summary>
        /// We override the StateFactory method to create the AttackState 
        /// and to ensure that we can transition from the MovementState to the
        /// AttackState and back.
        /// </summary>
        /// <param name="stateType"></param>
        /// <returns></returns>
        protected override State StateFactory(Type stateType)
        {
            State newState = null;
            if (stateType == typeof(AttackState))
            {
                newState = new AttackState(m_agentAnimations, m_mover);
                newState.AddTransition(new DelayedTransition(2f, typeof(MovementState)));
            }
            else
            {
                newState = base.StateFactory(stateType);
                if (stateType == typeof(MovementState))
                {
                    newState.AddTransition(new MoveAttackTransition(m_attackInput));
                }
            }

            return newState;
        }
    }
}