using System;

namespace Tips.Part_4_End
{
    /// <summary>
    /// Agent implementation that allows the NPC to wave at the player beside having the default states defined
    /// inside the abstract Agent class
    /// </summary>
    public class NPCAgent : Agent
    {
        private IAgentWaveInput m_waveInput;

        protected override void Awake()
        {
            base.Awake();
            m_waveInput = GetComponent<IAgentWaveInput>();
        }

        protected override State StateFactory(Type stateType)
        {
            State newState = null;
            if (stateType == typeof(WaveState))
            {
                newState = new WaveState(m_agentAnimations);
                newState.AddTransition(new WaveMoveTransition());
            }
            else
            {
                newState = base.StateFactory(stateType);
                if (stateType == typeof(MovementState))
                {
                    newState.AddTransition(new MoveWaveTransition(m_waveInput));
                }
            }
            return newState;
        }
    }
}