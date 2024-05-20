namespace Tips.Part_3_Start
{
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

        protected override void StateUpdate(float deltaTime)
        {
            return;
        }
    }
}
