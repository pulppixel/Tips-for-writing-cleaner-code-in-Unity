using UnityEngine;

/// <summary>
/// Stores data such as MovementSpeed for each character separately. This is an improvement over the base State Pattern implementation from Article 2 where those
/// values were hardcoded. This also serves a purpose of removing code / parameter duplication from Jump, Movement and Fall states.
/// </summary>
public class AgentStats : MonoBehaviour
{
    [field: SerializeField]
    public float MoveSpeed { get; private set; } = 2.0f;
    [field: SerializeField]
    public float SprintSpeed { get; private set; } = 5.335f;
    [field: SerializeField]
    public float Gravity { get; private set; } = -15.0f;
    [field: SerializeField]
    public float SpeedChangeRate { get; private set; } = 10.0f;
    [field: SerializeField]
    public float JumpHeight { get; private set; } = 1.2f;
    [field: SerializeField]
    public float FallTimeout { get; private set; } = 0.15f;
}
