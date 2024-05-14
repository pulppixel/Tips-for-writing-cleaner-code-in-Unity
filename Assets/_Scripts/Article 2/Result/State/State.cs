using System;
using System.Collections.Generic;

namespace Tips.Part_2_End
{
    public abstract class State
    {
        private List<ITransitionRule> TransitionRules = new();

        public event Action<Type> OnTransition;
        public abstract void Enter();
        public void Update(float deltaTime)
        {
            if (ShouldTransition(deltaTime))
                return;
            StateUpdate(deltaTime);
        }

        protected abstract void StateUpdate(float deltaTime);

        public abstract void Exit();
        private bool ShouldTransition(float deltaTime)
        {
            foreach (ITransitionRule rule in TransitionRules)
            {
                if (rule.ShouldTransition(deltaTime))
                {
                    OnTransition?.Invoke(rule.NextState);
                    return true;
                }
            }
            return false;
        }
        public void AddTransition(ITransitionRule rule)
        {
            TransitionRules.Add(rule);
        }
    }

}