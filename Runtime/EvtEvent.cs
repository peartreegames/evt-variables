using System;
using System.Linq;
using UnityEngine;

namespace PeartreeGames.EvtVariables
{
    [Serializable]
    public class EvtEvent
    {
        [HideInInspector]
        private Action _action = delegate {  };
        public void Invoke() => _action.Invoke();
        public void Subscribe(Action listener) => _action += listener;
        public void Unsubscribe(Action listener) => _action -= listener;
        public virtual Delegate[] Listeners => _action.GetInvocationList();
    }

    [Serializable]
    public class EvtEvent<T> : EvtEvent
    {
        [HideInInspector]
        private Action<T> _actionT = delegate {  };

        public void Invoke(T param)
        {
            _actionT.Invoke(param);
            base.Invoke();
        }

        public void Subscribe(Action<T> listener) => _actionT += listener;
        public void Unsubscribe(Action<T> listener) => _actionT -= listener;
        public override Delegate[] Listeners => base.Listeners.ToList().Concat(_actionT.GetInvocationList()).ToArray();
    }
}