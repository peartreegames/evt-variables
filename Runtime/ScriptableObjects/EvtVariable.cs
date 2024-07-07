using System;
using UnityEngine;

namespace PeartreeGames.Evt.Variables
{
    public abstract class EvtVariable : ScriptableObject
    {
        public virtual void Reset() {}
    }
    
    public abstract class EvtVariable<T> : EvtVariable
    {
        [SerializeField] private T startValue;
        [SerializeField] private EvtVar<T> evtT;
        
        public virtual T Value
        {
            get => evtT != null ? evtT.Value :  startValue;
            set
            {
                if (evtT == null) return;
                if (IsEqual(evtT.Value, value)) return;
                evtT.Value = value;
            }
        }

        public override void Reset()
        {
            evtT = new EvtVar<T>(startValue);
        }

        protected EvtVar<T> EvtT => evtT;

        protected virtual bool IsEqual(T current, T other) => current?.Equals(other) ?? false;

        public virtual event Action<T> OnEvt
        {
            add => evtT.OnEvt += value;
            remove => evtT.OnEvt -= value;
        }

        public void Invoke(T value) => evtT.Invoke(value);
    }
}