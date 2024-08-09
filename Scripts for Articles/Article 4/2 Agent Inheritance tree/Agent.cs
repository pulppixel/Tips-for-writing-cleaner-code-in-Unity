using System;
using UnityEngine;

namespace Tips.Part_4_End
{
    /// <summary>
    /// Abstract definition of an agent that can move, can be animated,
    /// can access input and detect if it is grounded.
    /// We also have here the basic State Machine implementation that we can
    /// extend because it is a virtual protected method.
    /// </summary>
    public abstract class Agent : MonoBehaviour
    {
        // We use protected access modifier to
        // allow the derived classes to access
        protected IAgentMover m_mover;

        protected IAgentMovementInput m_input;

        protected GroundedDetector m_groundDetector;

        protected AgentAnimations m_agentAnimations;

        protected State m_currentState;

        protected AgentStats m_agentStats;

        // We make all methods protected virtual so that we can override them
        // in the derived classes
        protected virtual void Awake()
        {
            m_input = GetComponent<IAgentMovementInput>();

            m_groundDetector = GetComponent<GroundedDetector>();
            m_agentAnimations = GetComponent<AgentAnimations>();
            m_mover = GetComponent<IAgentMover>();
            m_agentStats = GetComponent<AgentStats>();
        }

        protected virtual void Start()
        {
            TransitionToState(typeof(MovementState));
        }
            /// <summary>
        /// State Factory defines base states that we can transition to.
        /// In the future it would be wise to extract this code to a separate
        /// class as I can already see that many agents might overrider it
        /// defining the same state transitions - like Player and JumpingNPC both
        /// wanting to have JumpState defined here.
        /// </summary>
        /// <param name="stateType"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        protected virtual State StateFactory(Type stateType)
        {
            State newState = null;
            if (stateType == typeof(MovementState))
            {
                newState = new MovementState(m_mover, m_groundDetector, m_agentAnimations, m_input, m_agentStats);
                newState.AddTransition(new GroundedFallTransition(m_groundDetector));
            }
            else if (stateType == typeof(FallState))
            {
                newState = new FallState(m_mover, m_agentAnimations, m_input, m_agentStats);
                newState.AddTransition(new FallLandTransition(m_groundDetector));
            }
            else if (stateType == typeof(LandState))
            {
                newState = new LandState(m_agentAnimations);
                newState.AddTransition(new LandMovementTransition());
            }
            else
            {
                throw new Exception($"Type not handled {stateType}");
            }
            return newState;
        }

        /// <summary>
        /// Method to transition to a new state. It would be wise to keep it
        /// private as this logic is tied to State Machine implementation. 
        /// There is hardly any reason why derived classes would need to override
        /// it.
        /// </summary>
        /// <param name="stateType"></param>
        private void TransitionToState(Type stateType)
        {
            State newState = StateFactory(stateType);
            if (m_currentState != null)
            {
                m_currentState.Exit();
                m_currentState.OnTransition -= TransitionToState;
            }
            m_currentState = newState;
            Debug.Log($"Entering {stateType}");
            m_currentState.OnTransition += TransitionToState;
            m_currentState.Enter();
        }

        protected virtual void Update()
        {
            if (m_currentState != null)
                m_currentState.Update(Time.deltaTime);
        }

        protected virtual void FixedUpdate()
        {
            m_groundDetector.GroundedCheck();
            m_agentAnimations.SetBool(AnimationBoolType.Grounded, m_groundDetector.Grounded);
        }

    }
}

