namespace Tips.Part_4_Start
{
    public interface IActiveInteractable : IInteractable
    {
        bool IsInteractionActive { get; }
    }

}