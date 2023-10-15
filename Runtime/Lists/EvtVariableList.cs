using System;
using System.Collections.Generic;
using UnityEngine;

namespace PeartreeGames.Evt.Variables.Lists
{
    public abstract class EvtVariableList : ScriptableObject {}
    public abstract class EvtVariableList<T> :  EvtVariableList
    {
        #if ODIN_INSPECTOR
        [Sirenix.OdinInspector.ShowInInspector]
        #endif
        protected readonly EvtList<T> evt = new();
        public List<T> Value => evt.Value;
        public event Action<List<T>> OnEvt
        {
            add => evt.OnEvt += value;
            remove => evt.OnEvt -= value;
        }
    
        public void Add(T item) => Value.Add(item);
        public void Remove(T item) => Value.Remove(item);
        public void Clear() => Value.Clear();
        public bool Contains(T item) => Value.Contains(item);
    }
}