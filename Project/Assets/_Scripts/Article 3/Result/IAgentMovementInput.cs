using UnityEngine;

namespace Tips.Part_3_End
{
    public interface IAgentMovementInput
    {
        public Vector2 MovementInput { get; }
        public bool SprintInput { get; }
    }
}