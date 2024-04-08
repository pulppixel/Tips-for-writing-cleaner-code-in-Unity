using System;
using UnityEngine;

namespace Tips.Part_1_Result
{
    public interface IAgentMovementInput
    {
        public Vector2 MovementInput { get; }
        public bool SprintInput { get; }
    }
}