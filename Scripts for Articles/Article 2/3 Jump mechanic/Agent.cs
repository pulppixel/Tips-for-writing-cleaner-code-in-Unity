using System;
using UnityEngine;

namespace Tips.Part_2_End
{
    /// <summary>
    /// Agent is a reusable concept of a character entity - Player / NPC that can move, jump, wave and interact with objects.
    /// </summary>
    public class Agent : MonoBehaviour
    {
        private BasicCharacterControllerMover m_mover;

        private IAgentMovementInput m_input;

        private GroundedDetector m_groundDetector;

        private AgentAnimations m_agentAnimations;

        private State m_currentState;

        private IAgentJumpInput m_jumpInput;

        private void Awake()
        {
            m_input = GetComponent<IAgentMovementInput>();
            m_jumpInput = GetComponent<IAgentJumpInput>();

            m_groundDetector = GetComponent<GroundedDetector>();
            m_agentAnimations = GetComponent<AgentAnimations>();
            m_mover = GetComponent<BasicCharacterControllerMover>();
        }

        private void Start()
        {
            TransitionToState(typeof(MovementState));
        }


        private State StateFactory(Type stateType)
        {
            State newState = null;
            if (stateType == typeof(MovementState))
            {
                newState = new MovementState(m_mover, m_groundDetector, m_agentAnimations, m_input);
                newState.AddTransition(new GroundedFallTransition(m_groundDetector));
                if (m_jumpInput != null)
                    newState.AddTransition(new JumpTransition(m_jumpInput));

            }
            else if (stateType == typeof(FallState))
            {
                newState = new FallState(m_mover, m_agentAnimations, m_input);
                newState.AddTransition(new FallLandTransition(m_groundDetector));
            }
            else if (stateType == typeof(LandState))
            {
                newState = new LandState(m_agentAnimations);
                newState.AddTransition(new LandMovementTransition());
            }
            else if (stateType == typeof(JumpState))
            {
                newState = new JumpState(m_mover, m_agentAnimations, m_input);
                newState.AddTransition(new JumpFallTransition(m_mover));
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

        private void Update()
        {
            if (m_currentState != null)
                m_currentState.Update(Time.deltaTime);
        }

        private void FixedUpdate()
        {
            m_groundDetector.GroundedCheck();
            m_agentAnimations.SetBool(AnimationBoolType.Grounded, m_groundDetector.Grounded);
        }

    }
}

