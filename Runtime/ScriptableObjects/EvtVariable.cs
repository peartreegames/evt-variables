using System;
using UnityEngine;

namespace PeartreeGames.Evt.Variables
{
    public abstract class EvtVariable : ScriptableObject { }
    
    public abstract class EvtVariable<T> : EvtVariable
    {
        [SerializeField] private EvtVar<T> evtT;
        public EvtVar<T> EvtT => evtT;
        
        public virtual T Value
        {
            get => EvtT != null ? EvtT.Value : default;
            set
            {
                if (IsEqual(EvtT.Value, value)) return;
                EvtT.Value = value;
            }
        }

        protected virtual bool IsEqual(T current, T other) => current?.Equals(other) ?? false;

        public virtual event Action<T> OnEvt
        {
            add => EvtT.OnEvt += value;
            remove => EvtT.OnEvt -= value;
        }

        public void Invoke(T value) => EvtT.Invoke(value);
    }
}