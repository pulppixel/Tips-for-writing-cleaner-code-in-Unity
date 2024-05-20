using UnityEngine;

namespace Tips.Part_2_End
{
    public interface IAgentMovementInput
    {
        public Vector2 MovementInput { get; }
        public bool SprintInput { get; }
    }
}