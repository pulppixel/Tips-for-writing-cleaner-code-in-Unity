namespace Tips.Part_4_End
{
    public interface IActiveInteractable : IInteractable
    {
        bool IsInteractionActive { get; }
    }

}