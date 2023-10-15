using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

namespace PeartreeGames.Evt.Variables.Editor
{
    public class EvtDatabase : EditorWindow
    {
        private static int _index;

        private static int Index
        {
            get => _index;
            set => _index = Mathf.Clamp(value, 0, GetTabs().Count);
        }

        public static EvtDatabase Instance { get; private set; }

        [MenuItem("Tools/Evt/Database")]
        private static void ShowWindow()
        {
            var window = GetWindow<EvtDatabase>();
            window.titleContent = new GUIContent("EvtDatabase", Resources.Load("evt") as Texture);
            window.Show();
        }

        public void CreateGUI()
        {
            Instance = this;
            rootVisualElement.Clear();
            rootVisualElement.styleSheets.Add(Resources.Load<StyleSheet>("EvtDatabase"));

            var tabs = GetTabs();
            var tabContainer = new Toolbar();
            tabContainer.AddToClassList("tab-container");
            rootVisualElement.Add(tabContainer);
            for (var i = 0; i < tabs.Count; i++)
            {
                var tab = tabs[i];
                var index = i;
                var label = new ToolbarToggle {text = tab.Name};
                label.RegisterValueChangedCallback(c =>
                {
                    if (!c.newValue) return;
                    Index = index;
                    CreateGUI();
                });
                label.AddToClassList("tab");
                if (index == Index) label.AddToClassList("tab-active");
                else label.RemoveFromClassList("tab-active");
                tabContainer.Add(label);
            }

            var body = new VisualElement();
            body.AddToClassList("body");
            body.Add(tabs[Index].CreatePage());
            rootVisualElement.Add(body);
        }

        private static IReadOnlyList<IEvtDatabaseTab> GetTabs()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var tabs = new List<IEvtDatabaseTab>();
            foreach (var assembly in assemblies)
            {
                if (assembly.FullName.Contains("Unity")) continue;
                var assemblyTabs = assembly.GetTypes()
                    .Where(t => !t.IsAbstract && t.GetInterface(nameof(IEvtDatabaseTab)) != null)
                    .Select(t => (IEvtDatabaseTab) Activator.CreateInstance(t)).ToList();
                tabs.AddRange(assemblyTabs);
            }

            return tabs;
        }

        public static VisualElement CreatePage<T>(Func<List<T>> getItems = null,
            Action<IEnumerable<int>> onSelectionIndexChange = null, Action onAddItem = null)
            where T : Object
        {
            onSelectionIndexChange ??= OnListSelectionChange<T>;
            var split = new TwoPaneSplitView(0, 200, TwoPaneSplitViewOrientation.Horizontal);
            split.Add(new EvtSidebar<T>(getItems, onSelectionIndexChange)
            {
                name = "Left",
                OnAddItem = onAddItem
            });
            var right = new VisualElement {name = "Right"};
            split.Add(right);

            split.AddToClassList("margin-5");
            return split;
        }

        public static void OnListSelectionChange<T>(IEnumerable<int> indices) where T : Object
        {
            var right = Instance.rootVisualElement.Q("Right");
            right.Clear();
            if (indices == null) return;
            var list = Instance.rootVisualElement.Q<ListView>("List");
            foreach (var i in indices)
            {
                if (list.itemsSource[i] == null) continue;
                right.Add(new EvtItemToolbar<T>(list, i) {name = "ItemToolbar"});
                right.Add(new InspectorElement(new SerializedObject(list.itemsSource[i] as T)));
                break;
            }
        }
    }
}