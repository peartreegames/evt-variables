using System.Collections.Generic;
using PeartreeGames.Evt.Variables.Lists;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace PeartreeGames.Evt.Variables.Editor
{
    public class EvtDatabaseTabVariableList : EvtDatabaseTab<EvtVariableList>
    {
        public override string Name => "Lists";

        public override VisualElement CreatePage()
        {
            Page ??= EvtDatabase.CreatePage<EvtVariableList>(null, OnSelectionIndexChange);
            return Page;
        }

        private void OnSelectionIndexChange(IEnumerable<int> indices)
        {
            EvtDatabase.OnListSelectionChange<EvtVariableList>(indices);
            var toolbar = Page.Q<Toolbar>("ItemToolbar");
            var scene = new ToolbarButton(() => EvtDatabaseUtilities.SceneSearch<EvtVariableList>(Page))
                {text = "Find In Scene"};
            var project = new ToolbarButton(() => EvtDatabaseUtilities.ProjectPing<EvtVariableList>(Page))
                {text = "Find In Project"};
            toolbar.Insert(1, scene);
            toolbar.Insert(2, project);
        }
    }
}