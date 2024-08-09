using UnityEngine;

namespace Tips.Part_4_End
{
    /// <summary>
    /// Interface for providing movement input for an agent.
    /// This interface abstracts the movement input to decouple the input system from the agent's movement logic.
    /// </summary>
    public interface IAgentMover
    {
        void Move(Vector3 input, float speed);
        Vector3 CurrentVelocity { get; }
    }
}