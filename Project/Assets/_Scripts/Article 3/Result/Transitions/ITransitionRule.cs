using System;

namespace Tips.Part_3_End
{
    public interface ITransitionRule
    {
        bool ShouldTransition(float deltaTime);
        Type NextState { get; }
    }

}