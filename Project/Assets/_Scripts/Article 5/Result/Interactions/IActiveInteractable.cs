namespace Tips.Part_5_End
{
    public interface IActiveInteractable : IInteractable
    {
        bool IsInteractionActive { get; }
    }

}