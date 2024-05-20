using System;

namespace Tips.Part_3_Start
{
    public interface ITransitionRule
    {
        bool ShouldTransition(float deltaTime);
        Type NextState { get; }
    }

}