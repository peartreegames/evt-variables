using System;
using System.Collections.Generic;
using UnityEngine;

namespace PeartreeGames.EvtVariables.EvtCollections
{

    public abstract class EvtVariableCollection<T> : EvtVariableObject, IEvtVariable<List<T>>
    {
        [SerializeField] private List<T> value;
        private readonly EvtEvent<List<T>> _evt = new();
        
        public virtual List<T> Value
        {
            get => value;
            set
            {
                if (this.value == value) return;
                this.value = value;
                _evt.Invoke(value);
            }
        }
        
        public void Add(T item)
        {
            Value.Add(item);
            _evt.Invoke(value);
        }

        public void Remove(T item)
        {
            if (Value.Remove(item)) _evt.Invoke(value);
        }

        public bool Contains(T item) => value.Contains(item);
        
        public void Subscribe(Action listener) => _evt.Subscribe(listener);
        public void Unsubscribe(Action listener) => _evt.Unsubscribe(listener);
        public void Subscribe(Action<List<T>> listener) => _evt.Subscribe(listener);
        public void Unsubscribe(Action<List<T>> listener) => _evt.Unsubscribe(listener);
    }
}