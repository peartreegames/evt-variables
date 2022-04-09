using System;
using UnityEngine;

namespace PeartreeGames.EvtVariables
{
    public abstract class EvtVariableObject : ScriptableObject { }

    public abstract class EvtVariableObject<T> : EvtVariableObject, IEvtVariable<T>
    {
        [SerializeField] protected T value;
        private readonly EvtEvent<T> _evt = new();
        
        public virtual T Value
        {
            get => value;
            set
            {
                if (IsEqual(this.value, value)) return;
                this.value = value;
                _evt.Invoke(value);
            }
        }

        protected virtual bool IsEqual(T current, T other) => current?.Equals(other) ?? false;

        public virtual void Subscribe(Action listener) => _evt.Subscribe(listener);
        public virtual void Unsubscribe(Action listener) => _evt.Unsubscribe(listener);
        public virtual void Subscribe(Action<T> listener) => _evt.Subscribe(listener);
        public virtual void Unsubscribe(Action<T> listener) => _evt.Unsubscribe(listener);
        
    }
}