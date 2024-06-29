
using UnityEngine;
using UnityEngine.Events;

namespace PeartreeGames.Evt.Variables
{

    public class EvtEventReference : MonoBehaviour
    {
        [SerializeField] private EvtEvent variable;
        [SerializeField] private UnityEvent onEvent;
    }
    
    public abstract class EvtVariableReference<T> : MonoBehaviour
    {
        [SerializeField] private EvtVariable<T> variable;
        [SerializeField] private UnityEvent<T> onEvent;

        private void Awake()
        {
            onEvent?.Invoke(variable.Value);
            if (onEvent != null) variable.OnEvt += onEvent.Invoke;
        }

        private void OnDestroy()
        {
            if (onEvent != null) variable.OnEvt -= onEvent.Invoke;
        }
    }
}