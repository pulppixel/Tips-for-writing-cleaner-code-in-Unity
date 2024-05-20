using UnityEngine;

namespace Tips.Part_3_Start
{
    public interface IAgentMovementInput
    {
        public Vector2 MovementInput { get; }
        public bool SprintInput { get; }
    }
}