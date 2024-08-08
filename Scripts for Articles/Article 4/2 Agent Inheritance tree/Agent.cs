using System;
using UnityEngine;

namespace Tips.Part_4_End
{
    public abstract class Agent : MonoBehaviour
    {
        protected IAgentMover m_mover;

        protected IAgentMovementInput m_input;

        protected GroundedDetector m_groundDetector;

        protected AgentAnimations m_agentAnimations;

        protected State m_currentState;

        protected AgentStats m_agentStats;

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

