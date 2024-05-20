namespace Tips.Part_3_End
{
    public interface IActiveInteractable : IInteractable
    {
        bool IsInteractionActive { get; }
    }

}