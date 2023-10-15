using System;
using System.Collections.Generic;
using UnityEngine;

namespace PeartreeGames.Evt.Variables
{
    
    [Serializable]
    public class EvtList<T> : EvtEvent<List<T>>
    {
        public EvtList() => value = new List<T>();
        public EvtList(List<T> startingValue) => value = startingValue;

        [SerializeField] private List<T> value;
        public List<T> Value => value;
        public void Add(T item)
        {
            Value.Add(item);
            Invoke(Value);
        }

        public void Remove(T item)
        {
            if (Value.Remove(item)) Invoke(Value);
        }

        public void Clear()
        {
            Value.Clear();
            Invoke(Value);
        }

        public bool Contains(T item) => Value.Contains(item);
    }
}