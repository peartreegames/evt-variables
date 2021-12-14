namespace PeartreeGames.EvtVariables
{

    public interface IEvtVariable<T> : IEvtEvent<T>
    {
        T Value { get; set; }
    }
}