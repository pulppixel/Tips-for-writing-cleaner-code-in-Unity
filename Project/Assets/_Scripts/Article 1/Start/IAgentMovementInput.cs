using UnityEngine;

namespace Tips.Part_1_Start
{
    /// <summary>
    /// Agent에 대한 Movement Input을 제공하기 위한 인터페이스
    /// 이 인터페이스는 Movement Input을 추상화하여 Agent의 이동 로직에서 Input System을 분리한다.
    /// </summary>
    public interface IAgentMovementInput
    {
        public Vector2 MovementInput { get; }
        public bool SprintInput { get; }
    }
}