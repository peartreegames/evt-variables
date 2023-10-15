using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace PeartreeGames.Evt.Variables.Editor
{
    public static class EvtDatabaseUtilities
    {
        public static void ProjectPing<T>(VisualElement page) where T : ScriptableObject
        {
            var item =
                page.Q<ListView>().Q<VisualElement>(className: "unity-collection-view__item--selected")
                    .userData as T;
            Selection.activeObject = item;
        }

        public static void SceneSearch<T>(VisualElement page) where T : ScriptableObject
        {
            var item =
                page.Q<ListView>().Q<VisualElement>(className: "unity-collection-view__item--selected")
                    .userData as T;
            var windows =
                (SearchableEditorWindow[]) Resources.FindObjectsOfTypeAll(typeof(SearchableEditorWindow));
            SearchableEditorWindow hierarchy = null;
            foreach (var window in windows)
            {
                if (window.GetType().ToString() != "UnityEditor.SceneHierarchyWindow") continue;
                hierarchy = window;
                break;
            }

            if (hierarchy == null)
                return;

            var setSearchType = typeof(SearchableEditorWindow).GetMethod("SetSearchFilter",
                BindingFlags.NonPublic | BindingFlags.Instance);
            var parameters = new object[]
            {
                $"ref:{AssetDatabase.GetAssetPath(item)}", (int) HierarchyType.Assets,
                false, false
            };

            setSearchType?.Invoke(hierarchy, parameters);
        }
    }
}