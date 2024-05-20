using System;
using UnityEngine;

namespace Tips.Part_3_Start
{
    public class Agent : MonoBehaviour
    {
        private BasicCharacterControllerMover m_mover;

        private IAgentMovementInput m_input;

        private GroundedDetector m_groundDetector;

        private AgentAnimations m_agentAnimations;

        private State m_currentState;

        private IAgentJumpInput m_jumpInput;

        //IAgentWaveInput allows us to handle an NPC interaction that allows it to stop and Wave to us.
        private IAgentWaveInput m_waveInput;

        //IAgentInteractInput is similar to jump input. It allows us to use LMB to interact with objects.
        //Read Article 2 if you are unsure why we are using a separate interface for each input.
        private IAgentInteractInput m_interactInput;

        //AgentStats hold parameters like speed / gravity that has been duplicated inside our states
        private AgentStats m_AgentStats;

        private void Awake()
        {
            m_input = GetComponent<IAgentMovementInput>();
            m_jumpInput = GetComponent<IAgentJumpInput>();
            m_waveInput = GetComponent<IAgentWaveInput>();
            m_interactInput = GetComponent<IAgentInteractInput>();

            m_groundDetector = GetComponent<GroundedDetector>();
            m_agentAnimations = GetComponent<AgentAnimations>();
            m_mover = GetComponent<BasicCharacterControllerMover>();
            m_AgentStats = GetComponent<AgentStats>();
        }

        private void Start()
        {
            TransitionToState(typeof(MovementState));
        }

        //StateFactory was modified to pass AgentStats to the States that requires those parameters
        private State StateFactory(Type stateType)
        {
            State newState = null;
            if (stateType == typeof(MovementState))
            {
                newState = new MovementState(m_mover, m_groundDetector, m_agentAnimations, m_input, m_AgentStats);
                newState.AddTransition(new GroundedFallTransition(m_groundDetector));
                //Null checks aren't ideal but this way we don't require NPC to implement ex JumpInput if it doesn't uses it
                if (m_jumpInput != null)
                    newState.AddTransition(new JumpTransition(m_jumpInput));
                if (m_waveInput != null)
                    newState.AddTransition(new MoveWaveTransition(m_waveInput));
                if (m_interactInput != null)
                    newState.AddTransition(new MoveInteractTransition(m_interactInput));
            }
            else if (stateType == typeof(FallState))
            {
                newState = new FallState(m_mover, m_agentAnimations, m_input, m_AgentStats);
                newState.AddTransition(new FallLandTransition(m_groundDetector));
            }
            else if (stateType == typeof(LandState))
            {
                newState = new LandState(m_agentAnimations);
                newState.AddTransition(new LandMovementTransition());
            }
            else if (stateType == typeof(JumpState))
            {
                newState = new JumpState(m_mover, m_agentAnimations, m_input, m_AgentStats);
                newState.AddTransition(new JumpFallTransition(m_mover));
            }
            else if (stateType == typeof(WaveState))
            {
                newState = new WaveState(m_agentAnimations);
                newState.AddTransition(new WaveMoveTransition());
            }
            else if (stateType == typeof(InteractState))
            {
                newState = new InteractState(m_agentAnimations);
                newState.AddTransition(new InteractMoveTransition());
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

