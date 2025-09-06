
using UnityEngine;
using UnityEngine.Events;

namespace PeartreeGames.Evt.Variables
{

    public class EvtEventReference : MonoBehaviour
    {
        [SerializeField] private EvtEventObject variable;
        [SerializeField] private UnityEvent onEvent;
        private void OnEnable()
        {
            if (onEvent != null) variable.OnEvt += onEvent.Invoke;
        }

        private void OnDisable()
        {
            if (onEvent != null) variable.OnEvt -= onEvent.Invoke;
        }
    }
    
    public abstract class EvtVariableReference<T> : MonoBehaviour
    {
        [SerializeField] private EvtVariable<T> variable;
        [SerializeField] private UnityEvent<T> onEvent;
        [SerializeField] private bool triggerOnEnable;

        private void OnEnable()
        {
            if (triggerOnEnable) onEvent?.Invoke(variable.Value);
            if (onEvent != null) variable.OnEvt += onEvent.Invoke;
        }

        private void OnDisable()
        {
            if (onEvent != null) variable.OnEvt -= onEvent.Invoke;
        }
    }
}