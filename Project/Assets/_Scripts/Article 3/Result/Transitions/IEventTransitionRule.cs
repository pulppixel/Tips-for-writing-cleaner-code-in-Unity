namespace Tips.Part_3_End
{
    public interface IEventTransitionRule : ITransitionRule
    {
        void Subscribe();
        void Unsubscribe();
    }
}