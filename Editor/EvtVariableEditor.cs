using System;
using System.Reflection;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

namespace PeartreeGames.Evt.Variables.Editor
{
    [CustomEditor(typeof(EvtVariable), true)]
    public class EvtVariableEditor : UnityEditor.Editor
    {
        private SerializedProperty _evtTProperty;
        private SerializedProperty _isEventOnlyProperty;

        private void OnEnable()
        {
            _evtTProperty = serializedObject.FindProperty("evtT");
            _isEventOnlyProperty = serializedObject.FindProperty("isEventOnly");
        }

        public override VisualElement CreateInspectorGUI()
        {
            var elem = new VisualElement();
            elem.AddToClassList("flex-row");
            var value = _evtTProperty.FindPropertyRelative("value");
            object currentValue = value.boxedValue;

            var isEventField = new PropertyField(_isEventOnlyProperty);
            isEventField.Bind(serializedObject);
            isEventField.RegisterValueChangeCallback(_ => CreateValueField());
            elem.Add(isEventField);

            CreateValueField();

            var type = target.GetType();
            var info = type.GetMethod("Invoke", BindingFlags.Instance | BindingFlags.Public);
            if (info != null)
            {
                var invokeButton = new Button(() => info.Invoke(target, new[] { currentValue }))
                {
                    text = "Invoke"
                };
                elem.Add(invokeButton);
            }

            return elem;

            void CreateValueField()
            {
                var old = elem.Q("value");
                if (old != null) elem.Remove(old);
                var isEvt = _isEventOnlyProperty.boolValue;
                if (value.propertyType == SerializedPropertyType.ObjectReference && isEvt)
                {
                    var field = new ObjectField("Value (InvokeArg)")
                        { allowSceneObjects = true, name = "value" };
                    field.AddToClassList("flex-grow");
                    field.RegisterValueChangedCallback(evt => currentValue = evt.newValue);
                    elem.Insert(1, field);
                }
                else
                {
                    var field = new PropertyField(value)
                        { name = "value", label = !isEvt ? "Value" : "Value (InvokeArg)" };
                    field.AddToClassList("flex-grow");
                    field.RegisterValueChangeCallback(evt =>
                        currentValue = evt.changedProperty.boxedValue);
                    field.Bind(serializedObject);
                    elem.Insert(1, field);
                }
            }
        }
    }
}