using UnityEngine;

namespace PeartreeGames.Evt.Variables
{
    [CreateAssetMenu(fileName = "float_", menuName = "Evt/Variable/Float", order = 0)]
    public class EvtFloat : EvtVariable<float>
    {
        protected override bool IsEqual(float current, float other) => Mathf.Abs(current - other) < Mathf.Epsilon;
    }
}