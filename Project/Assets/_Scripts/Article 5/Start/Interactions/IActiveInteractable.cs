namespace Tips.Part_5_Start
{
    public interface IActiveInteractable : IInteractable
    {
        bool IsInteractionActive { get; }
    }

}