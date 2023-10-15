using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

namespace PeartreeGames.Evt.Variables.Editor
{
    public class EvtSidebar<T> : VisualElement where T : Object
    {
        public Action OnAddItem;
        public Func<VisualElement> MakeItem;
        public Action<VisualElement, int> BindItem;
        public EventCallback<ChangeEvent<string>> OnSearch;

        private readonly List<T> _filteredList;
        private readonly List<T> _fullList;
        private readonly ListView _listView;

        public EvtSidebar(Func<List<T>> getItems, Action<IEnumerable<int>> onSelectionIndexChange)
        {
            var buttons = new VisualElement {name = "LeftButtons"};
            buttons.AddToClassList("flex-row");
            var refresh = new Button(() => EvtDatabase.Instance.CreateGUI()) {text = "Refresh", name = "RefreshItems"};
            refresh.AddToClassList("flex-grow");
            var add = new Button(OnAddItem) {text = "Add", name = "AddItem"};
            add.AddToClassList("flex-grow");
            buttons.Add(refresh);
            buttons.Add(add);

            var input = new ToolbarSearchField {name = "SearchBar"};
            input.AddToClassList("auto-width");
            _listView = new ListView {name = "List"};

            _fullList = getItems != null ? getItems() : LoadAssetsOfType();
            _filteredList = input.value == string.Empty
                ? _fullList
                : _fullList.Where(v => v.name.ToLower().Contains(input.value.ToLower())).ToList();

            AddToClassList("left");
            Add(buttons);

            input.RegisterValueChangedCallback(OnSearchAction);
            _listView.makeItem = MakeItemFunc;
            _listView.bindItem = BindItemAction;
            _listView.itemsSource = _filteredList;
            _listView.fixedItemHeight = 18f;
            _listView.style.minHeight = 500;
            _listView.selectionType = SelectionType.Single;
            _listView.selectedIndicesChanged += onSelectionIndexChange;
            _listView.AddToClassList("margin-5");
            Add(input);
            Add(_listView);
        }

        private EventCallback<ChangeEvent<string>> OnSearchAction => OnSearch ?? (c =>
        {
            _filteredList.Clear();
            _filteredList.AddRange(c.newValue == string.Empty
                ? _fullList
                : _fullList.Where(v => v.name.Contains(c.newValue)).ToList());
            _listView.Rebuild();
        });
        private Func<VisualElement> MakeItemFunc => MakeItem ?? (() => new Label());

        private Action<VisualElement, int> BindItemAction => BindItem ?? ((item, i) =>
        {
            if (i >= _filteredList.Count) return;
            item.userData = _filteredList[i];
            ((Label) item).text = _filteredList[i].name;
        });


        private static List<T> LoadAssetsOfType() =>
            AssetDatabase.FindAssets($"t:{typeof(T).Name}").Select(g =>
                AssetDatabase.LoadAssetAtPath<T>(AssetDatabase.GUIDToAssetPath(g))).ToList();
    }
}