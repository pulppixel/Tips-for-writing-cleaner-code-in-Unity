namespace Tips.Part_3_Start
{
    public interface IEventTransitionRule : ITransitionRule
    {
        void Subscribe();
        void Unsubscribe();
    }
}