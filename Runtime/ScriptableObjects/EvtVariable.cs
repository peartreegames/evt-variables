﻿using System;
using UnityEngine;

namespace PeartreeGames.Evt.Variables
{
    public abstract class EvtVariable : ScriptableObject { }
    public abstract class EvtVariable<T> : EvtVariable
    {
        [SerializeField] private T startValue;
        [SerializeField] private EvtVar<T> evtT;
        
        private void OnEnable()
        {
            if (evtT != null) evtT.Value = startValue;
        }

        private void OnDisable()
        {
            if (evtT != null) evtT.Value = startValue;
        }

        public virtual T Value
        {
            get => evtT?.Value ?? startValue;
            set
            {
                if (evtT == null) return startValue;
                if (IsEqual(evtT.Value, value)) return;
                evtT.Value = value;
            }
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