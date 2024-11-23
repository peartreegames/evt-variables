using System;
using System.Reflection;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace PeartreeGames.Evt.Variables.Editor
{
    [CustomEditor(typeof(EvtVariable), true)]
    public class EvtVariableEditor : UnityEditor.Editor
    {
        private SerializedProperty _evtTProperty;
        private PropertyField _field;

        private void OnEnable()
        {
            _evtTProperty = serializedObject.FindProperty("evtT");
        }

        public override VisualElement CreateInspectorGUI()
        {
            var elem = new VisualElement();
            elem.AddToClassList("flex-row");
            if (_evtTProperty != null)
            {
                var value = _evtTProperty.FindPropertyRelative("value");
                _field = new PropertyField(value){ name = "value" };
                _field.AddToClassList("flex-grow");
                _field.Bind(serializedObject);
                elem.Add(_field);
            }

            var type = target.GetType();
            var info = type.GetMethod("Invoke", BindingFlags.Instance | BindingFlags.Public);
            if (info != null)
            {
                var invoke = new Button(() =>
                {
                    var prop = type.GetProperty("Value");
                    if (prop == null) info.Invoke(target, Array.Empty<object>());
                    else
                    {
                        var val = prop.GetValue(target);
                        info.Invoke(target, new[] {val});
                    }
                }) {text = "INVOKE", style = {maxHeight = 16}};
                elem.Add(invoke);
            }

            return elem;
        }
    }
}