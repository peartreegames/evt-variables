using UnityEngine;

namespace PeartreeGames.EvtVariables
{
    [CreateAssetMenu(fileName = "float_", menuName = "Evt/FloatObject", order = 0)]
    public class EvtFloatObject : EvtVariableObject<float>
    {
        protected override bool IsEqual(float current, float other) => Mathf.Abs(current - other) < Mathf.Epsilon;
    }
}