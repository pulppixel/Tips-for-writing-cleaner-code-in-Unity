using System;

namespace Tips.Part_2_Result
{
    public interface IState
    {
        event Action<Type> OnTransition;
        void Enter();
        void Update(float deltaTime);
        void Exit();
    }
}
