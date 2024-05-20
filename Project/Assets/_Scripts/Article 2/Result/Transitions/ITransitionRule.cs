using System;

namespace Tips.Part_2_End
{
    public interface ITransitionRule
    {
        bool ShouldTransition(float deltaTime);
        Type NextState { get; }
    }

}