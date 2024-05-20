using System;
using UnityEngine;

namespace Tips.Part_2_End
{
    public class Agent : MonoBehaviour
    {
        private BasicCharacterControllerMover m_mover;

        private IAgentMovementInput m_input;

        private GroundedDetector m_groundDetector;

        private AgentAnimations m_agentAnimations;

        private State m_currentState;

        private IAgentJumpInput m_jumpInput;

        //Getting reference to Unity specific objects
        private void Awake()
        {
            m_input = GetComponent<IAgentMovementInput>();
            //IAgentJumpInput is a specific interface that knows about the JumpInput
            m_jumpInput = GetComponent<IAgentJumpInput>();

            m_groundDetector = GetComponent<GroundedDetector>();
            m_agentAnimations = GetComponent<AgentAnimations>();
            m_mover = GetComponent<BasicCharacterControllerMover>();
        }

        //I want to ensure that at the start we transition to MovementState.
        //This way we can ensure that we transition to Falling if in the air.
        private void Start()
        {
            TransitionToState(typeof(MovementState));
        }

        //StateFactor is an example of FactoryMethod pattern. It allows us to limit the number of places
        //where we need to mak changes to this method. We could convert it into an Abstract Factory to make
        //our code more maintainable (to avoid the need to modify the code)
        private State StateFactory(Type stateType)
        {
            //Setting the state to null simplifies changes to adding a new if statement. 
            State newState = null;
            if (stateType == typeof(MovementState))
            {
                //One issue to address is the length of the constructor 
                newState = new MovementState(m_mover, m_groundDetector, m_agentAnimations, m_input);
                //Adding a new transition means adding new transition inside an existing if statements
                //We could possibly use ScriptableObjects to create a drag-and-drop system for adding those
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
                //If we forget to modify StateFactory method when adding a new State this exception will be thrown
                throw new Exception($"Type not handled {stateType}");
            }
            return newState;
        }

        //This code uses "Type" to transition between states. It can affect performance. We would instead
        //use 'string stateName' or 'enum StateType'. Each has its own issues. String based solution means that 
        //we can easily make a type. Enum based solution means the need to remember to add the new state to enum definition.
        //Using 'Type' is my Design Choice. Feel free to use another solution in your own implementation.
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

