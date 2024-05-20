namespace Tips.Part_3_End
{
    /// <summary>
    /// LandState triggers the land animation and just waits until a transition based on the Land animation length occurs.
    /// </summary>
    public class LandState : State
    {
        private AgentAnimations m_agentAnimations;

        public LandState(AgentAnimations agentAnimations)
        {
            m_agentAnimations = agentAnimations;
        }

        public override void Enter()
        {
            m_agentAnimations.SetFloat(AnimationFloatType.Speed, 0);
        }

        public override void Exit()
        {
            m_agentAnimations.SetTrigger(AnimationTriggerType.Land);
        }

        //One downside of using abstract methods for is that some states might have empty methods.
        //An alternative would be to have a more granular interfaces for our state or use virtual methods instead.
        protected override void StateUpdate(float deltaTime)
        {
            return;
        }
    }
}
