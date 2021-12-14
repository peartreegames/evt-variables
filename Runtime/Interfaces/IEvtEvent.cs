using System;

namespace PeartreeGames.EvtVariables
{
    public interface IEvtEvent
    {
        public void Subscribe(Action listener);
        public void Unsubscribe(Action listener);
    }

    public interface IEvtEvent<out T> : IEvtEvent
    {
        public void Subscribe(Action<T> listener);
        public void Unsubscribe(Action<T> listener);
    }
    
}