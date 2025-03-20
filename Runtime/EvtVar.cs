using System;
using UnityEngine;

namespace PeartreeGames.Evt.Variables
{
    [Serializable]
    public class EvtVar<T> : EvtEvent<T>
    {
        [SerializeField] private T value;

        public EvtVar() => value = default;
        public EvtVar(T startingValue) => value = startingValue;

        public T Value
        {
            get => value;
            set
            {
                if (this.value == null && value == null) return;
                if (this.value != null && this.value.Equals(value)) return;
                this.value = value;
                Invoke(this.value);
            }
        }

        public void SetWithoutNotify(T newValue) => value = newValue;
    }
}