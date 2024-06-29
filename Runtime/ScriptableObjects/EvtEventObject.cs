using System;
using UnityEngine;

namespace PeartreeGames.Evt.Variables
{
    
    [CreateAssetMenu(fileName = "evt_", menuName = "Evt/Event", order = 0)]
    public class EvtEventObject : ScriptableObject
    {
        private readonly EvtEvent _evt = new();
        
        public event Action OnEvt
        {
            add => _evt.OnEvt += value;
            remove => _evt.OnEvt -= value;
        }

        public void Invoke() => _evt?.Invoke();
    }
}