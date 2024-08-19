using System;

namespace Tips.Part_5_Start
{
    public class NPCDeathTransition : ITransitionRule
    {
        public Type NextState => typeof(NavMeshNPCDeathState);

        public bool ShouldTransition(float deltaTime)
        {
            return false;
        }
    }
}