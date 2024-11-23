using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PeartreeGames.Evt.Variables
{
    
    [Serializable]
    public class EvtList<T> : EvtEvent<List<T>>, IEnumerable<T>
    {
        public EvtList() => value = new List<T>();
        public EvtList(List<T> startingValue) => value = startingValue;

        [SerializeField] private List<T> value;
        public List<T> Value => value;
        public void Add(T item)
        {
            value.Add(item);
            Invoke(value);
        }

        public void Remove(T item)
        {
            if (value.Remove(item)) Invoke(value);
        }

        public void Clear()
        {
            value.Clear();
            Invoke(value);
        }

        public bool Contains(T item) => Value.Contains(item);

        public void Sort(Comparison<T> func)
        {
            value.Sort(func);
            Invoke(value);
        }

        public IEnumerator<T> GetEnumerator() => value.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}