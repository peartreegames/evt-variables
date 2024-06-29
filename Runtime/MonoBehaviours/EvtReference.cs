
using UnityEngine;
using UnityEngine.Events;

namespace PeartreeGames.Evt.Variables
{
    public abstract class EvtReference<T> : MonoBehaviour
    {
        [SerializeField] private EvtVariable<T> variable;
        [SerializeField] private UnityEvent<T> onEvent;

        private void Awake()
        {
            variable.OnEvt += onEvent.Invoke;
        }

        private void OnDestroy()
        {
            variable.OnEvt -= onEvent.Invoke;
        }
    }
}