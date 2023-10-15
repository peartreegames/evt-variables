using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace PeartreeGames.Evt.Variables.Editor
{
    public class EvtItemToolbar<T> : Toolbar where T : Object
    {
        public ListView ListView;
        public int Index;
        private readonly TextField _nameField;

        public EvtItemToolbar(ListView listView, int index)
        {
            ListView = listView;
            Index = index;
            var obj = ListView.itemsSource[Index] as T;
            if (obj == null) return;
            AddToClassList("width-100");
            AddToClassList("margin-5");
            _nameField = new TextField
            {
                name = "ItemName",
                value = obj.name,
                style =
                {
                    width = 300
                }
            };
            _nameField.RegisterCallback<BlurEvent>(OnBlur);
            Add(_nameField);
            var delete = new ToolbarButton(() =>
            {
                AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(obj));
                EvtDatabase.Instance.CreateGUI();
            })
            {
                text = "Delete",
                style =
                {
                    marginLeft = new StyleLength(StyleKeyword.Auto),
                    marginRight = 5
                }
            };

            Add(delete);
        }

        private void OnBlur(BlurEvent evt)
        {
            var item = ListView.Q<VisualElement>(className: "unity-collection-view__item--selected");
            if (item == null) return;

            var obj = ListView.itemsSource[Index] as T;
            AssetDatabase.RenameAsset(AssetDatabase.GetAssetPath(obj), _nameField.value);
            ((Label) item).text = _nameField.value;
        }
    }
}