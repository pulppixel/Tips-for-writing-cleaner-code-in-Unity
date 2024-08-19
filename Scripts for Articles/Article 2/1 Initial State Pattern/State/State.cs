using System;
using System.Collections.Generic;

namespace Tips.Part_2_End
{
    /// <summary>
    /// Abstract definition of a State. Allows us to separate the Agents behaviors from the Agent script.
    /// Most State Patterns requires at least 3 methods: OnEnter, OnExit and Update. Using OnTransitionEvent
    /// allows us to separate State from knowing anything about the Agent itself and how it Transitions between
    /// different states.
    /// </summary>
    public abstract class State
    {
        protected List<ITransitionRule> TransitionRules = new();

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