using System;

namespace Tips.Part_3_End
{
    public class MoveWaveTransition : IEventTransitionRule
    {
        private IAgentWaveInput m_waveInput;
        private bool m_waveFlag = false;
        public Type NextState => typeof(WaveState);

        public MoveWaveTransition(IAgentWaveInput waveInput)
        {
            m_waveInput = waveInput;
        }

        public bool ShouldTransition(float deltaTime)
        {
            return m_waveFlag;
        }

        public void Subscribe()
        {
            m_waveInput.OnWaveInput += HandleWaveInput;
        }

        private void HandleWaveInput()
        {
            m_waveFlag = true;
        }

        public void Unsubscribe()
        {
            m_waveInput.OnWaveInput -= HandleWaveInput;
        }
    }
}