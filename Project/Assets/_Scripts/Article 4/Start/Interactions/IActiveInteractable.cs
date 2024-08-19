namespace Tips.Part_4_Start
{
    /// <summary>
    /// Interface defining the interactable item that can be enabled or disabled
    /// </summary>
    public interface IActiveInteractable : IInteractable
    {
        bool IsInteractionActive { get; }
    }

}