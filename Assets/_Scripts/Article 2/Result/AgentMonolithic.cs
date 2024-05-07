using System;
using UnityEngine;

namespace Tips.Part_2_Result
{
    public class AgentMonolithic : MonoBehaviour
    {
        [Header("Movement Parameters")]
        public float MoveSpeed = 2.0f;
        public float SprintSpeed = 5.335f;
        public float SpeedChangeRate = 10.0f;
        public float FallTimeout = 0.15f;

        private BasicCharacterControllerMover m_mover;

        private IAgentMovementInput m_input;

        public float Gravity = -15.0f;

        private GroundedDetector m_groundDetector;

        private AgentAnimations m_agentAnimations;

        [Space(10)]
        [Header("Jumping")]
        [Tooltip("The height the player can jump")]
        public float JumpHeight = 1.2f;
        [Space(10)]
        [Tooltip("Time required to pass before being able to jump again. Set to 0f to instantly jump again")]
        public float JumpTimeout = 0.50f;

        private IAgentJumpInput m_jumpInput;

        private IState m_currentState;
        

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

        private void TransitionToState(Type stateType)
        {
            IState newState = StateFactory(stateType);
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

        private IState StateFactory(Type stateType)
        {
            if(stateType == typeof(MovementState))
            {
                return new MovementState(m_mover, m_groundDetector, m_agentAnimations, m_input, m_jumpInput);
            }
            else if (stateType == typeof(FallState))
            {
                return new FallState(m_mover, m_groundDetector, m_agentAnimations, m_input);
            }
            else if (stateType == typeof(JumpState))
            {
                return new JumpState(m_mover, m_agentAnimations, m_input);
            }
            else if (stateType == typeof(LandState))
            {
                return new LandState(m_agentAnimations);
            }
            else
            {
                throw new Exception($"Type not handled {stateType}");
            }
        }


        private void Update()
        {
            if (m_currentState != null)
            {
                m_currentState.Update(Time.deltaTime);
            }
        }


        private void FixedUpdate()
        {
            m_groundDetector.GroundedCheck();
        }
    }
}

