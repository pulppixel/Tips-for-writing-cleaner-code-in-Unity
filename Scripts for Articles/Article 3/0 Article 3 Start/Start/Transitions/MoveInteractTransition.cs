using System;

namespace Tips.Part_3_Start
{
    /// <summary>
    /// Transitions between Movement State and Interact State by listening for Interact Input. 
    /// </summary>
    public class MoveInteractTransition : IEventTransitionRule
    {
        private IAgentInteractInput m_interactInput;
        private bool m_interactFlag;
        public Type NextState => typeof(InteractState);

        public MoveInteractTransition(IAgentInteractInput interactInput)
        {
            m_interactInput = interactInput;
        }

        public bool ShouldTransition(float deltaTime)
        {
            return m_interactFlag;
        }

        public void Subscribe()
        {
            m_interactInput.OnInteract += HandleInteraction;
        }

        private void HandleInteraction()
        {
            m_interactFlag = true;
        }

        public void Unsubscribe()
        {
            m_interactInput.OnInteract -= HandleInteraction;
        }
    }
}
