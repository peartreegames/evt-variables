using System;
using UnityEngine;

namespace PeartreeGames.Evt.Variables
{
    [Serializable]
    public class EvtEvent
    {
        public event Action OnEvt = delegate { };
        public void Invoke() => OnEvt.Invoke();
        public Delegate[] Listeners => OnEvt.GetInvocationList();
    }

    [Serializable]
    public class EvtEvent<T>
    {
        public event Action<T> OnEvt = delegate { };
        public void Invoke(T param) => OnEvt.Invoke(param);
        public Delegate[] Listeners => OnEvt.GetInvocationList();
    }
}